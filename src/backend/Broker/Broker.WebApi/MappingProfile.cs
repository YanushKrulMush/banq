using AutoMapper;
using Broker.Domain;

namespace Broker
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountStock, StockDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Stock.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(x => x.Stock.Value))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Stock.Id))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(x => x.Stock.Value * x.Quantity))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(x => x.Quantity));

            CreateMap<Account, AccountDto> ();

        }
    }
}