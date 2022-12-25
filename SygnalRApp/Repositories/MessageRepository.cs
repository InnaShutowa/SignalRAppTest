using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NLog;
using SignalRApp.Entities;
using SignalRApp.Interfaces;

namespace SignalRApp.Repositories
{
    /// <inheritdoc cref="IMessageRepository" />
    public class MessageRepository : IMessageRepository
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <inheritdoc/>
        public Guid? AddItem(BaseEntity usr)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    var res = db.MessageEntities.Add(usr as MessageEntity);
                    db.SaveChanges();
                    return res.Entity.Id;
                }
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
                using (var db = new ApplicationContext())
                {
                    db.MessageEntities.Update(item as MessageEntity);
                    db.SaveChanges();
                }
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
                using (var db = new ApplicationContext())
                {
                    result = db.MessageEntities.Where(a => a.RecipientUserId == recipientUserId && a.AuthorUserId == authorUserId && !a.IsRead).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error {ex.Message}");
            }

            return result;
        }
    }
}
