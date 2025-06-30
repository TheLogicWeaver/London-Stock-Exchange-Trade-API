using LseTradeApi.Web;
using LseTradeApi.Web.DTOs;
using LseTradeApi.Web.Interfaces;
using LseTradeApi.Web.Repositories;
using LseTradeApi.Web.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "LSE Trade API", Version = "v1" });
});

// Get connection string from environment variable
var config = new ConfigurationBuilder()
    .AddJsonFile("local.settings.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var connectionString = config["Values:TRADE_DB_CONNECTION"]
    ?? throw new InvalidOperationException("Missing DB connection string.");

builder.Services.AddDbContext<TradeDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TradeDbContext>();
    dbContext.Database.EnsureCreated();
}

app.MapPost("/trades", async (TradeRequest request, ITradeService service) =>
{
    var tradeId = await service.RecordTradeAsync(request);
    return Results.Ok(new { message = "Trade recorded", trade_id = tradeId });
});

app.MapGet("/stock/{symbol}", async (string symbol, ITradeService service) =>
{
    var result = await service.GetStockAverageAsync(symbol);
    return result is not null ? Results.Ok(result) : Results.NotFound();
});

app.MapGet("/stocks", async (ITradeService service) =>
{
    var results = await service.GetAllAveragesAsync();
    return Results.Ok(results);
});

app.MapPost("/stocks/range", async (List<string> symbols, ITradeService service) =>
{
    var results = await service.GetAveragesBySymbolsAsync(symbols);
    return Results.Ok(results);
});

app.Run();
