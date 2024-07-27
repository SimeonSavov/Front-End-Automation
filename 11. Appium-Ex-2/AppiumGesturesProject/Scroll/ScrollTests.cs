using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace Scroll
{
	public class ScrollTests
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
		public void ScrollTest()
		{
			var viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
			viewsButton.Click();

			ScrollToText("Lists");

			var listsButton = _driver.FindElement(MobileBy.AccessibilityId("Lists"));

			Assert.That(listsButton, Is.Not.Null, "The list element is not present");

			listsButton.Click();

			var photosButton = _driver.FindElement(MobileBy.AccessibilityId("08. Photos"));

			Assert.That(photosButton, Is.Not.Null, "The photos element is not present");
		}

		public void ScrollToText(string text)
		{
			_driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
		}
	}
}