using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeague.TransportModels.Users
{
    public class UserCreateModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
