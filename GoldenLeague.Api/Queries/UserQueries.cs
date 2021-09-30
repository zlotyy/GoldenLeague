using GoldenLeague.Database;
using GoldenLeague.TransportModels.Users;
using System.Linq;

namespace GoldenLeague.Api.Queries
{
    public interface IUserQueries
    {
        Users GetUser(UserCredentials credentials);
    }
    public class UserQueries : IUserQueries
    {
        private readonly IDbContextFactory _dbContextFactory;

        public UserQueries(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Users GetUser(UserCredentials credentials)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.Users.FirstOrDefault(u => u.Login == credentials.Login && u.Password == credentials.Password); // TODO - PasswordHash
            }
        }
    }
}
