using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DepthCharts.Queries.GetFullDepthChart
{
    public record GetFullDepthChartQuery : IRequest<DepthChartVM>;

    public class GetFullDepthChartQueryHandler : IRequestHandler<GetFullDepthChartQuery, DepthChartVM>
    {

        private readonly IMapper _mapper;
        private IGameService _gameService;

        public GetFullDepthChartQueryHandler(IMapper mapper, IGameService gameService)
        {
            _mapper = mapper;
            _gameService = gameService;
        }

    public async Task<DepthChartVM> Handle(GetFullDepthChartQuery request, CancellationToken cancellationToken)
    {
            var chart = await _gameService.GetFullDepthChart();
            return _mapper.Map<DepthChartVM>(chart);
    }
}
}
