using SignalRApp.Entities;
using System.Collections.Generic;

namespace SignalRApp.Interfaces
{
    /// <summary>
    /// Интерфейс для действий с пользователями
    /// </summary>
    public interface IUserRepository : IBaseRepository
    {
        /// <summary>
        /// Поиск пользователя по email или логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Сущность пользователя</returns>
        public UserEntity FindItemByLoginOrEmail(string login);

        /// <summary>
        /// Метод для получения списка пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        public List<UserEntity> GetAllUsers();
    }
}
