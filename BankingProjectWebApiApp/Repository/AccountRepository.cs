using BankingProjectWebApiApp.Dtos;
using BankingProjectWebApiApp.Infrastructure;
using BankingProjectWebApiApp.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using BankingProjectWebApiApp.Response;

namespace BankingProjectWebApiApp.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankDbContext _bankDb;
        public AccountRepository(BankDbContext dbContext)
        {
            this._bankDb = dbContext;
        }
        public void AddAccount(AccountDtoAdd account)
        {
            if(account != null)
            {
                this._bankDb.Accounts.Add(new Account()
                {
                    AccountNumber=account.AccountNumber,
                    Balance=account.Balance,
                    Name=account.Name,
                    Roll=account.Roll,
                    Password=account.Password

                });
                this._bankDb.SaveChanges();
            }
        }

        public void DeleteAccount(int id)
        {
            var account=this._bankDb.Accounts.ToList().Find(a=>a.Id==id);
            if (account != null)
            {
                this._bankDb.Entry(account).State = EntityState.Deleted;
                this._bankDb.SaveChanges();
            }

        }

        public AccountDto GetAccount(int id)
        {
            var account = this._bankDb.Accounts.ToList().Find(a => a.Id == id);
            if (account != null)
            {
                var accountDto = new AccountDto()
                {
                    AccountNumber = account.AccountNumber,
                    Balance = account.Balance,
                    Name = account.Name,
                    Roll = account.Roll

                };
                return accountDto;
            }
            return null;
        }

        public List<AccountDto> GetAllAccounts()
        {
            var accountDtoList = new List<AccountDto>();
            var accounts = this._bankDb.Accounts.ToList();
            foreach(var account in accounts)
            {
                accountDtoList.Add(new AccountDto()
                {
                    AccountNumber=account.AccountNumber,
                    Name=account.Name,
                    Balance=account.Balance,
                    Roll=account.Roll
                });
            }
            return accountDtoList;
        }

        public void UpdateAccount(int id, AccountDtoUpdate updateAccount)
        {
            var account = this._bankDb.Accounts.ToList().Find(a => a.Id == id);
            if (account != null)
            {
                account.Name = updateAccount.Name;
                account.AccountNumber=updateAccount.AccountNumber;
                account.Roll=updateAccount.Roll;
                this._bankDb.Entry(account).State = EntityState.Modified;
                this._bankDb.SaveChanges();
            }
            
        }

        public AccountLoginResponse AccountLogin(AccountLoginRequest account)
        {
            var response = new AccountLoginResponse();
            var credentials=this._bankDb.Accounts.ToList().Find(x=>x.Name==account.UserName && x.Password==account.Password);
            if (credentials != null)
            {
                response.Name = credentials.Name;
                response.IsSuccess = true;
                response.Message = "Login Successfull";
                response.Roll = credentials.Roll;
                response.AccountNumber = credentials.AccountNumber;
                response.Token = GenerateJWT(credentials.Name, credentials.Roll);
                return response;
            }
            response.Message = "Login Fail";
            response.IsSuccess = false;
            return response;
        }
        public void Register(AccountDtoAdd accountDto)
        {
            Debug.WriteLine("Inside Register Method");
            if (accountDto != null)
            {
                Debug.WriteLine("Inside If condition");
                using (this._bankDb)
                {
                    using(var transaction = this._bankDb.Database.BeginTransaction())
                    {
                        Debug.WriteLine("Inside Transaction");
                        try
                        {
                            var account = new Account()
                            {
                                AccountNumber = accountDto.AccountNumber,
                                Balance = accountDto.Balance,
                                Name = accountDto.Name,
                                Roll = accountDto.Roll,
                                Password = accountDto.Password

                            };
                            this._bankDb.Accounts.Add(account);
                            Debug.WriteLine("Account Added");
                            if (this._bankDb.SaveChanges() > 0)
                            {
                                this._bankDb.Transactions.Add(new Transaction()
                                {
                                    AccountId = account.Id,
                                    Name = account.Name,
                                    Balance = account.Balance,
                                    TransactionDate = DateTime.Now,
                                    TransactionType = TransactionType.DEPOSIT.ToString()
                                }) ;
                                this._bankDb.SaveChanges();
                                transaction.Commit();
                                Debug.WriteLine("Transaction commit");

                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Exception Occure");
                            Debug.WriteLine(ex.Message);
                            transaction.Rollback();
                        }
                    }
                }
                
            }
        }
        public string GenerateJWT(string UserName, string Role)
        {
         
            string key = "ThisismySecretKey";
            var issuer = "*";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //claim is used to add identity to JWT token
            var claims = new[] {
         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
         new Claim(JwtRegisteredClaimNames.Sid, UserName),
         new Claim(ClaimTypes.Role,Role),
         new Claim("Date", DateTime.Now.ToString()),
         };

            var token = new JwtSecurityToken(issuer,
              "*",
              claims,    //null original value
              expires: DateTime.Now.AddMinutes(120),

              //notBefore:
              signingCredentials: credentials);

            string Data = new JwtSecurityTokenHandler().WriteToken(token); //return access token 
            return Data;
        }
    }
}
