using System;
using System.Collections.Generic;
using SignalRApp.Entities;

namespace SignalRApp.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс для действий с сообщениями
    /// </summary>
    public interface IMessageRepository : IBaseRepository
    {
        /// <summary>
        /// Метод для получения количества непрочитанных сообщений
        /// </summary>
        /// <param name="recipientUserId">Id получателя</param>
        /// <param name="authorUserId">Id автора</param>
        /// <returns>Список сообщений</returns>
        int GetUnreadMessages(Guid recipientUserId, Guid authorUserId);

        /// <summary>
        /// Метод для получения количества непрочитанных сообщений
        /// </summary>
        /// <param name="recipientUserId">Id получателя</param>
        /// <param name="authorUserId">Id автора</param>
        /// <returns>Список сообщений</returns>
        IEnumerable<MessageEntity> GetUsersTred(Guid recipientUserId, Guid authorUserId);
    }
}
