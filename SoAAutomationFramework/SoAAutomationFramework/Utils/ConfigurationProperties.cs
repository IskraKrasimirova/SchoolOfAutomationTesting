namespace SoAAutomationFramework.Utils
{
    internal static class ConfigurationProperties
    {
        public const string DRIVER = "webdriver:driver";
        public const string DRIVER_IMPLICIT_WAIT = "webdriver:implicit_wait";
        public const string DRIVER_EXPLICIT_WAIT = "webdriver:explicit_wait";
        public const string BROWSER_WINDOW_MODE = "webdriver:window_mode";
        public const string BASE_URL = "env:default:host";
        public const string LOGIN_PATH = "env:default:pages_path:login";
        public const string PAGES_PATH = "env:default:pages_path:";
        public const string API_URL = "env:default:api_url";
    }
}
