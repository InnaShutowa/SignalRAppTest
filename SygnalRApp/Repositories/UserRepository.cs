using Newtonsoft.Json;
using NLog;
using SignalRApp.Entities;
using SignalRApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalRApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
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
                _logger.Error($"Error in AddItem UserRepository {ex.Message}. Input data {JsonConvert.SerializeObject(usr)}.");
                return null;
            }
        }

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
                _logger.Error($"Error in FoundItemByLogin UserRepository with error {ex.Message}");
            }
        }

        public UserEntity FindItemByLogin(string login)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    var usr = db.UserEntities.Where(a => a.Login == login).FirstOrDefault();
                    return usr;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in FoundItemByLogin UserRepository with error {ex.Message}. Login {login}");
                return null;
            }
        }

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
                _logger.Error($"Error in FoundItemByLogin UserRepository with error {ex.Message}.");
            }

            return result;
        }

        public void KickAllUsers()
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    var result = db.UserEntities.ToList();
                    result.ForEach(a =>
                    {
                        db.UserEntities.Remove(a);
                    });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in KickAllUsers UserRepository with error {ex.Message}.");
            }
        }

        public void UpdateUsersPhoto()
        {
            try
            {
                var photos = new List<string>() {
                    "https://tn.fishki.net/26/upload/post/2020/07/13/3368570/5b0715f3cac9187359d6e0ba3ee929ff.jpg",
                    "https://www.meme-arsenal.com/memes/793c7f4fb01605b354492dbe03fbd700.jpg",
                    "https://www.my-sfinks.ru/photo/img/strashnyi-sfinks-1.jpg",
                    "https://bugaga.ru/uploads/posts/2020-03/1585333100_dzherdan-1.jpg",
                    "https://aroundpet.ru/wp-content/uploads/kakoi-samyi-tolstyi-kot-v-mire.jpg",
                    "https://www.meme-arsenal.com/memes/d0ffca74b1e3478678a4afe141814c64.jpg" };
                using (var db = new ApplicationContext())
                {
                    var users = db.UserEntities.ToList();
                    users.ForEach(usr =>
                    {
                        var rnd = new Random();
                        usr.JpegPhoto = photos[rnd.Next(0, photos.Count - 1)];
                        db.UserEntities.Update(usr);
                    });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
