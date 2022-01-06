using System.Collections.Generic;

namespace Internal.Domain
{
    public record TransactionDetailsListResponseDto
    {
        public IEnumerable<TransactionDto> Items { get; set; }
    }
}