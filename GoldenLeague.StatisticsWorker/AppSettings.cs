namespace GoldenLeague.StatisticsWorker
{
    public class AppSettings
    {
        public int FrequentWorkerDelay { get; set; }
        public int InFrequentWorkerDelay { get; set; }
        public RestApiSettings FantasyApi { get; set; }
        public RestApiWithCredentials GoldenLeagueApi { get; set; }
        public RestApiWithKey FootballApi { get; set; }
    }

    public class RestApiSettings
    {
        public string Url { get; set; }
    }

    public class RestApiWithCredentials : RestApiSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RestApiWithKey : RestApiSettings
    {
        public string ApiKey { get; set; }
    }
}
