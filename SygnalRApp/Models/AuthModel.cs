using Newtonsoft.Json;
using System;

namespace SignalRApp.Models
{
    public class AuthModel
    {
        public AuthModel(string accessToken, string username, Guid userId)
        {
            AccessToken = accessToken;
            Username = username;
            UserId = userId;
        }

        public string AccessToken { get; set; }
        public string Username { get; set; }
        public Guid UserId { get; set; }
    }
}
