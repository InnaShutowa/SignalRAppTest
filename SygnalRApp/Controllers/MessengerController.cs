using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SignalRApp.Models.MessagerModels.ViewModels;
using SignalRApp.Services;
using SignalRApp.Services.Interfaces;

namespace SignalRApp.Controllers
{
    public class MessengerController : Controller
    {
        private readonly IMessengerService _messengerService;
        private readonly IUsersService _usersService;

        public MessengerController(IMessengerService messengerService,
            IUsersService usersService)
        {
            _messengerService = messengerService;
            _usersService = usersService;
        }

        /// <summary>
        /// Получает список тредов
        /// </summary>
        /// <returns>Список тредов</returns>
        [HttpGet("/users")]
        [Authorize]
        public IActionResult Users()
        {
            var currentUserId = _usersService.GetCurrentUserId(User);
            var result = _messengerService.GetTredsList(currentUserId);

            if (result.IsSuccess)
            {
                return View(new UsersViewModel
                {
                    TredModels = result.Data,
                    UserName = User.Identity.Name
                });
            }

            return View();
        }

        /// <summary>
        /// Разлогинивает пользователя
        /// </summary>
        [HttpGet("/logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return View();
        }

        /// <summary>
        /// Возвращает список сообщений в конкретном треде
        /// </summary>
        /// <param name="recipientUserId">Id получателя</param>
        /// <returns>Модель сообщений в треде и данных собеседника</returns>
        [HttpGet]
        [Authorize]
        public JsonResult GetMessages(string recipientUserId)
        {
            Guid userId;
            if (!Guid.TryParse(recipientUserId, out userId))
            {
                return Json(null);
            }

            var currentUserId = _usersService.GetCurrentUserId(User);
            var result = _messengerService.GetMessagesList(currentUserId, userId);

            var recipientInfo = _usersService.GetUserInfo(userId);

            if (result.IsSuccess)
            {
                return Json(new MessagesViewModel
                {
                    Messages = result.Data,
                    RecipientJpegPhoto = recipientInfo.JpegPhoto,
                    RecipientName = recipientInfo.FullName
                });
            }

            return Json(null);
        }

    }
}
