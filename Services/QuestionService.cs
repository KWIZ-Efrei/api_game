using GrandeNoctuleAPI_Main.Helpers.Exceptions;
using kwiz_api_game.Helpers.Extensions;
using kwiz_api_game.Models.DTO;

namespace kwiz_api_game.Services;

public interface IQuestionService
{
    Task<bool> TryAnswerAsync(QuestionRequest request);
    Task<List<QuestionDTO>> GetRandomQuestionsAsync(int numberOfQuestions = 10);
    Task<List<QuestionDTO>> GetQuestionsAsync(List<string> ids);
}

public class QuestionService : IQuestionService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<QuestionService> _logger;

    public QuestionService(HttpClient httpClient, ILogger<QuestionService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<bool> TryAnswerAsync(QuestionRequest request)
    {
        var endpoint = $"questions/answer/{request.Id}";
        var response = await _httpClient.PostAsync<bool>(endpoint, request);
        return response;
    }

    public async Task<List<QuestionDTO>> GetRandomQuestionsAsync(int numberOfQuestions = 10)
    {
        var endpoint = $"questions/random/{numberOfQuestions}";
        var response = await _httpClient.GetAsync<List<QuestionDTO>>(endpoint);
        
        if (response == null)
            throw new AppException("Questions not found", 404, _logger);
        
        return response;
    }

    public async Task<List<QuestionDTO>> GetQuestionsAsync(List<string> ids)
    {
        var endpoint = "sent-questions-by-id";
        var body = new { ids };
        var response = await _httpClient.PostAsync<List<QuestionDTO>>(endpoint, body);
        
        if (response == null)
            throw new AppException("Questions not found", 404, _logger);
        
        return response;
    }
}