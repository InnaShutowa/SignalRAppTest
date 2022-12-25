using Newtonsoft.Json;
using NLog;
using SignalRApp.Entities;
using SignalRApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalRApp.Repositories
{
    /// <inheritdoc cref="IUserRepository" />
    public class UserRepository : IUserRepository
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <inheritdoc/>
        public Guid? AddItem(BaseEntity usr)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    var res = db.UserEntities.Add(usr as UserEntity);
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
                    db.UserEntities.Update(item as UserEntity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public UserEntity? FindItemByLoginOrEmail(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return null;
            }
            login = login.ToLower().Trim();

            try
            {
                using (var db = new ApplicationContext())
                {
                    var usr = db.UserEntities.Where(a => a.Login.ToLower() == login
                        || a.EmailPrimary.ToLower() == login)
                        .FirstOrDefault();

                    return usr;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error {ex.Message}. Login {login}");
                return null;
            }
        }

        /// <inheritdoc/>
        public List<UserEntity> GetAllUsers()
        {
            var result = new List<UserEntity>();
            try
            {
                using (var db = new ApplicationContext())
                {
                    result = db.UserEntities.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error {ex.Message}.");
            }

            return result;
        }
    }
}
