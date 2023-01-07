
namespace SignalRApp.Models
{
    /// <summary>
    /// Обобщенная модель результата работы метода
    /// </summary>
    public class ResultDataModel<T>
    {
        public ResultDataModel(string error)
        {
            IsSuccess = false;
            Error = error;
        }
        public ResultDataModel(T data)
        {
            Data = data;
            IsSuccess = true;
        }

        /// <summary>
        /// Статус операции
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Ошибка, если есть
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Возвращаемые данные универсального типа
        /// </summary>
        public T Data { get; set; }
    }
}
