using GoldenLeague.Common.Services;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;

namespace GoldenLeague.StatisticsWorker.Services
{
    public interface IRestServiceFactory
    {
        RestService CreateFantasyService();
        RestService CreateGoldenLeagueService();
        RestService CreateFootballApiService();
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

        public RestService CreateFootballApiService()
        {
            return new RestService(
                _config.FootballApi.Url, 
                new Dictionary<string, string> { { "x-rapidapi-key", _config.FootballApi.ApiKey } }
            );
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

        public RestService(string baseUrl, Dictionary<string, string> headers)
        {
            _client = new RestClient(baseUrl);
            _client.AddDefaultHeaders(headers);
        }
    }
}
