using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApp.Models.MessagerModels
{
    /// <summary>
    /// Модель сообщения в чате
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Исходящее ли сообщение
        /// </summary>
        public bool IsOutgoing { get; set; }

        /// <summary>
        /// Дата и время отправки
        /// </summary>
        public DateTime SendDate { get; set; }
    }
}
