using System.Collections.Generic;

namespace SignalRApp.Models.MessagerModels.ViewModels
{
    /// <summary>
    /// Модель информации о пользователях и диалогах
    /// </summary>
    public class UsersViewModel
    {
        /// <summary>
        /// Список чатов
        /// </summary>
        public List<TredModel> TredModels { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }    
    }
}
