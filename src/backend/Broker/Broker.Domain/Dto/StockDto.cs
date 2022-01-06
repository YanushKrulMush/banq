using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broker.Domain
{
    public record StockDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Value { get; set; }

        public int Quantity { get; set; }

        public int Total { get; set; }
    }
}
