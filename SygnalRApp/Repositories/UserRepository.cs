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
        private readonly ApplicationContext _db;
        public UserRepository(ApplicationContext db)
        {
            _db = db;
        }

        /// <inheritdoc/>
        public BaseEntity AddItem(BaseEntity usr)
        {
            var res = _db.UserEntities.Add(usr as UserEntity);
            _db.SaveChanges();

            return res.Entity;
        }

        /// <inheritdoc/>
        public void UpdateItem(BaseEntity item)
        {
            _db.UserEntities.Update(item as UserEntity);
            _db.SaveChanges();
        }

        /// <inheritdoc/>
        public UserEntity GetItemByLoginOrEmail(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return null;
            }
            login = login.ToLower().Trim();

            return _db.UserEntities.Where(a => a.Login.ToLower() == login
                || a.EmailPrimary.ToLower() == login)
                .FirstOrDefault();
        }

        /// <inheritdoc/>
        public List<UserEntity> GetAllUsers()
        {
            return _db.UserEntities.ToList();
        }

        /// <inheritdoc/>
        public Guid? GetUserIdByIdentityId(string identityId)
        {
            return _db.UserEntities
                .FirstOrDefault(l => l.IdentityId == identityId)?.Id;
        }

        /// <inheritdoc/>
        public UserEntity GetItemByGuid(Guid? id)
        {
            var usr = _db.UserEntities.Where(a => a.Id == id)
                .FirstOrDefault();

            return usr;
        }
    }
}
