using OpenQA.Selenium;
using SeleniumTestFramework.Utilities;
using SeleniumTestFramework.Utilities.Extensions;

namespace SeleniumTestFramework.Pages
{
    public class SearchResultPage : BasePage
    {
        private IWebElement ResultsHeader => _driver.FindElement(By.XPath("//h3[contains(text(),'Search Results')]"));
        private IWebElement ResultsTable => _driver.FindElement(By.XPath("//table[contains(@class, 'table')]"));
        private IWebElement NewSearchButton => _driver.FindElement(By.XPath("//a[@href='search.php' and contains(@class, 'btn')]"));

        private IWebElement? FindUserRowByEmail(string email) =>
            _driver.FindElements(By.XPath($"//td[contains(text(), '{email}')]/parent::tr"))
           .FirstOrDefault();

        private ICollection<IWebElement> FindRowsBySkill(string skillName) =>
            _driver.FindElements(By.XPath($"//td[contains(text(), '{skillName}')]/parent::tr"));

        public SearchResultPage(IWebDriver driver) : base(driver)
        {
        }

        public void VerifyIsAtSearchResultPage()
        {
            _driver.WaitUntilUrlContains("/search_result");

            Retry.Until(() =>
            {
                if (!ResultsHeader.Displayed)
                    throw new RetryException("Search Results page not loaded yet.");
            });

            Assert.Multiple(() =>
            {
                Assert.That(ResultsTable.Displayed, "Results Table is not visible.");
                Assert.That(NewSearchButton.Displayed, "Button for New Search is not visible.");
            });
        }

        public void VerifyUserExists(string email)
        {
            IWebElement? userRow = FindUserRowByEmail(email);
            Assert.That(userRow, Is.Not.Null, $"User with email {email} was not found.");
        }

        public void VerifyAllRowsHaveSkill(string skillName)
        {
            var skillCells = GetColumnCells("Skill");

            foreach (var cell in skillCells) 
            { 
                Assert.That(cell.Text.Trim(), Is.EqualTo(skillName), $"Expected skill '{skillName}', but found '{cell.Text.Trim()}'."); 
            }
        }

        private int GetColumnIndex(string columnName)
        {
            var headers = _driver.FindElements(By.XPath("//table//thead//th"));
            foreach (var h in headers) 
            { 
                Console.WriteLine($"HEADER: '{h.Text}'"); 
            }

            for (int i = 0; i < headers.Count; i++)
            {
                if (headers[i].Text.Trim().Equals(columnName, StringComparison.OrdinalIgnoreCase)) 
                    return i;
            }

            throw new Exception($"Column '{columnName}' not found.");
        }

        private IReadOnlyCollection<IWebElement> GetColumnCells(string columnName)
        {
            int columnIndex = GetColumnIndex(columnName);
            var cellsByColomn = _driver.FindElements(By.XPath($"//table//tbody//tr/td[{columnIndex}]"));

            return cellsByColomn;
        }
    }
}
