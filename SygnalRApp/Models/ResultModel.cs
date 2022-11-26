using Newtonsoft.Json;
using System;

namespace SignalRApp.Models
{
    public class ResultModel<T>
    {
        public ResultModel(string error)
        {
            IsSuccess = false;
            Error = error;
        }
        public ResultModel(T authModel)
        {
            Data = authModel;
            IsSuccess = true;
        }

        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }
    }
}
