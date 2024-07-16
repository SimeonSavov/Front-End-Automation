using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace StudentRegistryPOM.Tests
{
	public class BaseTest
	{
		protected IWebDriver driver;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			driver = new ChromeDriver();
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			driver.Quit();
			driver.Dispose();
		}
	}
}
