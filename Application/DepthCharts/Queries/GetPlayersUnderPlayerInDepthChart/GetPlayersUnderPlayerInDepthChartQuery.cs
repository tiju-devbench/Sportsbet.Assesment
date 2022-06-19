using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DepthCharts.Queries.GetPlayersUnderPlayerInDepthChart
{
    public record GetPlayersUnderPlayerInDepthChartQuery : IRequest<PlayerListVM>
    {
        public int Id { get; set; }
        public string Position { get; set; }
    }
    public class GetPlayersUnderPlayerInDepthChartQueryHandler : IRequestHandler<GetPlayersUnderPlayerInDepthChartQuery, PlayerListVM>
    {

        private readonly IMapper _mapper;
        private IGameService _gameService;

        public GetPlayersUnderPlayerInDepthChartQueryHandler(IMapper mapper, IGameService gameService)
        {
            _mapper = mapper;
            _gameService = gameService;
        }

        public async Task<PlayerListVM> Handle(GetPlayersUnderPlayerInDepthChartQuery request, CancellationToken cancellationToken)
        {
            var playerList = await _gameService.GetPlayersUnderPlayerInDepthChart(request.Id, request.Position);
            return _mapper.Map<PlayerListVM>(playerList);
        }
    }
}
