using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;
using System.Drawing;

namespace ZoomInAndOut
{
	public class ZoomInAndOutTests
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
		public void ZoomInTest()
		{
			var viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
			viewsButton.Click();

			ScrollToText("WebView");
			var webViewButton = _driver.FindElement(MobileBy.AccessibilityId("WebView"));
			webViewButton.Click();

			PerformZoomIn(431, 727, 258, 446, 550, 998, 792, 1222);
		}

		[Test]
		public void ZoomOutTest()
		{
			var viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
			viewsButton.Click();

			ScrollToText("WebView");
			var webViewButton = _driver.FindElement(MobileBy.AccessibilityId("WebView"));
			webViewButton.Click();

			PerformZoomIn(132, 370, 112, 655, 90, 1050, 105, 785);
		}

		public void ScrollToText(string text)
		{
			_driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
		}

		public void PerformZoomIn(int ffStartX, int ffStartY, int ffEndX, int ffEndY, int sfStartX, int sfStartY, int sfEndX, int sfEndY)
		{
			var finger = new PointerInputDevice(PointerKind.Touch);
			var finger2 = new PointerInputDevice(PointerKind.Touch);
			var zoomFinger1 = new ActionSequence(finger);
			var zoomFinger2 = new ActionSequence(finger2);

			zoomFinger1.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, ffStartX, ffStartY, TimeSpan.Zero));

			zoomFinger1.AddAction(finger.CreatePointerDown(MouseButton.Left));

			zoomFinger1.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, ffEndX, ffEndY, TimeSpan.FromSeconds(1)));

			zoomFinger1.AddAction(finger.CreatePointerUp(MouseButton.Left));

			// actions for finger 2
			zoomFinger2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, sfStartX, sfStartY, TimeSpan.Zero));

			zoomFinger2.AddAction(finger2.CreatePointerDown(MouseButton.Left));

			zoomFinger2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, sfEndX, sfEndY, TimeSpan.FromSeconds(1)));

			zoomFinger2.AddAction(finger2.CreatePointerUp(MouseButton.Left));

			// Perform actions
			_driver.PerformActions(new List<ActionSequence>() { zoomFinger1, zoomFinger2 });
		}
	}
}