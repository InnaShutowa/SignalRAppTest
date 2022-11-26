using SignalRApp.Interfaces;
using System;
using System.Security.Cryptography;

namespace SignalRApp.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateAccessToken()
        {
            throw new NotImplementedException();
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
