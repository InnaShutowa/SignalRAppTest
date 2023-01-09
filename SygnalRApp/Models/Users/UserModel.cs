using System;

namespace SignalRApp.Models.Users
{
    /// <summary>
    /// Модель с данными пользователя
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Id пользователя в системе
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Полное имя пользователя
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Ссылка на фото
        /// </summary>
        public string JpegPhoto { get; set; }
    }
}
