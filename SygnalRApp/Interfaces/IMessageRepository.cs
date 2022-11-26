using System;
using System.Collections.Generic;
using SignalRApp.Entities;

namespace SignalRApp.Interfaces
{
    public interface IMessageRepository : IBaseRepository
    {
        List<MessageEntity> GetUnreadMessages(Guid recipientUserId, Guid authorUserId);
    }
}
