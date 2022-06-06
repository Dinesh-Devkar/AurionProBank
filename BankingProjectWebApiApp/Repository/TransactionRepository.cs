using BankingProjectWebApiApp.Dtos;
using BankingProjectWebApiApp.Infrastructure;
using BankingProjectWebApiApp.Model;
using System.Collections.Generic;
using System.IO;
using System.Transactions;

namespace BankingProjectWebApiApp.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private BankDbContext _bankDb;
        public TransactionRepository(BankDbContext dbContext)
        {
            this._bankDb = dbContext;
        }
        public void DoTransation(TransactionDto transactionDto,AccountDto accountDto)
        {
            using (this._bankDb)
            {
                using(var transaction = this._bankDb.Database.BeginTransaction())
                {
                    if (transactionDto.TransactionType == TransactionType.DEPOSIT.ToString())
                    {
                        //var account=this._bankDb the mode of arrangement of sepal or petal just 
                    }
                }
            }
        }

        public void DownloadPassbook(int accountId)
        {
            throw new System.NotImplementedException();
        }

        public List<Transactions> GetAllTransactions(int accountId)
        {
            throw new System.NotImplementedException();
        }

        public Transactions GetTransaction(int accountId, int transactionId)
        {
            throw new System.NotImplementedException();
        }

       
    }
}
