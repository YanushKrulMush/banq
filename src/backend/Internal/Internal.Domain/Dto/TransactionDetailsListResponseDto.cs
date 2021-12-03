using System.Collections.Generic;

namespace Internal.Domain
{
    public record TransactionDetailsListResponseDto
    {
        public IEnumerable<Transaction> Items { get; set; }
    }
}