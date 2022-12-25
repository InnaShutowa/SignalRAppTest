using SignalRApp.Extensions;
using System;

namespace SignalRApp.Entities
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    public class UserEntity : BaseEntity
    {
        public UserEntity(string firstName, string lastName, string login, string email, string photo)
        {
            Id = Guid.NewGuid();
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            JpegPhoto = photo;
            EmailPrimary = email;
            Hash = this.GenerateStateHash();
        }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Фото
        /// </summary>
        public string JpegPhoto { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string EmailPrimary { get; set; }

        /// <summary>
        /// Хеш для проверки нужно ли обновлять данные
        /// </summary>
        public string Hash { get; set; }
    }
}
