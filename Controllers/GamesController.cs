using kwiz_api_game.Models.DTO;
using kwiz_api_game.Services;
using Microsoft.AspNetCore.Mvc;

namespace kwiz_api_game.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;
    
    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }
    
    [HttpGet("{id}")]
    public async Task<GameResponse> GetGameAsync(string id)
        => await _gameService.GetGameAsync(id);
    
    [HttpPost]
    public async Task<GameResponse> CreateGameAsync()
        => await _gameService.CreateGameAsync();
    
    [HttpPost("{id}/answer")]
    public async Task<QuestionAnswerResponse> TryAnswerAsync(string id, QuestionRequest request)
        => await _gameService.TryAnswerAsync(id, request);
    
    
    
}