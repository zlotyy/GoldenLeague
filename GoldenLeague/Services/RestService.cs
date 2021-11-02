using GoldenLeague.Common.Services;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;

namespace GoldenLeague.Services
{
    public class RestService : Common.Services.RestService, IRestService
    {
        public RestService(IOptions<AppSettings> options)
        {
            _client = new RestClient(options.Value.RestApi.Url)
            {
                Authenticator = new HttpBasicAuthenticator(options.Value.RestApi.UserName, options.Value.RestApi.Password)
            };
        }
    }
}
