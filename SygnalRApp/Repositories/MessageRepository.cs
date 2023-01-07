using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using NLog;

using SignalRApp.Entities;
using SignalRApp.Repositories.Interfaces;

namespace SignalRApp.Repositories
{
    /// <inheritdoc cref="IMessageRepository" />
    public class MessageRepository : IMessageRepository
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ApplicationContext _db;

        public MessageRepository(ApplicationContext db)
        {
            _db = db;
        }

        /// <inheritdoc/>
        public Guid? AddItem(BaseEntity usr)
        {
            try
            {
                var res = _db.MessageEntities.Add(usr as MessageEntity);
                _db.SaveChanges();

                return res.Entity.Id;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error {ex.Message}. Input data {JsonConvert.SerializeObject(usr)}.");

                return null;
            }
        }

        /// <inheritdoc/>
        public void UpdateItem(BaseEntity item)
        {
            try
            {
                _db.MessageEntities.Update(item as MessageEntity);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public List<MessageEntity> GetUnreadMessages(Guid recipientUserId, Guid authorUserId)
        {
            var result = new List<MessageEntity>();
            try
            {
                result = _db.MessageEntities.Where(a => a.RecipientUserId == recipientUserId && a.AuthorUserId == authorUserId && !a.IsRead).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error {ex.Message}");
            }

            return result;
        }
    }
}
