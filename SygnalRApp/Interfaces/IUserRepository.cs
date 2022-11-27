using SignalRApp.Entities;
using System.Collections.Generic;

namespace SignalRApp.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        public UserEntity FindItemByLoginOrEmail(string login);
        public List<UserEntity> GetAllUsers();

        public void KickAllUsers();
        public void UpdateUsersPhoto();
    }
}
