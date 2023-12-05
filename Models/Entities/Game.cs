using MongoDB.Bson;

namespace kwiz_api_game.Models.Entities;

public class Game
{
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    
    public List<Question> Questions { get; set; }
}