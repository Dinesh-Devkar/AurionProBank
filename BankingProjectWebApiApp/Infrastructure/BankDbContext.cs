
using BankingProjectWebApiApp.Model;
using Microsoft.EntityFrameworkCore;

namespace BankingProjectWebApiApp.Infrastructure
{
    public class BankDbContext:DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
