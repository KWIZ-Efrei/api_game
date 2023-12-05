namespace kwiz_api_game.Models.DTO;

public record GameResponse(string Id, IEnumerable<QuestionResponse> Questions);