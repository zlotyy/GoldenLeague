using GoldenLeague.TransportModels.Users;

namespace GoldenLeague.Models.Users
{
    public class UserAuthenticatedModel : UserModel
    {
        public UserAuthenticatedModel(UserModel user, string token)
        {
            UserId = user.UserId;
            Login = user.Login;
            FullName = user.FullName;
            IsAdmin = user.IsAdmin;
            Token = token;
        }

        public string Token { get; set; }
    }
}
