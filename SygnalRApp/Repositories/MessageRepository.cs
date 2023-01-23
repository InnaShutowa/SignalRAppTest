using System;
using System.Collections.Generic;
using System.Linq;
using SignalRApp.Entities;
using SignalRApp.Repositories.Interfaces;

namespace SignalRApp.Repositories
{
    /// <inheritdoc cref="IMessageRepository" />
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationContext _db;

        public MessageRepository(ApplicationContext db)
        {
            _db = db;
        }

        /// <inheritdoc/>
        public BaseEntity AddItem(BaseEntity usr)
        {
            var res = _db.MessageEntities.Add(usr as MessageEntity);
            _db.SaveChanges();

            return res.Entity;
        }

        /// <inheritdoc/>
        public void UpdateItem(BaseEntity item)
        {
            _db.MessageEntities.Update(item as MessageEntity);
            _db.SaveChanges();
        }

        /// <inheritdoc/>
        public int GetUnreadMessages(Guid recipientUserId, Guid authorUserId)
        {
            return _db.MessageEntities
                .Where(a => a.RecipientUserId == recipientUserId)
                .Where(a => a.AuthorUserId == authorUserId)
                .Where(a => !a.IsRead)
                .Count();
        }

        /// <inheritdoc/>
        public IEnumerable<MessageEntity> GetUsersTred(Guid recipientUserId, Guid authorUserId)
        {
            return _db.MessageEntities
                .Where(a => a.RecipientUserId == recipientUserId && a.AuthorUserId == authorUserId
                    || a.RecipientUserId == authorUserId && a.AuthorUserId == recipientUserId)
                .ToList();
        }
    }
}
