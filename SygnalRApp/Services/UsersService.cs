using System;
using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

using NLog;

using SignalRApp.Entities;
using SignalRApp.Models.Users;
using SignalRApp.Repositories.Interfaces;

namespace SignalRApp.Services
{
    /// <summary>
    /// Сервис для операций над пользователями
    /// </summary>
    public class UsersService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUserRepository _userRepository;
        private readonly UserManager<UserIdentity> _userManager;

        public UsersService(IUserRepository userRepository, UserManager<UserIdentity> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        /// <summary>
        /// Получает идентификатор пользователя в системе
        /// </summary>
        /// <param name="claimsPrincipal">Авторизационные клеймы пользователя</param>
        /// <returns>Id в системе</returns>
        public Guid? GetCurrentUserId(ClaimsPrincipal claimsPrincipal)
        {
            var userIdentity = _userManager.GetUserId(claimsPrincipal);

            return _userRepository.GetUserIdByIdentityId(userIdentity);
        }

        /// <summary>
        /// Получает информацию о пользователе
        /// </summary>
        /// <param name="userId">Id пользователя в системе</param>
        /// <returns>Модель информации о пользователе</returns>
        public UserModel GetUserInfo(Guid? userId)
        {
            var userInfo = _userRepository.FindItemByGuid(userId);

            if (userInfo != null)
            {
                return new UserModel
                {
                    Id = userInfo.Id,
                    JpegPhoto = userInfo.JpegPhoto,
                    UserName = userInfo.Login,
                    FullName = userInfo.FirstName + " " + userInfo.LastName,
                };
            }

            return null;
        }
    }
}
