using System;

namespace SignalRApp.Extensions
{
    /// <summary>
    /// Расширение для работы с DateTime
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// Возвращает время или дату для отображения в чате
        /// </summary>
        /// <param name="dateTime">Дата и время</param>
        /// <returns>Дата и время в формате строки</returns>
        public static string ToChatString(this DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime) == DateOnly.FromDateTime(DateTime.UtcNow)
                            ? dateTime.ToString("t")
                            : dateTime.ToString("f");
        }
    }
}
