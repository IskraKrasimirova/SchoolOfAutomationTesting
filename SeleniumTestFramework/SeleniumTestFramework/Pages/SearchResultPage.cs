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
            _driver.FindElements(By.XPath($"//td[text()='{skillName}']/parent::tr"));

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

            Assert.That(skillCells, Is.Not.Empty, $"No rows found for skill '{skillName}'.");

            foreach (var cell in skillCells) 
            { 
                Assert.That(cell.Text.Trim(), Is.EqualTo(skillName), $"Expected skill '{skillName}', but found '{cell.Text.Trim()}'."); 
            }
        }

        public void VerifyAllRowsHaveCountry(string countryName)
        {
            var countryCells = GetColumnCells("Country");

            Assert.That(countryCells, Is.Not.Empty, $"No rows found for country '{countryName}'.");

            foreach (var cell in countryCells)
            {
                Assert.That(cell.Text.Trim(), Is.EqualTo(countryName), $"Expected country '{countryName}', but found '{cell.Text.Trim()}'.");
            }
        }

        public void VerifyRowsContainOnlyCities(List<string> expectedCities)
        {
            var cityCells = GetColumnCells("City"); 

            Assert.That(cityCells, Is.Not.Empty, "No rows found in the City column."); 

            var actualCities = cityCells
                .Select(c => c.Text.Trim())
                .ToList();

            var unexpectedCities = actualCities.Except(expectedCities).ToList(); 
            
            Assert.That(unexpectedCities, Is.Empty, $"Unexpected cities found: {string.Join(", ", unexpectedCities)}");

            var missingCities = expectedCities.Except(actualCities).ToList(); 
            
            Assert.That(missingCities, Is.Empty, $"Expected cities not found: {string.Join(", ", missingCities)}");
        }

        private int GetColumnIndex(string columnName)
        {
            var headers = _driver.FindElements(By.XPath("//table//thead//th"));

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
