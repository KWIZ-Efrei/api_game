using MongoDB.Bson;

namespace kwiz_api_game.Models.Entities;

public class Question
{
    public string Id { get; set; }
    public bool? CorrectlyAnswered { get; set; }
}