using kwiz_api_game.Models.DTO;
using kwiz_api_game.Models.Entities;
using MongoDB.Bson;

namespace kwiz_api_game.Models.Extensions;

public static class QuestionsExtensions
{
    public static Question AsEntity(this QuestionDTO question)
        => new()
        {
            Id = question.Id,
            CorrectlyAnswered = null
        };

    public static QuestionResponse AsResponse(this QuestionDTO questionDto, bool? correctlyAnswered = null)
        => new QuestionResponse(
            questionDto.Id, 
            questionDto.QuestionText, 
            questionDto.Answers,
            correctlyAnswered);
}