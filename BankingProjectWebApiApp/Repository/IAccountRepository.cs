using BankingProjectWebApiApp.Dtos;
using BankingProjectWebApiApp.Response;
using System.Collections.Generic;

namespace BankingProjectWebApiApp.Repository
{
    public interface IAccountRepository
    {
        List<AccountDto> GetAllAccounts();
        AccountDto GetAccount(int id);
        void AddAccount(AccountDtoAdd account);
        void UpdateAccount(int id,AccountDtoUpdate account);

        AccountLoginResponse AccountLogin(AccountLoginRequest account);
        void DeleteAccount(int id);
        void Register(AccountDtoAdd account);
    }
}
