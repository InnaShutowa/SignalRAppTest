using SignalRApp.Entities;
using System.Collections.Generic;

namespace SignalRApp.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        UserEntity FindItemByLogin(string login);
        List<UserEntity> GetAllUsers();

        void KickAllUsers();
        void UpdateUsersPhoto();
    }
}
