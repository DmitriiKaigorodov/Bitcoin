namespace Bitcoin.Core.Models
{
    public class TransactionDetails
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Account { get; set; }
        public double Amount { get; set; }   
        public string TransactionTxId { get; set; }
        public Transaction Transaction { get; set; }
        public TransactionCategories Category { get; set; }    
    }
}