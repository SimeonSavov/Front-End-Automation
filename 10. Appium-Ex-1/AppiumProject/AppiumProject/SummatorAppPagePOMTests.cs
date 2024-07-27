using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;

namespace AppiumProject
{
	public class SummatorAppPagePOMTests
	{
		private AndroidDriver _driver;
		private AppiumLocalService _localService;
		private SummatorPage _summatorPage;

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

			_summatorPage = new SummatorPage(_driver);	
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
			var result = _summatorPage.Calculate("10", "10");

			// Assert
			Assert.That(result, Is.EqualTo("20"));
		}

		[Test]
		public void Test_WithInvalidData_ClickOnlyCalcButton()
		{
			_summatorPage.ClearFields();

			_summatorPage.calcButton.Click();

			// Assert
			Assert.That(_summatorPage.resultField.Text, Is.EqualTo("error"));
		}

		[Test]
		public void Test_WithInvalidData_FillOnlyFirstField()
		{
			_summatorPage.ClearFields();

			_summatorPage.field1.SendKeys("10");

			_summatorPage.calcButton.Click();

			// Assert
			Assert.That(_summatorPage.resultField.Text, Is.EqualTo("error"));
		}

		[Test]
		public void Test_WithInvalidData_FillOnlySecondField()
		{
			_summatorPage.ClearFields();

			_summatorPage.field2.SendKeys("10");

			_summatorPage.calcButton.Click();

			// Assert
			Assert.That(_summatorPage.resultField.Text, Is.EqualTo("error"));
		}

		[Test]
		[TestCase("10", "20", "30")]
		[TestCase("100", "100", "200")]
		[TestCase("1000", "1000", "2000")]
		[TestCase("0", "1000", "1000")]
		[TestCase("10.9", "10.1", "21.0")]
		public void Test_WithValidData_Parametrized(string input1, string input2, string expectedResult)
		{
			var result = _summatorPage.Calculate(input1, input2);

			Assert.That(result, Is.EqualTo("error"));
		}
	}
}
