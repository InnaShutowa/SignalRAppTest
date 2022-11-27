using NLog;
using Novell.Directory.Ldap;
using SignalRApp.Entities;
using SignalRApp.Models;
using System;
using Logger = NLog.Logger;

namespace SignalRApp.Services
{
    /// <summary>
    /// Сервис по работе с Ldap
    /// </summary>
    [Obsolete]
    public static class LdapService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Авторизация пользователя через ldap
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="pass">Пароль</param>
        /// <returns></returns>
        public static ResultModel<UserEntity> GetLdapUser(string login, string pass)
        {
            var errMessage = string.Empty;
            try
            {
                var novelLdap = new LdapConnection();

                novelLdap.Connect(Settings.LdapHost, Settings.LdapPort);
                novelLdap.Bind(LdapConnection.LdapV3, $"uid={login},DC=nordclan", pass);

                var result = novelLdap.Search($"uid={login},DC=nordclan", LdapConnection.ScopeBase, "(objectClass=*)",
                    null, false);

                var ldapUser = result.Next();
                if (ldapUser == null)
                {
                    errMessage = $"LdapUser with ";
                    _logger.Error(errMessage);
                    return new ResultModel<UserEntity>(errMessage);
                }

                var attrs = ldapUser.GetAttributeSet();

                if (attrs["firstName"] == null || attrs["lastName"] == null || attrs["emailPrimary"] == null)
                {
                    errMessage = $"LdapUser with userName {login} has empty fields";
                    _logger.Error(errMessage);

                    return new ResultModel<UserEntity>(errMessage);
                }

                var usr = new UserEntity(attrs["firstName"]?.StringValue,
                    attrs["lastName"]?.StringValue, 
                    login,
                    attrs["emailPrimary"]?.StringValue,
                    attrs["jpegPhoto"]?.StringValue);

                return new ResultModel<UserEntity>(usr);
            }
            catch (Exception ex)
            {
                errMessage = $"Error: {ex.Message}";
                _logger.Error(errMessage);

                return new ResultModel<UserEntity>(errMessage);
            }
        }
    }
}
