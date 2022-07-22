using System;

namespace GoldenLeague.TransportModels.Users
{
    public class UserPasswordChangeModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string PasswordPrevious { get; set; }
        public string PasswordNew { get; set; }
    }
}
