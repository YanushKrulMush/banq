namespace Internal.Domain
{
    public record AddTransactionDto
    {
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public string RecipientAccountNumber { get; set; }
        public int Amount { get; set; }
        public string Title { get; set; }
    }
}