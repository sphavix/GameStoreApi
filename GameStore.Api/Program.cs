using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpoint = "GetGame";
List<GameDto> games = [
    new(
        1, 
        "Street Fighter II",
        "Fighting",
        19.99m,
        new DateOnly(1992, 7, 15)),
    new(
        2, 
        "Call of Duty V",
        "Fighting",
        50.99m,
        new DateOnly(1995, 11, 15)),
    new(
        3, 
        "Final Fantasy VII Rebirth",
        "Arcade",
        44.99m,
        new DateOnly(2012, 4, 23)),
    new(
        4, 
        "Astro Bot",
        "Arcade",
        30.99m,
        new DateOnly(2016, 9, 22)),
    new(
        5, 
        "Need For Speed Most Wanted",
        "Racing",
        34.99m,
        new DateOnly(2001, 5, 25))
];

// GET: /games
app.MapGet("/games", () => games);

// GET: /games/{id}
app.MapGet("/games/{id}", (int id) =>
{
    var game = games.Find( game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
    .WithName(GetGameEndpoint);

// POST: /games
app.MapPost("/games", (CreateGameDto game) =>
{
    GameDto newGame = new (
        games.Count+1,
        game.Name,
        game.Genre,
        game.Price,
        game.ReleaseDate
    );

    games.Add(newGame);

    return Results.CreatedAtRoute(GetGameEndpoint, new { id = newGame.Id }, newGame);
});

// PUT: /games/{id}
app.MapPut("games/{id}", (int id, UpdateGameDto game) =>
{
   var index = games.FindIndex(game => game.Id == id);

    games[index] = new GameDto(
        id,
        game.Name,
        game.Genre,
        game.Price,
        game.ReleaseDate
    );

    return Results.NoContent();
});

// DELETE: /games/{id}
app.MapDelete("games/{id}", (int id) =>
{
   games.RemoveAll(game => game.Id ==id);

   return Results.NoContent();
});

app.Run();
