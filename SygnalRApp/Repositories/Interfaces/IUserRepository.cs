using SignalRApp.Entities;

using System;
using System.Collections.Generic;

namespace SignalRApp.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс для действий с пользователями
    /// </summary>
    public interface IUserRepository : IBaseRepository
    {
        /// <summary>
        /// Поиск пользователя по email или логину
        /// </summary>
        /// <param name="login">Имя пользователя или email</param>
        /// <returns>Сущность пользователя</returns>
        public UserEntity GetItemByLoginOrEmail(string login);

        /// <summary>
        /// Метод для получения списка пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        public List<UserEntity> GetAllUsers();

        /// <summary>
        /// Получает id пользователя в системе
        /// </summary>
        /// <param name="identityId">Identity id</param>
        /// <returns></returns>
        public Guid? GetUserIdByIdentityId(string identityId);

        /// <summary>
        /// Поиск пользователя по id
        /// </summary>
        /// <param name="id">Id пользователя в системе</param>
        /// <returns>Сущность пользователя</returns>
        public UserEntity GetItemByGuid(Guid? id);
    }
}
