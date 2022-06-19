using Application.Common.Enums;
using Application.Common.Interfaces;
using Domain.Common.Exceptions;
using Domain.Entities;

using MediatR;

namespace Application.DepthCharts.Commands.CreateDepthChart
{
    public record CreateDepthChartCommand : IRequest<DepthChart>
    {
        public GameTypes GameType { get; set; }
        
    }
    public class CreateDepthChartCommandHandler : IRequestHandler<CreateDepthChartCommand, DepthChart>
    {
        private IGameService _gameService;

        public CreateDepthChartCommandHandler(IGameService gameService)
        {
            _gameService = gameService;
        }
        public async Task<DepthChart> Handle(CreateDepthChartCommand request, CancellationToken cancellationToken)
        {
            DepthChart chart = request.GameType switch
            {
                GameTypes.NFL => new NflDepthChart(),
                GameTypes.MLB => new MlbDepthChart(),
                _ => throw new DomainUnprocessableException("Game type not supported.")
            };
            await _gameService.InitializeGame(chart);
            return chart;
        }
    }
}
