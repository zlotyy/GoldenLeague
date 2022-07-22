using GoldenLeague.Api.Helpers;
using GoldenLeague.Database;
using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.Users;
using LinqToDB;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace GoldenLeague.Api.Commands
{
    public interface IUserCommands
    {
        Result<UserModel> UserCreate(UserCreateModel model);
    }

    public class UserCommands : IUserCommands
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<UserCommands> _logger;

        public UserCommands(IDbContextFactory dbContextFactory, ILogger<UserCommands> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }

        public Result<UserModel> UserCreate(UserCreateModel model)
        {
            var result = new Result<UserModel>();

            try
            {
                using (var db = _dbContextFactory.Create())
                {
                    var salt = PasswordHelpers.GetSalt();
                    var password = PasswordHelpers.GetHash(model.Password, salt);

                    var user = new Users
                    {
                        UserId = Guid.NewGuid(),
                        Login = model.Login,
                        Email = model.Email,
                        Password = password,
                        PasswordSalt = salt,
                        IsAdmin = false,
                        IsDeleted = false
                    };

                    var globalLeagueId = db.BookmakerLeagues.Where(x => !x.InsertUserId.HasValue).Select(x => x.LeagueId).First();
                    var globalLeagueLUser = new BookmakerLeaguesLUsers
                    {
                        LeagueId = globalLeagueId,
                        UserId = user.UserId,
                        UserJoinDate = DateTime.Now
                    };

                    db.Insert(user);

                    result.Data = new UserModel
                    {
                        UserId = user.UserId,
                        Login = user.Login,
                        Email = user.Email,
                        IsAdmin = user.IsAdmin
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(UserCreate)}");
                result.Errors.Add("Wystąpił błąd podczas tworzenia użytkownika");
            }

            return result;
        }
    }
}
