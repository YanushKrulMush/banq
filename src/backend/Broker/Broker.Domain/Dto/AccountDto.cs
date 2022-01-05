using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broker.Domain
{
    public record AccountDto
    {
        public string Number { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; }

        public DateTime OpenedOn { get; set; }
    }
}
