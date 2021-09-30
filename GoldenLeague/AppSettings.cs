using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenLeague
{
    public class AppSettings
    {
        public ServiceSettings WebApi { get; set; }
        public string Secret { get; set; }
    }

    public class ServiceSettings : UserCredentials
    {
        public string Url { get; set; }
    }

    public class UserCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
