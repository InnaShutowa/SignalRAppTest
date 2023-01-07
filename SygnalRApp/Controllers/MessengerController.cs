using System.Collections.Generic;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SignalRApp.Models;
using SignalRApp.Models.MessagerModels;
using SignalRApp.Repositories;
using SignalRApp.Services;

namespace SignalRApp.Controllers
{
    public class MessengerController : Controller
    {
        private readonly AccountService _accountService;
        private readonly MessengerService _messengerService;

        public MessengerController(AccountService accountService,
            MessengerService messengerService)
        {
            _accountService = accountService;
            _messengerService = messengerService;
        }

        [HttpGet("/users")]
        [Authorize]
        public ResultModel<List<TredModel>> Users()
        {
            return _messengerService.GetTredsList(User.Identity.Name);
        }


    }
}
