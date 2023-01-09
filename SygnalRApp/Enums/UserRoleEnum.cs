using System.ComponentModel.DataAnnotations;

namespace SignalRApp.Enums
{
    /// <summary>
    /// Роль пользователя приложения
    /// </summary>
    public enum UserRoleEnum
    {
        /// <summary>
        /// Администратор
        /// </summary>
        [Display(Name = "Администратор")]
        Administrator = 1,

        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        User = 2
    }
}
