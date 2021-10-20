using GoldenLeague.Database.Enums;
using System.Linq;

namespace GoldenLeague.Database.Queries
{
    public interface IBaseQueries
    {
        string GetConfigValue(ConfigKeys key);
    }
    public class BaseQueries : IBaseQueries
    {
        private readonly IDbContextFactory _dbContextFactory;

        public BaseQueries(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public string GetConfigValue(ConfigKeys key)
        {
            using (var db = _dbContextFactory.Create())
            {
                var k = key.ToString();
                return db.ConfigDictionary.First(x => x.ConfigKey == k).ConfigValue;
            }
        }
    }
}
