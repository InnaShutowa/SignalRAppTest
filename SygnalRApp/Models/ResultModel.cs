namespace SignalRApp.Models
{
    /// <summary>
    /// Модель результата работы метода
    /// </summary>
    public class ResultModel
    {
        public ResultModel(string error)
        {
            IsSuccess = false;
            Error = error;
        }

        public ResultModel(bool result)
        {
            IsSuccess = result;
        }

        /// <summary>
        /// Статус операции
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Ошибка, если есть
        /// </summary>
        public string Error { get; set; }
    }
}
