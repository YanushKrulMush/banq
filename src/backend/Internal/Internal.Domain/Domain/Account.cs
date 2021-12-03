using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internal.Domain
{
    public class Account
    {
        public int Number { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; }

        public DateTime OpenedOn { get; set; }
    }
}
