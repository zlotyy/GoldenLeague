using GoldenLeague.Common.Services;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;

namespace GoldenLeague.StatisticsWorker.Services
{
    public interface IRestServiceFactory
    {
        RestService CreateFantasyService();
        RestService CreateGoldenLeagueService();
    }

    public class RestServiceFactory : IRestServiceFactory
    {
        private readonly AppSettings _config;

        public RestServiceFactory(IOptions<AppSettings> config)
        {
            _config = config.Value;
        }

        public RestService CreateFantasyService()
        {
            return new RestService(_config.FantasyApi.Url);
        }

        public RestService CreateGoldenLeagueService()
        {
            return new RestService(_config.GoldenLeagueApi.Url, _config.GoldenLeagueApi.UserName, _config.GoldenLeagueApi.Password);
        }
    }

    public class RestService : Common.Services.RestService, IRestService
    {
        public RestService(string baseUrl, string userName = null, string password = null)
        {
            if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                _client = new RestClient(baseUrl);
            }
            else
            {
                _client = new RestClient(baseUrl)
                {
                    Authenticator = new HttpBasicAuthenticator(userName, password)
                };
            }
        }
    }
}
