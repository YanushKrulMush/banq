using AutoMapper;
using Internal.Domain;
using System;

namespace Internal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(x => x.TransactionType.ToString()))
                .ForMember(dest => dest.ReceiverOrSender, opt => opt.MapFrom(x => x.RecipientName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(x => $"{x.RecipientAccountNumber}\n{x.Title}" ));

            CreateMap<Account, AccountDto> ();

        }
    }
}