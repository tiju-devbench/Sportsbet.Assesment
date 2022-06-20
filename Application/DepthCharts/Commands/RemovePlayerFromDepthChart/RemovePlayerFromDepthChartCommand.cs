using Application.Common.Interfaces;
using Domain.Common.Exceptions;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DepthCharts.Commands.RemovePlayerFromDepthChart
{
    public record RemovePlayerFromDepthChartCommand : IRequest<bool>
    {
        public int PlayerId { get; set; }
        public string Position { get; set; }

    }
    public class RemovePlayerFromDepthChartCommandHandler : IRequestHandler<RemovePlayerFromDepthChartCommand, bool>
    {
        private readonly IGameService _gameService;

        public RemovePlayerFromDepthChartCommandHandler(IGameService gameService)
        {
            _gameService = gameService;
        }
        public async Task<bool> Handle(RemovePlayerFromDepthChartCommand request, CancellationToken cancellationToken)
        {
            var result = await RemovePlayerFromDepthChart(request.PlayerId, request.Position);
            return result;
            
        }

        private async Task<bool> RemovePlayerFromDepthChart(int playerId, string position)
        {
            try
            {
                var result = await _gameService.RemovePlayerFromDepthChart(playerId, position);
                return result;
            }
            catch (Exception ex)
            {
                throw new DomainInvalidOperationException($"Error removing player to depth chart. PlayeId: {playerId}. Message: {ex.Message}");
            }
        }
    }
}
