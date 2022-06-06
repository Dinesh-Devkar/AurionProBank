using BankingProjectWebApiApp.Dtos;
using BankingProjectWebApiApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BankingProjectWebApiApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private IAccountRepository _accountRepository;
        public AccountController(IAccountRepository repository)
        {
            _accountRepository=repository;
        }

        [HttpGet]
        [Route("GetAllAccounts")]
        [AllowAnonymous]
        public List<AccountDto> GetAllAccounts()
        {
            return _accountRepository.GetAllAccounts();
        }
        [HttpGet]
        [Route("{accountId}/GetAccount")]
        [Authorize]
        public AccountDto GetAccount(int accountId)
        {
            return _accountRepository.GetAccount(accountId);
        }
        [HttpPost]
        [Route("AddAccount")]
        [Authorize]
        public IActionResult AddAccount(AccountDtoAdd account)
        {
            _accountRepository.AddAccount(account);
            return this.Ok("Account Added Successfully");
        }
        [HttpPut]
        [Route("{id}/UpdateAccount")]
        [Authorize]
        public IActionResult UpdateAccount(int id,AccountDtoUpdate account)
        {
            _accountRepository.UpdateAccount(id,account);
            return this.Ok("Account Updated Successfully");
        }
        [HttpDelete]
        [Route("{id}/DeleteAccount")]
        [Authorize]
        public IActionResult DeleteAccount(int id)
        {
            _accountRepository.DeleteAccount(id);
            return this.Ok("Account Delete Succesffully");
        }

        [HttpPost]
        [Route("LoginAccount")]
        [AllowAnonymous]
        public IActionResult LoginAccont(AccountLoginRequest account)
        {
            return this.Ok(_accountRepository.AccountLogin(account));
        }
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public IActionResult Register(AccountDtoAdd account)
        {
            _accountRepository.Register(account);
            return this.Ok("Account Added Successfully");
        }
    }
}
