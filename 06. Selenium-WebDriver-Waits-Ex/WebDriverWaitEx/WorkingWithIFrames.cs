using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebDriverWaitEx
{
	public class WorkingWithIFrames
	{
		private WebDriver driver;

		[SetUp]
		public void Setup()
		{
			driver = new ChromeDriver();

			driver.Navigate().GoToUrl("https://codepen.io/pervillalva/full/abPoNLd");
		}

		[TearDown]
		public void TearDown()
		{
			driver.Quit();
			driver.Dispose();
		}

		[Test]
		public void TestFrameByIndex()
		{
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

			// Wait until the Iframe is available and switch to it by finding the first Iframe
			wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));

			// Click the dropdown button
			var dropDownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
			dropDownButton.Click();

			// Select the links inside the dropdown menu
			var dropDownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            // Verify and print the link texts
            foreach (var link in dropDownLinks)
            {
                Console.WriteLine(link.Text);
				Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected");
            }

			driver.SwitchTo().DefaultContent();
        }

		[Test]
		public void TestFrameById()
		{
			// Explicit wait
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

			// Wait until the Iframe is available and switch to it by Id
			wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("result"));

			// Click the dropdown button
			var dropDownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
			dropDownButton.Click();

			// Select the links inside the dropdown menu
			var dropDownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

			// Verify and print the link texts
			foreach (var link in dropDownLinks)
			{
				Console.WriteLine(link.Text);
				Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected");
			}

			driver.SwitchTo().DefaultContent();
		}

		[Test]
		public void TestFrameByWebElement()
		{
			// Explicit wait
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

			// Locate the frame element
			var frameElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#result")));

			// Swithc to the frame by web element
			driver.SwitchTo().Frame(frameElement);

			// Click the dropdown button
			var dropDownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
			dropDownButton.Click();

			// Select the links inside the dropdown menu
			var dropDownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

			// Verify and print the link texts
			foreach (var link in dropDownLinks)
			{
				Console.WriteLine(link.Text);
				Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected");
			}

			driver.SwitchTo().DefaultContent();
		}
	}
}
