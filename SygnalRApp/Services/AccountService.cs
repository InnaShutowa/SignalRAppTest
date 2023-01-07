using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog;
using SignalRApp.Entities;
using SignalRApp.Enums;
using SignalRApp.Extensions;
using SignalRApp.Models;
using SignalRApp.Models.AccountModels;
using SignalRApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignalRApp.Services
{
    /// <summary>
    /// Сервис для работы с аккаунтом
    /// </summary>
    public class AccountService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUserRepository _userRepository;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;

        public AccountService(IUserRepository userRepository,
            UserManager<UserIdentity> userManager,
            SignInManager<UserIdentity> signInManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Получает идентификатор пользователя в системе
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public Guid? GetCurrentUserId(ClaimsPrincipal claimsPrincipal)
        {
            var userIdentity = _userManager.GetUserId(claimsPrincipal);

            return _userRepository.GetUserIdByIdentityId(userIdentity);
        }

        /// <summary>
        /// Метод для выхода из приложения
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }

        /// <summary>
        /// Метод для авторизации пользователя
        /// </summary>
        /// <param name="model">Модель с авторизационными данными</param>
        /// <returns>Модель ответа с результатами авторизации</returns>
        public async Task<ResultModel> LoginUser(AuthInputModel model)
        {
            model.Login = model.Login.Trim();
            model.Password = model.Password?.Trim();

            var userIdentity = await _userManager.FindByEmailAsync(model.Login);
            if (userIdentity == null)
            {
                return new ResultModel("Пользователь не найден.");
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(userIdentity, model.Password);
            if (!isValidPassword)
            {
                return new ResultModel("Неверный пароль.");
            }

            await _signInManager.SignOutAsync();

            var signInResult = await _signInManager.PasswordSignInAsync(userIdentity.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (!signInResult.Succeeded)
            {
                return new ResultModel("Не удалось войти на сайт. Попробуйте повторить позднее.");
            }

            /// логика получения wt токена для авторизации через API, сейчас на уровне MVC не используется
            //var claims = await _userManager.GetClaimsAsync(userIdentity);
            //var jwt = _getJwtToken(claims);

            //var user = _userRepository.FindItemByLoginOrEmail(model.Login);

            return new ResultModel(true);

        }

        /// <summary>
        /// Метод для получения jwt-токена
        /// </summary>
        /// <param name="claims">Claims для данного пользователя</param>
        /// <returns>Токен авторизации</returns>
        [Obsolete]
        private string _getJwtToken(IEnumerable<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                   issuer: AuthOptions.ISSUER,
                   audience: AuthOptions.AUDIENCE,
                   notBefore: DateTime.UtcNow,
                   claims: claims,
                   signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
