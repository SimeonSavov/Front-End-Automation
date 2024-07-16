using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebDriverWaitEx
{
	public class ImplicitWaitTests
	{
		private WebDriver driver;

		[SetUp]
		public void Setup()
		{
			driver = new ChromeDriver();

			driver.Navigate().GoToUrl("http://practice.bpbonline.com/");

			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
		}

		[TearDown]
		public void TearDown()
		{
			driver.Quit();
			driver.Dispose();
		}

		[Test]
		public void SearchProduct_Keyboard_ShouldAddToCart()
		{
			// Fill in the search field textbox
			driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("keyboard");

			// Click on the search icon
			driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

			try
			{
				// Click on the Buy Now link
				driver.FindElement(By.LinkText("Buy Now")).Click();

				// Verify text
				Assert.IsTrue(driver.PageSource.Contains("keyboard"), "The product 'keyboard' was not found in the cart page");
			}
			catch (Exception ex)
			{
				Assert.Fail("Unexpected exception: " + ex.Message);
			}
			
		}

		[Test]
		public void SearchProduct_Junk_ShouldThrowNoSuchElementException()
		{
			// Fill in the search field textbox
			driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("junk");

			// Click on the search icon
			driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

			try
			{
				// Try to click on Buy now link
				driver.FindElement(By.LinkText("Buy Now")).Click();
			}
			catch (NoSuchElementException ex)
			{
				// Verify the exception for non-existing product
				Assert.Pass("Expected NoSuckElementException was thrown.");
			}
			catch (Exception ex)
			{
				Assert.Fail("Unexpected exception: " + ex.Message);
			}
		}
	}
}