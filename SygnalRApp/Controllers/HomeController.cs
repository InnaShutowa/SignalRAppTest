using Microsoft.AspNetCore.Mvc;

namespace SignalRApp.Controllers
{
    /// <summary>
    /// Основной контроллер
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Главная страница
        /// </summary>
        /// <returns>Представление главной страницы</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Страница ошибки при запросе несуществующего экшена
        /// </summary>
        /// <returns>Представление с текстом ошибки</returns>
        [HttpGet("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
