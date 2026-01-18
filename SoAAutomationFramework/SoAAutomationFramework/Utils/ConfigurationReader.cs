using Microsoft.Extensions.Configuration;

namespace SoAAutomationFramework.Utils
{
    internal static class ConfigurationReader
    {
        private static readonly IConfigurationRoot _config;

        static ConfigurationReader()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();
        }

        public static IConfigurationRoot GetConfigurationProperty() => _config;
    }
}
