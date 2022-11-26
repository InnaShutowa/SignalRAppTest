using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApp
{
    public static class Settings
    {
        public static string LdapHost
        {
            get
            {
                return ConfigurationManager.AppSettings[nameof(LdapHost)];
            }
        }
        public static int LdapPort
        {
            get
            {
                var res = int.TryParse(ConfigurationManager.AppSettings[nameof(LdapHost)], out var portInt);
                return res ? portInt : 389;
            }
        }
    }
}
