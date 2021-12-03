using System;

namespace Internal.Domain
{
    public class Transaction
    {
        public int Id { get; set; }

        public decimal Ammount { get; set; }

        public string Currency { get; set; }

        public TransactionType TransactionType { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
    }

    public enum TransactionType
    {
        IncomingTransfer,
        OutgoingTransfer
    }
}