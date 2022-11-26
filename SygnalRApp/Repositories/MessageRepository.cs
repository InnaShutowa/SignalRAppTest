using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NLog;
using SignalRApp.Entities;
using SignalRApp.Interfaces;

namespace SignalRApp.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
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
                _logger.Error($"Error in AddItem MessageRepository {ex.Message}. Input data {JsonConvert.SerializeObject(usr)}.");
                return null;
            }
        }

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
                _logger.Error($"Error in FoundItemByLogin MessageRepository with error {ex.Message}");
            }
        }

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
                _logger.Error($"Error in FoundItemByLogin MessageRepository with error {ex.Message}");
            }
            return result;
        }
    }
}
