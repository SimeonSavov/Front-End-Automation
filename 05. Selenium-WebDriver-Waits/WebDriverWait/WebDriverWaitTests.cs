using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebDriverWaitLab
{
	public class WebDriverWaitTests
	{
		private IWebDriver driver;

		[SetUp]
		public void Setup()
		{
			driver = new ChromeDriver();
			driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/dynamic.html");
		}

		[TearDown]
		public void TearDown()
		{
			driver.Quit();
			driver.Dispose();
		}

		[Test, Order(1)]
		public void AddBoxWithoutWait_Fails()
		{
			var addButton = driver.FindElement(By.XPath("//input[@id='adder']"));
			addButton.Click();

			// Assert that NoSuchElementException is thrown when the nex box element is not found
			Assert.Throws<NoSuchElementException>(() =>
			{
				driver.FindElement(By.Id("box0"));
			});
		}

		[Test, Order(2)]
		public void RevealInputWithoutWaits_Fail()
		{
			// Click the button to reveal the input
			driver.FindElement(By.Id("reveal")).Click();

			// Assert that ElementNotInteractableException is thrown when the input element is not interactable
			Assert.Throws<ElementNotInteractableException>(() =>
			{
				var revealed = driver.FindElement(By.Id("revealed"));
				revealed.SendKeys("Displayed");
			});
		}

		[Test, Order(3)]
		public void AddBox_WithThreadSleep()
		{
			// Click the button to add a box
			driver.FindElement(By.Id("adder")).Click();

			// Wait for a fixed amount of time (3 seconds)
			Thread.Sleep(3000);

			// Attempt to find the newly added box element
			var newBox = driver.FindElement(By.Id("box0"));

			// Assert that the new box element is displayed
			Assert.IsTrue(newBox.Displayed);
		}

		[Test, Order(4)]
		public void AddBox_WithImplicitWait()
		{
			// Click the button to add a box
			driver.FindElement(By.Id("adder")).Click();

			// Set up implicit wait
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			// Attempt to find the newly added box element
			var newBox = driver.FindElement(By.Id("box0"));

			// Assert that the new box element is displayed
			Assert.IsTrue(newBox.Displayed);
		}

		[Test, Order(5)]
		public void RevealInput_WithImplicitWaits()
		{
			// Set up implicit wait
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			// Click the button to reveal the input
			driver.FindElement(By.Id("reveal")).Click();

			// Find the revealed element (implicit wait will handle the wait)
			var revealed = driver.FindElement(By.Id("revealed"));

			Assert.That(revealed.TagName, Is.EqualTo("input"));
		}

		[Test, Order(6)]
		public void RevealInput_WithExplicitWaits()
		{
			driver.FindElement(By.Id("reveal")).Click();

			var revealed = driver.FindElement(By.Id("revealed"));

			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

			wait.Until(d => revealed.Displayed);

			revealed.SendKeys("Displayed");

			Assert.That(revealed.GetAttribute("value"), Is.EqualTo("Displayed"));
		}

		[Test, Order(7)]
		public void AddBox_WithFluentWait_ExpectedConditions_And_IgnoredExceptions()
		{
			// Click the button to add a box
			driver.FindElement(By.Id("adder")).Click();

			// Set up FluentWait with ExpectedConditions
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			wait.PollingInterval = TimeSpan.FromMilliseconds(500);
			wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

			// Wait until the nex box element is present and displayed
			var newBox = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("box0")));

			// Assert that the new box element is displayed
			Assert.IsTrue(newBox.Displayed);
		}

		[Test, Order(8)]
		public void RevealInput_WithCustomFluentWait()
		{
			driver.FindElement(By.Id("reveal")).Click();

			var revealed = driver.FindElement(By.Id("revealed"));

			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
			{
				PollingInterval = TimeSpan.FromMilliseconds(200)
			};
			wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));

			wait.Until(d =>
			{
				revealed.SendKeys("Displayed");
				return true;
			});

			Assert.That(revealed.TagName, Is.EqualTo("input"));
			Assert.That(revealed.GetAttribute("value"), Is.EqualTo("Displayed"));
		}
	}
}