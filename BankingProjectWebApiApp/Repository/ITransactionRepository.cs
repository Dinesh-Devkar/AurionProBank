using BankingProjectWebApiApp.Dtos;
using BankingProjectWebApiApp.Model;
using System.Collections.Generic;
using System.IO;

namespace BankingProjectWebApiApp.Repository
{
    public interface ITransactionRepository
    {
        List<Transactions> GetAllTransactions(int accountId);
        Transactions GetTransaction(int accountId,int transactionId);
        void DoTransation(TransactionDto transactionDto,int accountId);

        void DownloadPassbook(int accountId);



    }
}
