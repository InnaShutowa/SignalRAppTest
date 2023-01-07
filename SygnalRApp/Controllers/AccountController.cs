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
        /// Авторизация
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("/token")]
        public async Task<IActionResult> Token(string username, string password)
        {
            var authModel = await _accountService.LoginUser(new AuthInputModel
            {
                Login = username,
                Password = password
            });

            if (!authModel.IsSuccess)
            {
                return View(authModel.Error);
            }

            return RedirectToAction(nameof(MessengerController.Users), "Messenger");
        }

        /// <summary>
        /// Выход из аккаунта
        /// </summary>
        /// <returns></returns>
        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutUser();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
