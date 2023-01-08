using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SignalRApp.Models.MessagerModels.ViewModels;
using SignalRApp.Services;

namespace SignalRApp.Controllers
{
    [Route("Messenger")]
    public class MessengerController : Controller
    {
        private readonly AccountService _accountService;
        private readonly MessengerService _messengerService;
        private readonly UsersService _usersService;

        public MessengerController(AccountService accountService,
            MessengerService messengerService,
            UsersService usersService)
        {
            _accountService = accountService;
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
        [HttpGet("/messages")]
        [Authorize]
        public JsonResult GetMessages(Guid recipientUserId)
        {
            var currentUserId = _usersService.GetCurrentUserId(User);
            var result = _messengerService.GetMessagesList(currentUserId, recipientUserId);

            var recipientInfo = _usersService.GetUserInfo(recipientUserId);

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
