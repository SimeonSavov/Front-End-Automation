using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebDriverWaitsExamples
{
	public class Tests
	{
		private WebDriver driver;

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
		public void ExplicitWait_ElementCreated_But_NotVisible()
		{
			driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");

			driver.FindElement(By.XPath("//div[@class='example']//div[@id='start']//button")).Click();

			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

			var finishDiv = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='example']//div[@id='finish']")));

			Assert.True(finishDiv.Displayed);
		}

		[Test]
		public void ImplicitWait_Element_NotCreated()
		{
			driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");

			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			driver.FindElement(By.XPath("//div[@class='example']//div[@id='start']//button")).Click();

			var finishDiv = driver.FindElement(By.XPath("//div[@class='example']//div[@id='finish']"));

			Assert.True(finishDiv.Displayed);
		}

		[Test]
		public void PageLoadTimeout()
		{
			driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

			driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");

			var startButton = driver.FindElement(By.XPath("//div[@class='example']//div[@id='start']//button"));

			Assert.True(startButton.Displayed);
		}

		[Test]
		public void AsyncJSTimeouts()
		{
			driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(60);

			string script = @"const start = new Date().getTime();
			const delay = 45000;
			while (new Date().getTime() < start + delay) {
			// do something while waiting 45 second
			}
			console.log('45 seconds of execution')";

			IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
			jsExecutor.ExecuteScript(script);
		}

		[Test]
		public void FluentWait_ElementCreated_But_NotVisible()
		{
			driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");

			driver.FindElement(By.XPath("//div[@class='example']//div[@id='start']//button")).Click();

			DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
			fluentWait.Timeout = TimeSpan.FromSeconds(10);
			fluentWait.PollingInterval = TimeSpan.FromMilliseconds(50);

			var finishDiv = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='example']//div[@id='finish']")));

			Assert.True(finishDiv.Displayed);
		}

		[Test]
		public void IgnoreException_With_FluentWait()
		{
			driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");

			driver.FindElement(By.XPath("//div[@class='row']//div[@id='content']//div[@class='example']//div//button")).Click();

			DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);

			fluentWait.Timeout = TimeSpan.FromSeconds(10);
			fluentWait.PollingInterval = TimeSpan.FromMilliseconds(50);
			fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

			var finishDiv = fluentWait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@id='finish']")));

			Assert.True(finishDiv.Displayed);
		}
	}
}