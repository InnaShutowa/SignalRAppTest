using System;
using System.Security.Claims;
using SignalRApp.Models.Users;

namespace SignalRApp.Services.Interfaces
{
    /// <summary>
    /// Сервис для операций над пользователями
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Получает идентификатор пользователя в системе
        /// </summary>
        /// <param name="claimsPrincipal">Авторизационные клеймы пользователя</param>
        /// <returns>Id в системе</returns>
        Guid? GetCurrentUserId(ClaimsPrincipal claimsPrincipal);

        /// <summary>
        /// Получает информацию о пользователе
        /// </summary>
        /// <param name="userId">Id пользователя в системе</param>
        /// <returns>Модель информации о пользователе</returns>
        UserModel GetUserInfo(Guid? userId);
    }
}
