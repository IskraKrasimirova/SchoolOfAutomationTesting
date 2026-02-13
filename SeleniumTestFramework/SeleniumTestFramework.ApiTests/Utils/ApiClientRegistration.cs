using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using SeleniumTestFramework.ApiTests.Apis;

namespace SeleniumTestFramework.ApiTests.Utils
{
    public static class ApiClientRegistration
    {
        public static void AddApiClient(this IServiceCollection services, string baseUrl)
        {
            services.AddSingleton(sp =>
            {
                var options = new RestClientOptions(baseUrl);
                var client = new RestClient(options);
                client.AddDefaultHeader("Accept", "application/json");
                return client;
            });

            services.AddScoped<UsersApi>();
        }
    }
}
