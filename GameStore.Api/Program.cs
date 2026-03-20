using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new GameDto(
        1, 
        "The Legend of Zelda: Breath of the Wild", 
        "Action-Adventure", 
        59.99m, 
        new DateOnly(2017, 3, 3)),
    new GameDto(
        2, 
        "God of War", 
        "Action-Adventure", 
        49.99m, 
        new DateOnly(2018, 4, 20)),
    new GameDto(
        3, 
        "Red Dead Redemption 2", 
        "Action-Adventure", 
        39.99m, 
        new DateOnly(2018, 10, 26)),
];

// GET /games
app.MapGet("/games", () => games);

// GET /games/{id}
app.MapGet("/games/{id}", (int id) => 
    games.Find(game => game.Id == id)
).WithName("GetGame");

// POST /games
app.MapPost("/games", (CreateGameDto newGame) =>
{
   GameDto game = new GameDto(
    games.Count + 1,
    newGame.Name,
    newGame.Genre,
    newGame.Price,
    newGame.ReleaseDate
   );

   games.Add(game);

   return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game);
});

// app.MapGet("/", () => "Hello World!");

app.Run();
