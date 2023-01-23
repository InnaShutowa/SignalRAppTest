using Microsoft.AspNetCore.SignalR;
using SignalRApp.Services.Interfaces;
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

        public async Task Send(string text, string authorUserName, string recipientUserName)
        {
            var authorUserId = _usersService.GetUserIdByUsername(authorUserName);
            var recipientUserId = _usersService.GetUserIdByUsername(recipientUserName);

            var message = _messengerService.AddMessage(authorUserId, recipientUserId, text);
            if (message.IsSuccess)
            {
                await this.Clients.All.SendAsync("Send", text, authorUserName, message.Data.SendDate);
            }
        }

    }
}
