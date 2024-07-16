using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebDriverWaitEx
{
	public class ExplicitWaitTests
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

			// Set the implicit wait to 0 before using explicit wait
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

			try
			{
				// Create WebDriverWait object with timeout set to 10 second
				WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

				// Wait to identify the Buy Now link using the LinkText prop
				IWebElement buyNowLink = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));

				// Set the implicit wait back to 10 seconds
				driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

				buyNowLink.Click();

				var productName = driver.FindElement(By.XPath("//form[@name='cart_quantity']//a//strong")).Text;

				// Verify text
				Assert.That(productName, Is.EqualTo("Microsoft Internet Keyboard PS/2"));

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

			// Set the implicit wait to 0 before using explicit wait
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

			try
			{
				// Create WebDriverWait object with timeout set to 10 second
				WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

				// Wait to identify the Buy Now link using the LinkText prop
				IWebElement buyNowLink = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));

				// If found, fail the test as it should not exist
				buyNowLink.Click();
				Assert.Fail("The 'Buy Now' link was found for a non-existing product.");
			}
			catch (WebDriverTimeoutException)
			{
				// Expected exception for non-existing product
				Assert.Pass("Expected WebDriverTimeoutException was thrown.");
			}
			catch (Exception ex)
			{
				Assert.Fail("Unexpected exception: " + ex.Message);
			}
			finally
			{
				// Reset the implicit wait
				driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
			}
		}
	}
}
