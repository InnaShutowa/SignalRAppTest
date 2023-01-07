using Newtonsoft.Json;

using NLog;

using SignalRApp.Entities;
using SignalRApp.Repositories.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalRApp.Repositories
{
    /// <inheritdoc cref="IUserRepository" />
    public class UserRepository : IUserRepository
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ApplicationContext _db;
        public UserRepository(ApplicationContext db)
        {
            _db = db;
        }

        /// <inheritdoc/>
        public Guid? AddItem(BaseEntity usr)
        {
            try
            {
                var res = _db.UserEntities.Add(usr as UserEntity);
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
                _db.UserEntities.Update(item as UserEntity);
                _db.SaveChanges();
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
                var usr = _db.UserEntities.Where(a => a.Login.ToLower() == login
                    || a.EmailPrimary.ToLower() == login)
                    .FirstOrDefault();

                return usr;
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
                result = _db.UserEntities.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error {ex.Message}.");
            }

            return result;
        }
    }
}
