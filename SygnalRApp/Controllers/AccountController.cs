﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalRApp.Models.AccountModels;
using SignalRApp.Services.Interfaces;

namespace SignalRApp.Controllers
{
    /// <summary>
    /// Контроллер для действий с аккаунтом - авторизация, лог аут
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Редирект на главную, если юзер не авторизован
        /// </summary>
        /// <returns>Редирект на главную</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns>Представление чата</returns>
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
        /// <returns>Редирект на главную</returns>
        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutUser();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
