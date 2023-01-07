using SignalRApp.Extensions;
using System;

namespace SignalRApp.Entities
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    public class UserEntity : BaseEntity
    {
        public UserEntity(string firstName, string lastName, string login, string emailPrimary, string? jpegPhoto)
        {
            Id = Guid.NewGuid();
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            JpegPhoto = jpegPhoto;
            EmailPrimary = emailPrimary;
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
        public string? JpegPhoto { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string EmailPrimary { get; set; }

        /// <summary>
        /// Хеш для проверки нужно ли обновлять данные
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// ID сущности данных авторизации ASP.NET Identity
        /// </summary>
        public string IdentityId { get; set; }

        /// <summary>
        /// Данные авторизации пользователя ASP.NET Identity
        /// </summary>
        public UserIdentity Identity { get; set; }
    }
}
