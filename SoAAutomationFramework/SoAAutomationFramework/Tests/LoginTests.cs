using SoAAutomationFramework.Actions;
using SoAAutomationFramework.Utils;

namespace SoAAutomationFramework.Tests
{
    public class LoginTests
    {
        private BaseUserActions _user;

        [SetUp]
        public void Setup()
        {
            WebDriverProvider.InitDriver();
            _user = new BaseUserActions();
            _user.StartApplication();
        }

        [TearDown]
        public void TearDown()
        {
            WebDriverProvider.GetPreparedDriver().Quit();
        }

        [Test]
        public void LoginPageLoadsTest()
        {
            _user.OpenPage("Login");
            Assert.Pass();
        }
    }
}