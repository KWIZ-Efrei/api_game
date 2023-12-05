using kwiz_api_game.Models.DTO;
using kwiz_api_game.Models.Entities;
using MongoDB.Bson;

namespace kwiz_api_game.Models.Extensions;

public static class GamesExtensions
{
    public static GameResponse AsResponse(this Game game, List<QuestionDTO> questionDtos)
    {
        return new GameResponse(
            game.Id.ToString(),
            questionDtos.Select(q => 
                q.AsResponse(
                    game.Questions
                        .FirstOrDefault(qe => qe.Id == q.Id)?
                        .CorrectlyAnswered))
        );
    }

    public static List<string> GetQuestionsIds(this Game game)
        => game.Questions.Select(q => q.Id).ToList();
}