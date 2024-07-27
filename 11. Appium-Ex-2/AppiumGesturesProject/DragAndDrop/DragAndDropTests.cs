using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace DragAndDrop
{
	public class DragAndDropTests
	{
		private AndroidDriver _driver;
		private AppiumLocalService _appiumLocalService;

		[OneTimeSetUp]
		public void Setup()
		{
			_appiumLocalService = new AppiumServiceBuilder()
				.WithIPAddress("127.0.0.1")
				.UsingPort(4723)
				.Build();
			_appiumLocalService.Start();

			var androidOptions = new AppiumOptions()
			{
				PlatformName = "Android",
				AutomationName = "UiAutomator2",
				DeviceName = "Pixel 7",
				PlatformVersion = "14",
				App = @"C:\\SoftUni - Automation\\11. Appium-Ex-2\\ApiDemos-debug.apk"
			};

			_driver = new AndroidDriver(_appiumLocalService, androidOptions);

			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			_driver?.Quit();
			_driver?.Dispose();
			_appiumLocalService?.Dispose();
		}

		[Test]
		public void DragAndDropItemTest()
		{
			var viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
			viewsButton.Click();

			var dragAndDropButton = _driver.FindElement(MobileBy.AccessibilityId("Drag and Drop"));
			dragAndDropButton.Click();

			var firstDot = _driver.FindElement(MobileBy.Id("drag_dot_1"));

			var secondDot = _driver.FindElement(MobileBy.Id("drag_dot_2"));

			var scriptArgs = new Dictionary<string, object>
			{
				{ "elementId", firstDot.Id },
				{ "endX",  secondDot.Location.X + (secondDot.Size.Width/2)},
				{ "endY", secondDot.Location.Y + (secondDot.Size.Height/2) },
				{ "speed", 2500 }
			};

			_driver.ExecuteScript("mobile: dragGesture", scriptArgs);

			var dropedMessage = _driver.FindElement(MobileBy.Id("drag_result_text"));

			Assert.That(dropedMessage.Text, Is.EqualTo("Dropped!"), "The dot was not dropped");
		}   
	}
}