


using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.DepthCharts.Commands.AddPlayerToDepthChart;
using Application.DepthCharts.Commands.CreateDepthChart;
using Application.DepthCharts.Queries.GetFullDepthChart;
using Application.DepthCharts.Queries.GetPlayersUnderPlayerInDepthChart;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddApplicationServices()).Build();

RunGame(host.Services);
await host.RunAsync();


async void RunGame(IServiceProvider services)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;    

    await InitializeGame(provider, GameTypes.NFL.ToString());
    var bob = new Player(1, "Bob");
    var alice = new Player(2, "Alice ");
    var charlie = new Player(3, "Charlie");

    await AddPlayerToDepthChart(provider, bob, "WR", 0);
    await AddPlayerToDepthChart(provider, alice, "WR", 0);
    await AddPlayerToDepthChart(provider, charlie, "WR", 2);
    await AddPlayerToDepthChart(provider, bob, "KR");

    var fullDepthChart = await GetFullDepthChart(provider);

    var subList = await GetPlayersUnderPlayerInDepthChart(provider, alice, "WR");

    Console.WriteLine("Output: GetFullDepthChart");
    foreach (var item in fullDepthChart.Chart)
    {        
        Console.WriteLine($"{item.Key} - {string.Join(" ",item.Value.AsEnumerable())}");
    }

    Console.WriteLine("Output: GetPlayersUnderPlayerInDepthChart(alice, 'WR')");
    Console.WriteLine($"{string.Join(" ", subList.PlayerList.AsEnumerable())}");


    Console.ReadKey();
}

async Task<PlayerListVM> GetPlayersUnderPlayerInDepthChart(IServiceProvider provider, Player player, string position)
{
    var _mediator = provider.GetRequiredService<IMediator>();
    var query = new GetPlayersUnderPlayerInDepthChartQuery
    {
        Id = player.Id,
        Position = position
    };
    return await _mediator.Send(query);
}

async Task<DepthChartVM> GetFullDepthChart(IServiceProvider provider)
{
    var _mediator = provider.GetRequiredService<IMediator>();
    var query = new GetFullDepthChartQuery
    {
    };
    return await _mediator.Send(query);
}

async Task<bool> AddPlayerToDepthChart(IServiceProvider provider, Player player, string position, int? positionDepth = null)
{
    var _mediator = provider.GetRequiredService<IMediator>();
    var command = new AddPlayerToDepthChartCommand
    {
        PlayerId = player.Id,
        Position = position,
        PositionDepth = positionDepth.GetValueOrDefault(),
        
    };
    return await _mediator.Send(command);
}

async Task<bool> InitializeGame(IServiceProvider provider, string gameType)
{
    Enum.TryParse(gameType, out GameTypes outGameType);
    var _mediator = provider.GetRequiredService<IMediator>();
    var command = new CreateDepthChartCommand
    {
        GameType = outGameType
    };
    var chart = await _mediator.Send(command);
    if(chart == null) { return false; }
    return true;
}


