using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTest
{
	public class FirstTest
	{
		private IWebDriver driver;

		[SetUp]
		public void Setup()
		{
			driver = new ChromeDriver();
		}

		[TearDown]
		public void TearDown()
		{
			driver.Quit();
			driver.Dispose();
		}

		[Test]
		public void Test_NakovCom()
		{
			driver.Url = "https://nakov.com";
			var windowTitle = driver.Title;
			Assert.That(windowTitle.Contains("Svetlin Nakov – Official Web Site"));

			var searchLink = driver.FindElement(By.ClassName("smoothScroll"));
			Assert.That(searchLink.Text, Does.Contain("SEARCH"));

			searchLink.Click();

			var message = driver.FindElement(By.Id("s"));
			var placeholderText = message.GetAttribute("placeholder");
			Assert.That(placeholderText, Is.EqualTo("Search this site"));
		}
	}
}