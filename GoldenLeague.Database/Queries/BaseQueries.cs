using GoldenLeague.Database.Enums;
using System;
using System.Linq;

namespace GoldenLeague.Database.Queries
{
    public interface IBaseQueries
    {
        string GetConfigValue(ConfigKeys key);
        int GetCurrentSeasonNo();
        int GetCurrentGameweek();
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

        public int GetCurrentSeasonNo()
        {
            return int.Parse(GetConfigValue(ConfigKeys.CURRENT_SEASON_NO));
        }

        public int GetCurrentGameweek()
        {
            using (var db = _dbContextFactory.Create())
            {
                var now = DateTime.Now;
                var currentSeasonNo = GetCurrentSeasonNo();

                var data = db.Matches.Where(x => x.MatchDateTime >= now).OrderBy(x => x.MatchDateTime).FirstOrDefault()?.GameweekNo;
                return data ?? 1;
            }
        }
    }
}
