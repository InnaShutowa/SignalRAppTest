using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRApp.Entities;
using SignalRApp.Managers;
using SignalRApp.Models;
using SignalRApp.Repositories;

namespace SignalRApp.Controllers
{
    public class MessengerController : Controller
    {
        private readonly AuthManager _authManager = new AuthManager(new UserRepository());
        private readonly MessengerManager _messengerManager = new MessengerManager(new UserRepository(), new MessageRepository());

        [HttpGet("/users")]
        [Authorize]
        public ResultModel<List<TredModel>> Users()
        {
            return _messengerManager.GetTredsList(User.Identity.Name);
        }


    }
}
