using Microsoft.AspNetCore.SignalR;

using SignalRApp.Services;

using System;
using System.Threading.Tasks;

namespace SignalRApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly MessengerService _messengerService;
        private readonly UsersService _usersService;

        public ChatHub(MessengerService messengerService,
            UsersService usersService)
        {
            _messengerService = messengerService;
            _usersService = usersService;
        }

        public async Task Send(string message, string userName)
        {
            await this.Clients.All.SendAsync("Send", message, userName);
        }

    }
}
