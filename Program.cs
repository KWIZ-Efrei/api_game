using GrandeNoctuleAPI_Main.Helpers.Exceptions;
using kwiz_api_game.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

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
    client.BaseAddress = new Uri("http://localhost:8080/");
});

var configuration = builder.Configuration;
// Register the MongoDB DbContext
builder.Services.AddScoped<DataContext>(provider => 
    DataContext.Create(new MongoClient(configuration["MongoDB"]).GetDatabase("kwiz_games")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();