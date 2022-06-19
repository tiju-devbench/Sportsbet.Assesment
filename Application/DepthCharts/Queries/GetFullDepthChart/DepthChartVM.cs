using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DepthCharts.Queries.GetFullDepthChart
{
    public class DepthChartVM : IMapFrom<DepthChart>
    {
        public string GameType { get; set; }
        public Dictionary<string, List<int>> Chart { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DepthChart, DepthChartVM>()
                .ForMember(d => d.Chart, opt => opt.MapFrom(s => s.Chart.Where(c=> c.Value.Any())));
        }
    }
}