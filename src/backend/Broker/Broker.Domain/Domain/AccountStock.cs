using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broker.Domain
{
    public class AccountStock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Account Account { get; set; }
        public int AccountId { get; set; }
        public Stock Stock { get; set; }
        public int StockId { get; set; }

        public int Quantity { get; set; }
    }
}
