using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IGameService
    {
        Task InitializeGame(DepthChart depthChart);
        Task<bool> AddPlayerToDepthChart(int playerId, string position, int? positionDepth = null);
        Task<DepthChart?> GetFullDepthChart();
        Task<List<int>> GetPlayersUnderPlayerInDepthChart(int id, string position);
        Task<bool> RemovePlayerFromDepthChart(int playerId, string position);
    }
}
