using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebDriver_Lab
{
	internal class WikipediaSearch
	{
		static void Main(string[] args)
		{
			// Create a new instance of ChromeDriver
			var driver = new ChromeDriver();

			// Navigate to the Wikipedia page
			driver.Url = "https://wikipedia.org";

			// Find the search input element by Id
			var searchBox = driver.FindElement(By.Id("searchInput"));

			// Click on the search box to focus it
			searchBox.Click();

			// Type "QA" into the search box and press Enter
			searchBox.SendKeys("Quality Assurance" + Keys.Enter);

			// Print the title of the QA search
			Console.WriteLine("Quality Assurance page title: " + driver.Title);

			// Close the browser
			driver.Quit();
		}
	}
}
