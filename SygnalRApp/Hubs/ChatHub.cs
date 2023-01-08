using Microsoft.AspNetCore.SignalR;

using SignalRApp.Services;
using SignalRApp.Services.Interfaces;

using System;
using System.Threading.Tasks;

namespace SignalRApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessengerService _messengerService;
        private readonly IUsersService _usersService;

        public ChatHub(IMessengerService messengerService,
            IUsersService usersService)
        {
            _messengerService = messengerService;
            _usersService = usersService;
        }

        public async Task Send(string message, string authorUserName, string recipientUserName)
        {
            var authorUserId = _usersService.GetUserIdByUsername(authorUserName);
            var recipientUserId = _usersService.GetUserIdByUsername(recipientUserName);

            _messengerService.AddMessage(authorUserId, recipientUserId, message);
            await this.Clients.All.SendAsync("Send", message, authorUserName);
        }

    }
}
