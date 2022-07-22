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
            Email = dbUser.Email;
            IsAdmin = dbUser.IsAdmin;
        }

        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
