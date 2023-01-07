using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SignalRApp.Extensions
{
    /// <summary>
    /// Содержит методы расширения для перечислений
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Получает отображаемое значение
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        /// <param name="value">Значение.</param>
        /// <returns>Отображаемое значение или null, если значение null.</returns>
        public static string? GetDisplayName<T>(this T? value) where T : struct, Enum
        {
            var valueString = value?.ToString();
            if (valueString == null)
            {
                return null;
            }

            return typeof(T)
                .GetMember(valueString)
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?.Name ?? valueString;
        }
    }
}
