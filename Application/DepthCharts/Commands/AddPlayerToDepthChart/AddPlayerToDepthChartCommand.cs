using Application.Common.Interfaces;
using Domain.Common.Exceptions;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DepthCharts.Commands.AddPlayerToDepthChart
{
    public record AddPlayerToDepthChartCommand : IRequest<bool>
    {
        public int PlayerId { get; set; }
        public string Position { get; set; }
        public int? PositionDepth { get; set; }

    }
    public class AddPlayerToDepthChartCommandHandler : IRequestHandler<AddPlayerToDepthChartCommand, bool>
    {
        private readonly IGameService _gameService;

        public AddPlayerToDepthChartCommandHandler(IGameService gameService)
        {
            _gameService = gameService;
        }
        public async Task<bool> Handle(AddPlayerToDepthChartCommand request, CancellationToken cancellationToken)
        {
            var result = await AddPlayerToDepthChart(request.PlayerId, request.Position, request.PositionDepth);
            return result;
            
        }

        private async Task<bool> AddPlayerToDepthChart(int playerId, string position, int? positionDepth)
        {
            try
            {
                var result = await _gameService.AddPlayerToDepthChart(playerId, position, positionDepth);
                return result;
            }
            catch (Exception ex)
            {
                throw new DomainInvalidOperationException($"Error adding player to depth chart. PlayeId: {playerId}. Message: {ex.Message}");
            }
        }
    }
}
