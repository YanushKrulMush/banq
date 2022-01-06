using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Domain
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Number { get; set; }

        public double Balance { get; set; }

        public string Currency { get; set; }

        public DateTime OpenedOn { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}