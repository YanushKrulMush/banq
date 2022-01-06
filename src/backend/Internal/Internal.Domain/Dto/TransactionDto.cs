using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internal.Domain
{
    public record TransactionDto
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string TransactionType { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
    }
}
