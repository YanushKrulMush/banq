using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Domain
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public decimal Ammount { get; set; }

        public string Currency { get; set; }

        public TransactionType TransactionType { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }

    }

    public enum TransactionType
    {
        IncomingTransfer,
        OutgoingTransfer
    }
}