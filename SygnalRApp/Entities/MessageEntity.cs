using System;

namespace SignalRApp.Entities
{
    /// <summary>
    /// Сущность сообщения
    /// </summary>
    public class MessageEntity : BaseEntity
    {
        /// <summary>
        /// Id отправителя
        /// </summary>
        public Guid AuthorUserId { get; set; }

        /// <summary>
        /// Id получателя
        /// </summary>
        public Guid RecipientUserId { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Прочитано или нет
        /// </summary>
        public bool IsRead { get; set; }

    }
}
