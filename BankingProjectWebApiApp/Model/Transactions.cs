using System;

namespace BankingProjectWebApiApp.Model
{
    public class Transactions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int AccountId { get; set; }
    }
}
