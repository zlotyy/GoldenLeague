using GoldenLeague.TransportModels.Users;

namespace GoldenLeague.Models.Users
{
    public class UserAuthenticatedModel : UserModel
    {
        public UserAuthenticatedModel(UserModel user, string token)
        {
            UserId = user.UserId;
            Login = user.Login;
            Email = user.Email;
            IsAdmin = user.IsAdmin;
            Token = token;
        }

        public string Token { get; set; }
    }
}
