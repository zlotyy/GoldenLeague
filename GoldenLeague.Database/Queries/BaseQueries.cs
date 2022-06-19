using GoldenLeague.Common.Extensions;
using GoldenLeague.Database.Enums;
using System;
using System.Linq;

namespace GoldenLeague.Database.Queries
{
    public interface IBaseQueries
    {
        string GetConfigValue(ConfigKeys key);
        [Obsolete("Current season is now individual for competition in DB Competitions table")]
        int GetCurrentSeasonNo();
        [Obsolete("Current gameweek is now individual for competition in DB Competitions table")]
        int GetCurrentGameweekNo();
    }

    public class BaseQueries : IBaseQueries
    {
        protected readonly IDbContextFactory _dbContextFactory;

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

        [Obsolete("Current season is now individual for competition in DB Competitions table")]
        public int GetCurrentSeasonNo()
        {
            return int.Parse(GetConfigValue(ConfigKeys.CURRENT_SEASON_NO));
        }

        [Obsolete("Current gameweek is now individual for competition in DB Competitions table")]
        public int GetCurrentGameweekNo()
        {
            using (var db = _dbContextFactory.Create())
            {
                var now = DateTime.Now;
                var currentSeasonNo = GetCurrentSeasonNo();

                var data = db.Matches.Where(x => x.MatchDateTime >= now.BeginOfDay()).OrderBy(x => x.MatchDateTime).FirstOrDefault()?.GameweekNo;
                return data ?? 1;
            }
        }
    }
}
