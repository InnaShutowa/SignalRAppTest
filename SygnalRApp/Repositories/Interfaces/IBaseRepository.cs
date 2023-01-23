using SignalRApp.Entities;

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
        BaseEntity AddItem(BaseEntity usr);
    }
}
