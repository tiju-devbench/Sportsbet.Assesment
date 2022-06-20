using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Common.Services
{
    public class GameService : IGameService
    {
        private static DepthChart? _depthChart;
        private bool _isInitialized = false;

        public async Task<bool> AddPlayerToDepthChart(int playerId, string position, int? positionDepth = null)
        {
            if (!_isInitialized)
            {
                //Log error
                throw new Exception("Game chart is not initialized.");
                
            }
            var playerList = FindPlayersInPosition(position);
            if (playerList.Any())
            {
                if(playerList.Any(x=> x == playerId))
                {
                    throw new Exception($"Player already exist in the position. PlayerId: {playerId}, Position: {position}.");
                }

                if (positionDepth < 0)
                {                    
                    playerList.Add(playerId);

                }
                else if(positionDepth > playerList.Count())
                {
                    playerList.AddRange(Enumerable.Repeat(0, positionDepth.GetValueOrDefault(0) - playerList.Count()).ToList());
                    playerList.Add(playerId);
                    
                }
                else
                {
                    playerList.Insert(positionDepth.GetValueOrDefault(), playerId);
                }
            }
            else
            {
                if (positionDepth > 0)
                {
                    playerList = Enumerable.Repeat(0, positionDepth.GetValueOrDefault(0) - 1).ToList();
                }
                playerList.Add(playerId);
            }

            UpsertPlayersInPosition(position, playerList);
            return true;
        }

        public async Task InitializeGame(DepthChart depthChart)
        {
            if (depthChart != null)
            {
                _depthChart = depthChart;
                _isInitialized = true;
            }
        }

        public async Task<DepthChart> GetFullDepthChart()
        {            
            return _depthChart;
        }
        public async Task<bool> RemovePlayerFromDepthChart(int playerId, string position)
        {
            if (!_isInitialized)
            {
                //Log error
                throw new Exception("Game chart is not initialized.");
            }
            var playerList = FindPlayersInPosition(position);
            if (playerList.Any())
            {
                playerList.Remove(playerId);
                return true;
            }
            return false;
        }
        public async Task<List<int>> GetPlayersUnderPlayerInDepthChart(int id, string position)
        {
            var result = new List<int>();
            var playersInPosition = FindPlayersInPosition(position);
            if (playersInPosition.Any())
            {
                var playerIndex = playersInPosition.IndexOf(id);
                var filteredPlayers = playersInPosition.Skip(playerIndex + 1).ToList();
                return filteredPlayers;
            }
            return result;
        }

        private List<int> FindPlayersInPosition(string position)
        {
            var isValid = _depthChart.Chart.TryGetValue(position, out var result);
            if (isValid)
                return result;
            return null;
        }

        private bool UpsertPlayersInPosition(string position, List<int> playerList)
        {
            var isUpdate = _depthChart.Chart.TryGetValue(position, out var result);
            if (isUpdate)
            {
                _depthChart.Chart[position] = playerList;
            }
            else
            {
                _depthChart.Chart.Add(position, playerList);
            }
            return true;
        }
    }
}
