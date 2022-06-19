using Application.Common.Mappings;
using AutoMapper;

namespace Application.DepthCharts.Queries.GetPlayersUnderPlayerInDepthChart
{
    public class PlayerListVM : IMapFrom<List<int>>
    {
        public List<int> PlayerList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<List<int>, PlayerListVM>()
                .ForMember(d => d.PlayerList, opt => opt.MapFrom(s => s));
        }
    }
}