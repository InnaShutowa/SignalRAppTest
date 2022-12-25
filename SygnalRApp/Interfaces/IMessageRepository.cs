using System;
using System.Collections.Generic;
using SignalRApp.Entities;

namespace SignalRApp.Interfaces
{
    /// <summary>
    /// Интерфейс для действий с сообщениями
    /// </summary>
    public interface IMessageRepository : IBaseRepository
    {
        /// <summary>
        /// Метод для получения непрочитанных сообщений
        /// </summary>
        /// <param name="recipientUserId">Id получателя</param>
        /// <param name="authorUserId">Id автора</param>
        /// <returns>Список сообщений</returns>
        List<MessageEntity> GetUnreadMessages(Guid recipientUserId, Guid authorUserId);
    }
}
