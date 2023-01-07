using System.Collections.Generic;

namespace SignalRApp.Models.MessagerModels.ViewModels
{
    /// <summary>
    /// Модель информации о переписке пользователя
    /// </summary>
    public class MessagesViewModel
    {
        /// <summary>
        /// Имя собеседника
        /// </summary>
        public string RecipientName { get; set; }

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
