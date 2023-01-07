using System;

namespace SignalRApp.Models.AccountModels
{
    /// <summary>
    /// Модель с данными для авторизованного пользователя
    /// </summary>
    public class AuthModel
    {
        public AuthModel(string accessToken, string username, Guid userId)
        {
            AccessToken = accessToken;
            Username = username;
            UserId = userId;
        }
        /// <summary>
        /// Токен доступа
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Id пользователя в системе
        /// </summary>
        public Guid UserId { get; set; }
    }
}
