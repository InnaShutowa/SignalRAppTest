using Microsoft.AspNetCore.Identity;

namespace SignalRApp.Entities
{
    /// <summary>
    /// Сущность данных роли пользователя ASP.NET Identity
    /// </summary>
    public class RoleIdentity : IdentityRole
    {
        public RoleIdentity() : base() { }

        /// <summary>
        /// Название роли для вывода на странице
        /// </summary>
        public string DisplayName { get; set; }
    }
}
