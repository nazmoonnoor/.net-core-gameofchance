using AutoMapper;

namespace GameOfChance.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Domain.Player, Client.Player>()
                .ReverseMap();
            CreateMap<Core.Domain.Bet, Client.Bet>()
                .ReverseMap();
        }
    }
}
