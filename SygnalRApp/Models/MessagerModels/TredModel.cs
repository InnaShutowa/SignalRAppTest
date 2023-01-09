using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApp.Models.MessagerModels
{
    /// <summary>
    /// Модель чата
    /// </summary>
    public class TredModel
    {
        public TredModel(Guid userId, string userName, string photo, int messageCount)
        {
            CompanionUserId = userId;
            CompanionUsername = userName;
            UnreadMessagesCount = messageCount;
            CompanionJpegPhoto = photo;
        }

        /// <summary>
        /// Количество непрочитанных сообщений 
        /// </summary>
        public int UnreadMessagesCount { get; set; }

        /// <summary>
        /// Id возможного собеседника
        /// </summary>
        public Guid CompanionUserId { get; set; }

        /// <summary>
        /// Имя возможного собеседника
        /// </summary>
        public string CompanionUsername { get; set; }

        /// <summary>
        /// Фото возможного собеседника
        /// </summary>
        public string CompanionJpegPhoto { get; set; }
    }
}
