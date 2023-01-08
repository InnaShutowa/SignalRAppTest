using System.Threading.Tasks;

using SignalRApp.Models;
using SignalRApp.Models.AccountModels;

namespace SignalRApp.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с аккаунтом
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Метод для выхода из приложения
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task LogoutUser();

        /// <summary>
        /// Метод для авторизации пользователя
        /// </summary>
        /// <param name="model">Модель с авторизационными данными</param>
        /// <returns>Модель ответа с результатами авторизации</returns>
        Task<ResultModel> LoginUser(AuthInputModel model);
    }
}
