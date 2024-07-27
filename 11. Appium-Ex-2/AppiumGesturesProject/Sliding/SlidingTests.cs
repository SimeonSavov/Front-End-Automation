using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Interactions;
using System.Drawing;

namespace Sliding
{
	public class SlidingTests
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
		public void SeekBarTest()
		{
			var viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
			viewsButton.Click();

			ScrollToText("Seek Bar");
			var seekBarButton = _driver.FindElement(MobileBy.AccessibilityId("Seek Bar"));
			seekBarButton.Click();

			MoveSeekBarWithInspectorCoordinates(542, 231, 1042, 234);

			var resultElement = _driver.FindElement(MobileBy.Id("progress"));

			Assert.That(resultElement.Text, Is.EqualTo("100 from touch=true"));
		}

		public void ScrollToText(string text)
		{
			_driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
		}

		public void MoveSeekBarWithInspectorCoordinates(int startX, int startY, int endX, int endY)
		{
			var finger = new PointerInputDevice(PointerKind.Touch);
			var start = new Point(startX, startY);
			var end = new Point(endX, endY);
			var swipe = new ActionSequence(finger);

			swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, startX, startY, TimeSpan.Zero));

			swipe.AddAction(finger.CreatePointerDown(MouseButton.Left));

			swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, endX, endY, TimeSpan.FromSeconds(1)));

			swipe.AddAction(finger.CreatePointerUp(MouseButton.Left));

			_driver.PerformActions(new List<ActionSequence>() { swipe });
		}
	}
}