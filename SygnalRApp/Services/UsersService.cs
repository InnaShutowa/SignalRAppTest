using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SignalRApp.Entities;
using SignalRApp.Models.Users;
using SignalRApp.Repositories.Interfaces;
using SignalRApp.Services.Interfaces;

namespace SignalRApp.Services
{
    /// <inheritdoc cref="IUsersService" />
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<UserIdentity> _userManager;

        public UsersService(IUserRepository userRepository, UserManager<UserIdentity> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        /// <inheritdoc/>
        public Guid? GetCurrentUserId(ClaimsPrincipal claimsPrincipal)
        {
            var userIdentity = _userManager.GetUserId(claimsPrincipal);

            return _userRepository.GetUserIdByIdentityId(userIdentity);
        }

        /// <inheritdoc/>
        public Guid? GetUserIdByUsername(string userName)
        {
            var user = _userRepository.GetItemByLoginOrEmail(userName);

            return user?.Id;
        }

        /// <inheritdoc/>
        public UserModel GetUserInfo(Guid? userId)
        {
            var userInfo = _userRepository.GetItemByGuid(userId);

            if (userInfo != null)
            {
                return new UserModel
                {
                    Id = userInfo.Id,
                    JpegPhoto = userInfo.JpegPhoto,
                    UserName = userInfo.Login,
                    FullName = userInfo.FirstName + " " + userInfo.LastName,
                };
            }

            return null;
        }
    }
}
