using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using SignalRApp.Models;
using SignalRApp.Models.AccountModels;
using SignalRApp.Repositories;
using SignalRApp.Services;

namespace SignalRApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Авторизация и получение jwt-токена
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("/token")]
        public async Task<ResultModel<AuthModel>> Token(string username, string password)
        {
            var authModel = await _accountService.LoginUser(new AuthInputModel
            {
                Login = username,
                Password = password
            });

            if (!authModel.IsSuccess)
            {
                return new ResultModel<AuthModel>(authModel.Error);
            }

            return authModel;
        }
    }
}
