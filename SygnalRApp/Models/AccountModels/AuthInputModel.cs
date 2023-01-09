namespace SignalRApp.Models.AccountModels
{
    /// <summary>
    /// Модель входных данных для авторизации пользователя
    /// </summary>
    public class AuthInputModel
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
