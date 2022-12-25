﻿using Microsoft.IdentityModel.Tokens;
using NLog;
using SignalRApp.Entities;
using SignalRApp.Interfaces;
using SignalRApp.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SignalRApp.Managers
{
    public class AuthManager
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUserRepository _userRepository;

        public AuthManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ResultModel<AuthModel> LoginUser(string login, string pass)
        {
            var errMessage = "";

            try
            {
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass))
                {
                    errMessage = "Input data is incorrect! Login or pass is empty";
                    _logger.Error(errMessage);

                    return new ResultModel<AuthModel>(errMessage);
                }

                /// Функционал с Ldap выключен
                //var ldapUser = LdapService.GetLdapUser(login, pass);
                //if (!ldapUser.IsSuccess)
                //{
                //    errMessage = ldapUser.Error;
                //    _logger.Error(errMessage);

                //    return new ResultModel<AuthModel>(errMessage);
                //}

                var userEntity = _userRepository.FindItemByLoginOrEmail(login);

                if (userEntity == null)
                {
                    errMessage = "Input data is incorrect! Login or pass is empty";
                    _logger.Error(errMessage);

                    return new ResultModel<AuthModel>(errMessage);
                }

                var claim = _getIdentity(userEntity);
                var jwt = _getJwtToken(claim);

                /// проверка по хешу, изменились ли данные пользователя на стороне LDAP
                //var ldapUserHash = ldapUser.Data.GenerateStateHash();
                //if (userEntity.Hash != ldapUserHash)
                //{
                //    ldapUser.Data.Hash = ldapUserHash;
                //    ldapUser.Data.Id = userEntity.Id;
                //    _userRepository.UpdateItem(ldapUser.Data);
                //}

                // _userRepository.UpdateUsersPhoto();

                return new ResultModel<AuthModel>(new AuthModel(jwt, userEntity.Login, userEntity.Id));
            }
            catch (Exception ex)
            {
                errMessage = $"Error in LoginUser {ex.Message}";
                _logger.Error(errMessage);

                return new ResultModel<AuthModel>(errMessage);
            }
        }

        private string _getJwtToken(ClaimsIdentity identity)
        {
            var jwt = new JwtSecurityToken(
                   issuer: AuthOptions.ISSUER,
                   audience: AuthOptions.AUDIENCE,
                   notBefore: DateTime.UtcNow,
                   claims: identity.Claims,
                   signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }


        private ClaimsIdentity _getIdentity(UserEntity user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
                };

            var claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
