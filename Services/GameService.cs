using GrandeNoctuleAPI_Main.Helpers.Exceptions;
using kwiz_api_game.Models.DTO;
using kwiz_api_game.Models.Entities;
using kwiz_api_game.Models.Extensions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace kwiz_api_game.Services;

public interface IGameService
{
    Task<GameResponse> CreateGameAsync();
    Task<GameResponse> GetGameAsync(string id);
    Task<QuestionAnswerResponse> TryAnswerAsync(string id, QuestionRequest request);
}
public class GameService : IGameService
{
    private readonly ILogger<GameService> _logger;
    private readonly DataContext _context;
    private readonly IQuestionService _questionService;

    public GameService(ILogger<GameService> logger, IQuestionService questionService,
        IConfiguration configuration)
    {
        _logger = logger;
        _context = DataContext.Create(new MongoClient(configuration["MongoDB"]).GetDatabase("kwiz_games"));
        _questionService = questionService;
    }

    public async Task<GameResponse> CreateGameAsync()
    {
        var game = new Game { Questions = new List<Question>() };
        
        // ask questions Api using service
        var randomQuestions = await _questionService
            .GetRandomQuestionsAsync();
        
        // transform questions to game questions
        game.Questions.AddRange(
            randomQuestions.Select(q => q.AsEntity()));
        
        // save game
        var entityEntry = await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();

        game.Id = entityEntry.Entity.Id;
        
        // return game
        return game.AsResponse(randomQuestions);
    }

    public async Task<GameResponse> GetGameAsync(string id)
    {
        var game = await GetGameEntityAsync(id);

        var questionDtos = await _questionService
            .GetQuestionsAsync(game.GetQuestionsIds());

        return game.AsResponse(questionDtos);
    }

    public async Task<QuestionAnswerResponse> TryAnswerAsync(string id, QuestionRequest request)
    {
        var game = await GetGameEntityAsync(id);
        var question = game.Questions.FirstOrDefault(q => q.Id == request.Id);
        
        if (question == null)
            throw new AppException("Question not found", 404, _logger);

        var isResponseCorrect = await _questionService.TryAnswerAsync(request);
        
        // update game
        question.CorrectlyAnswered = isResponseCorrect;
        await _context.SaveChangesAsync();

        return question.AsResponse();
    }

    // helpers methods
    private async Task<Game> GetGameEntityAsync(string id)
    {
        var game = await _context.Games
            .Include(g => g.Questions)
            .FirstOrDefaultAsync(g => g.Id.ToString() == id);

        if (game == null)
            throw new AppException("Game not found", 404, _logger);

        return game;
    }
}