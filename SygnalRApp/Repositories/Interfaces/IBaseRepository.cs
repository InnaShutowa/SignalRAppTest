using SignalRApp.Entities;

using System;

namespace SignalRApp.Repositories.Interfaces
{
    /// <summary>
    /// Базовый интерфейс для работы с бд
    /// </summary>
    public interface IBaseRepository
    {
        /// <summary>
        /// Обновление сущности
        /// </summary>
        void UpdateItem(BaseEntity item);

        /// <summary>
        /// Добавление сущности
        /// </summary>
        Guid? AddItem(BaseEntity usr);
    }
}
