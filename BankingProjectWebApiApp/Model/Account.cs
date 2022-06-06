using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankingProjectWebApiApp.Model
{
    public class Account
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        [Required]
        public string Name { get; set; }
        public double Balance { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Roll { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
