using GoldenLeague.Database;
using System;
using System.Linq;

namespace GoldenLeague.Api.Queries
{
    public interface IUserQueries
    {
        Users GetUser(Guid userId);
        Users GetUser(string login);
        bool UserExists(Guid userId);
        bool UserExists(string login);
    }

    public class UserQueries : IUserQueries
    {
        private readonly IDbContextFactory _dbContextFactory;

        public UserQueries(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Users GetUser(Guid userId)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.Users.FirstOrDefault(x => x.UserId == userId && !x.IsDeleted);
            }
        }

        public Users GetUser(string login)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.Users.FirstOrDefault(x => x.Login == login && !x.IsDeleted);
            }
        }

        public bool UserExists(Guid userId)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.Users.Any(x => x.UserId == userId && !x.IsDeleted);
            }
        }

        public bool UserExists(string login)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.Users.Any(x => x.Login == login && !x.IsDeleted);
            }
        }
    }
}
