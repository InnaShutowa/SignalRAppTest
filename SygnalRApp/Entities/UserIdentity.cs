using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace SignalRApp.Entities
{
    /// <summary>
    /// Сущность данных авторизации пользователя ASP.NET Identity
    /// </summary>
    public class UserIdentity : IdentityUser
    {
        /// <summary>
        /// Дата и время создания в UTC
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата и время последнего изменения данных сущности в UTC
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Коллекция клаймов пользователя
        /// </summary>
        public ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public UserIdentity() : base()
        {
            UpdatedAt = CreatedAt = DateTime.UtcNow;
        }
    }
}
