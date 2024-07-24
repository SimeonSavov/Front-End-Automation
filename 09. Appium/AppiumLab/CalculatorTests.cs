using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace AppiumLab
{
	public class CalculatorTests
	{
		private AndroidDriver _driver;
		private AppiumLocalService _service;

		[SetUp]
		public void Setup()
		{
			_service = new AppiumServiceBuilder()
				.WithIPAddress("127.0.0.1")
				.UsingPort(4723)
				.Build();

			AppiumOptions options = new AppiumOptions();
			options.App = @"C:\Users\simo7\OneDrive\Работен плот\Front-End Automation\09. Appium-Lab\com.example.androidappsummator.apk";
			options.PlatformName = "Android";
			options.DeviceName = "Pixel 7";
			options.AutomationName = "UIAutomator2";

			_driver = new AndroidDriver(_service, options);
		}

		[TearDown]
		public void TearDown()
		{
			_driver.Quit();
			_driver.Dispose();
			_service.Dispose();
		}

		[Test]
		public void Test_Valid_Calculation()
		{
			// Find First Input Field
			var firstInput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
			firstInput.Clear();
			firstInput.SendKeys("2");

			// Find Second Input Field
			var secondInput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
			secondInput.Clear();
			secondInput.SendKeys("3");

			// Find Calc Button
			var calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
			calcButton.Click();

			// Find Result Text
			var resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

			// Assert The Result -> 2 + 3 = 5
			Assert.That(resultField.Text, Is.EqualTo("5"), "The Result is not correct");
		}

		[Test]
		public void Test_Invalid_Calculation_ResultIsError()
		{
			// Find First Input Field
			var firstInput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
			firstInput.Clear();
			firstInput.SendKeys("2");

			// Find Second Input Field
			var secondInput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
			secondInput.Clear();
			secondInput.SendKeys(".");

			// Find Calc Button
			var calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
			calcButton.Click();

			// Find Result Text
			var resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

			// Assert The Result -> 2 + . = error
			Assert.That(resultField.Text, Is.EqualTo("error"), "The Result is not correct");
		}
	}
}