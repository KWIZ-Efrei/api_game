using GrandeNoctuleAPI_Main.Helpers.Exceptions;
using kwiz_api_game.Services;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// ssl security
builder.WebHost.UseUrls("http://kwiz-game-api.test");

// cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        Console.Out.WriteLine("Adding cors policy");
        corsPolicyBuilder.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<AppExceptionFiltersAttribute>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services to the container.
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();

// HttpClients
builder.Services.AddHttpClient<IQuestionService, QuestionService>(client =>
{
    client.BaseAddress = new Uri("http://kwiz-api-question:8080/");
});

var configuration = builder.Configuration;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();