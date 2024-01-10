namespace kwiz_api_game.Models.DTO;

public record QuestionResponse(string Id, string QuestionText, IEnumerable<string> Answers, bool? CorrectlyAnswered);

public record QuestionRequest(string Id, string attemptedAnwser);

public record QuestionDTO(string Id, string QuestionText, IEnumerable<string> Answers);

public record QuestionAnswerResponse(string Id, bool? CorrectlyAnswered);