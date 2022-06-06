using BankingProjectWebApiApp.Dtos;
using BankingProjectWebApiApp.Infrastructure;
using BankingProjectWebApiApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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



        public void DoTransation(TransactionDto transactionDto, int accountId)
        {
            using (this._bankDb)
            {
                using (var transaction = this._bankDb.Database.BeginTransaction())
                {
                    try
                    {
                        var account = this._bankDb.Accounts.ToList().Find(x => x.Id == accountId);
                        if (account != null)
                        {
                            if (transactionDto.TransactionType == TransactionType.DEPOSIT.ToString())
                            {
                                account.Balance += transactionDto.Amount;
                            }
                            else if ((transactionDto.TransactionType == TransactionType.DEPOSIT.ToString()))
                            {
                                account.Balance -= transactionDto.Amount;
                            }
                            this._bankDb.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            if (this._bankDb.SaveChanges() > 0)
                            {
                                this._bankDb.Transactions.Add(new Transactions()
                                {
                                    AccountId = account.Id,
                                    Balance = transactionDto.Amount,
                                    Name = account.Name,
                                    TransactionDate = System.DateTime.Now,
                                    TransactionType = transactionDto.TransactionType
                                });
                                this._bankDb.SaveChanges();
                                transaction.Commit();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Debug.WriteLine(ex.Message);
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
