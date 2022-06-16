using GoldenLeague.Database;
using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.Users;
using LinqToDB;
using Microsoft.Extensions.Logging;
using System;

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
                    var user = new Users
                    {
                        UserId = Guid.NewGuid(),
                        Login = model.Login,
                        FullName = model.FullName,
                        Password = model.Password,
                        IsAdmin = false,
                        IsDeleted = false                        
                    };

                    db.Insert(user);

                    result.Data = new UserModel
                    {
                        UserId = user.UserId,
                        Login = user.Login,
                        FullName = user.FullName,
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
