using AutoMapper;
using Internal.Domain;

namespace Internal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionDto>();

            CreateMap<Account, AccountDto> ();

        }
    }
}