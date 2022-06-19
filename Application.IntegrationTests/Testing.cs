
using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.DepthCharts.Commands.AddPlayerToDepthChart;
using Application.DepthCharts.Commands.CreateDepthChart;
using Application.DepthCharts.Queries.GetFullDepthChart;
using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;
using Respawn;

namespace Application.IntegrationTests;

    [SetUpFixture]
    public partial class Testing
    {
    private static IServiceProvider _serviceProvider;

    [OneTimeSetUp]
        public void RunBeforeAnyTestsAsync()
        {
        IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
                services.AddApplicationServices()).Build();
        _serviceProvider = host.Services;
    }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var mediator = _serviceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(request);
        }

        public static async Task<DepthChartVM> GetFullDepthChart()
        {
            var query = new GetFullDepthChartQuery
            {
            };
            return await FluentActions.Invoking(() =>
            SendAsync(query)).Invoke();
        }

        public static async Task CreateGameChart()
        {
            var command = new CreateDepthChartCommand
            {
                GameType = GameTypes.NFL
            };
            var chart = await FluentActions.Invoking(() =>
            SendAsync(command)).Invoke();
        }

    public static async Task AddPlayerToDepthChart(int id, string position, int? positionDepth = null)
    {
        var command = new AddPlayerToDepthChartCommand
        {
            PlayerId = id,
            Position = position,
            PositionDepth = positionDepth
        };
        var chart = await FluentActions.Invoking(() =>
        SendAsync(command)).Invoke();
    }


    [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        
        }
    }

