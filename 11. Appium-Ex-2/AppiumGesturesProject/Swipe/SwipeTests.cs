using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;

namespace Swipe
{
	public class SwipeTests
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
		public void SwipeTest()
		{
			var viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
			viewsButton.Click();

			var galleryButton = _driver.FindElement(MobileBy.AccessibilityId("Gallery"));
			galleryButton.Click();

			var photosButton = _driver.FindElement(MobileBy.AccessibilityId("1. Photos"));
			photosButton.Click();

			var firstPhoto = _driver.FindElements(MobileBy.ClassName("android.widget.ImageView"))[0];

			var actions = new Actions(_driver);
			var swipe = actions.ClickAndHold(firstPhoto)
				.MoveByOffset(-200, 0)
				.Release()
				.Build();
			swipe.Perform();

			var thirdPhoto = _driver.FindElements(MobileBy.ClassName("android.widget.ImageView"))[2];

			Assert.That(thirdPhoto, Is.Not.Null, "Third photo is not visible.");
		}
	}
}