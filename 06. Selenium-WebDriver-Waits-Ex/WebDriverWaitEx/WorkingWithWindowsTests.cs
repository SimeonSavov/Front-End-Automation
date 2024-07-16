using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WebDriverWaitEx
{
	public class WorkingWithWindowsTests
	{
		private WebDriver driver;

		[SetUp]
		public void Setup()
		{
			driver = new ChromeDriver();

			driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");
		}

		[TearDown]
		public void TearDown()
		{
			driver.Quit();
			driver.Dispose();
		}

		[Test]
		public void HandleMultipleWindows()
		{
			// Click on "Click Here" link to open a new window
			driver.FindElement(By.LinkText("Click Here")).Click();

			// Get all window handles
			ReadOnlyCollection<string> handles = driver.WindowHandles;

			// Ensure that there are at least two windwos open
			Assert.That(handles.Count, Is.EqualTo(2), "There should be two windows open.");

			// Switch to the new window
			driver.SwitchTo().Window(handles[1]);

			// Verify the content of the new window
			string newWindowContent = driver.FindElement(By.TagName("h3")).Text;
			Assert.That(newWindowContent, Is.EqualTo("New Window"), "The content of the new window is not as expected");

			// Log the content of the new window
			string path = Path.Combine(Directory.GetCurrentDirectory(), "windows.txt");

			if (File.Exists(path))
			{
				File.Delete(path);
			}

			File.AppendAllText(path, "Window handle for a new window: " + driver.CurrentWindowHandle + "\n\n");
			File.AppendAllText(path, "The page content: " + newWindowContent + "\n\n");

			// Close the new window
			driver.Close();

			// Switch back to the original window
			driver.SwitchTo().Window(handles[0]);

			// Verify the content of the original window
			string originalWindowContent = driver.FindElement(By.TagName("h3")).Text;
			Assert.That(originalWindowContent, Is.EqualTo("Opening a new window"), "The content of the original window is not as expected");

			// Log the content of the original window
			File.AppendAllText(path, "Window handle for original window: " + driver.CurrentWindowHandle + "\n\n");
			File.AppendAllText(path, "The page content: " + originalWindowContent + "\n\n");
		}

		[Test]
		public void HandleNoSuckWindowException()
		{
			// Click on "Click Here" link to open a new window
			driver.FindElement(By.LinkText("Click Here")).Click();

			// Get all window handles
			ReadOnlyCollection<string> handles = driver.WindowHandles;

			// Switch to the new window
			driver.SwitchTo().Window(handles[1]);

			// Close the new window
			driver.Close();

			try
			{
				// Attempt to switch back to the closed window
				driver.SwitchTo().Window(handles[1]);
			}
			catch (NoSuchWindowException ex)
			{
				// Log the exception
				string path = Path.Combine(Directory.GetCurrentDirectory(), "windows.txt");
				File.AppendAllText(path, "NoSuchWindowException caught: " + ex.Message + "\n\n");
				Assert.Pass("NoSuchWindowException was correctly handled.");
			}
			catch (Exception ex)
			{
				Assert.Fail("An unexpected exception was thrown: " + ex.Message);
			}
			finally
			{
				// Switch back to the original window
				driver.SwitchTo().Window(handles[0]);
			}
		}
	}
}
