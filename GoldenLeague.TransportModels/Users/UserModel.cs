using System;

namespace GoldenLeague.TransportModels.Users
{
    public class UserModel
    {
        public UserModel()
        {
        }

        public UserModel(Database.Users dbUser)
        {
            UserId = dbUser.UserId;
            Login = dbUser.Login;
            FullName = dbUser.FullName;
            IsAdmin = dbUser.IsAdmin;
        }

        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
