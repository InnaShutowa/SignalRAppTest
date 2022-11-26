using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRApp.Managers;
using SignalRApp.Models;
using SignalRApp.Repositories;

namespace SignalRApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthManager _authManager = new AuthManager(new UserRepository());

        [HttpPost("/token")]
        public ResultModel<AuthModel> Token(string username, string password)
        {
            var authModel = _authManager.LoginUser(username, password);

            if (!authModel.IsSuccess)
            {
                return new ResultModel<AuthModel>(authModel.Error);
            }

            return authModel;
        }
    }
}
