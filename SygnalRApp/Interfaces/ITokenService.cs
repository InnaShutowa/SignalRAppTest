using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApp.Interfaces
{
    interface ITokenService
    {
        string GenerateAccessToken();
        string GenerateRefreshToken();
    }
}
