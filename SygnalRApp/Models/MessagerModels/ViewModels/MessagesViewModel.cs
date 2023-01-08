using System.Collections.Generic;

namespace SignalRApp.Models.MessagerModels.ViewModels
{
    /// <summary>
    /// Модель информации о переписке пользователя
    /// </summary>
    public class MessagesViewModel
    {
        /// <summary>
        /// Полное имя собеседника
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// Логин собеседника
        /// </summary>
        public string RecipientUserName { get; set; }

        /// <summary>
        /// Фото собеседника
        /// </summary>
        public string RecipientJpegPhoto { get; set; }

        /// <summary>
        /// Список писем
        /// </summary>
        public List<MessageModel> Messages { get; set; }
    }
}
