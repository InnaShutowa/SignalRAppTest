using System;
using System.ComponentModel.DataAnnotations;

namespace SignalRApp.Entities
{
    /// <summary>
    /// Базовый класс сущности, хранимой в БД
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Дата и время создания в UTC
        /// </summary>
        public DateTime CreateDateTimeUtc { get; set; }

        /// <summary>
        /// Дата и время последнего изменения данных сущности в UTC
        /// </summary>
        public DateTime ModifyDateTimeUtc { get; set; }

        protected BaseEntity()
        {
            ModifyDateTimeUtc = CreateDateTimeUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// Обновляет метку времени последнего изменения для объекта
        /// </summary>
        /// <remarks>
        /// Создана для того, что-бы для всех объектов метка времени обновлялась единообразно
        /// </remarks>
        public void UpdateModifiedTimestamp()
        {
            ModifyDateTimeUtc = DateTime.UtcNow;
        }
    }
}
