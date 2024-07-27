using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace AppiumProject
{
	public class SummatorAppTests
	{
		private AndroidDriver _driver;
		private AppiumLocalService _localService;

		[OneTimeSetUp]
		public void Setup()
		{
			_localService = new AppiumServiceBuilder()
				.WithIPAddress("127.0.0.1")
				.UsingPort(4723)
				.Build();
			_localService.Start();

			var androidOptions = new AppiumOptions
			{
				PlatformName = "Android",
				AutomationName = "UiAutomator2",
				DeviceName = "Pixel 7",
				App = @"C:\\SoftUni - Automation\\10. Appium-Ex\\com.example.androidappsummator.apk",
				PlatformVersion = "14"
			};

			_driver = new AndroidDriver(_localService, androidOptions);
		}

		[OneTimeTearDown]
		public void Teardown()
		{
			_driver?.Quit();
			_driver?.Dispose();
			_localService?.Dispose();
		}

		[Test]
		public void Test_WithValidData()
		{
			// First input
			var field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
			field1.Clear();
			field1.SendKeys("10");

			// Second input
			var field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
			field2.Clear();
			field2.SendKeys("10");

			// Calc button
			var calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
			calcButton.Click();

			// Result field
			var resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

			// Assert
			Assert.That(resultField.Text, Is.EqualTo("20"));
		}

		[Test]
		public void Test_WithInvalidData_ClickOnlyCalcButton()
		{
			// First input
			var field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
			field1.Clear();

			// Second input
			var field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
			field2.Clear();

			// Calc button
			var calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
			calcButton.Click();

			// Result field
			var resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

			// Assert
			Assert.That(resultField.Text, Is.EqualTo("error"));
		}

		[Test]
		public void Test_WithInvalidData_FillOnlyFirstField()
		{
			// First input
			var field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
			field1.Clear();
			field1.SendKeys("10");

			// Second input
			var field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
			field2.Clear();

			// Calc button
			var calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
			calcButton.Click();

			// Result field
			var resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

			// Assert
			Assert.That(resultField.Text, Is.EqualTo("error"));
		}

		[Test]
		public void Test_WithInvalidData_FillOnlySecondField()
		{
			// First input
			var field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
			field1.Clear();

			// Second input
			var field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
			field2.Clear();
			field2.SendKeys("10");

			// Calc button
			var calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
			calcButton.Click();

			// Result field
			var resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

			// Assert
			Assert.That(resultField.Text, Is.EqualTo("error"));
		}

		[Test]
		[TestCase("10", "20", "30")]
		[TestCase("100", "100", "200")]
		[TestCase("1000", "1000", "2000")]
		[TestCase("0", "1000", "1000")]
		[TestCase("10.9", "10.1", "21.0")]
		public void Test_WithValidData_Parametrized(string input1, string input2, string expectedResult)
		{
			// First input
			var field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
			field1.Clear();
			field1.SendKeys(input1);

			// Second input
			var field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
			field2.Clear();
			field2.SendKeys(input2);

			// Calc button
			var calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
			calcButton.Click();

			// Result field
			var resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

			// Assert
			Assert.That(resultField.Text, Is.EqualTo(expectedResult));
		}
	}
}