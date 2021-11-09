namespace GoldenLeague.StatisticsWorker
{
    public class AppSettings
    {
        public int MatchResultWorkerDelay { get; set; }
        public ServiceSettings FantasyApi { get; set; }
        public ServiceSettings GoldenLeagueApi { get; set; }
    }

    public class ServiceSettings : RestApiCredentials
    {
        public string Url { get; set; }
    }

    public class RestApiCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
