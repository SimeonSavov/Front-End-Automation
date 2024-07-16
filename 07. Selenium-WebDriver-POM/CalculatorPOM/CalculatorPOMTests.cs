using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CalculatorPOM
{
	public class CalculatorPOMTests
	{
		public IWebDriver driver;

		[SetUp]
		public void Setup()
		{
			driver = new ChromeDriver();
		}

		[TearDown]
		public void TearDown()
		{
			driver.Quit();
			driver.Dispose();
		}

		[Test]
		public void AddTwoNumbers_ValidResult()
		{
			var calculatorPage = new SumNumberPage(driver);
			calculatorPage.OpenPage();

			string result = calculatorPage.AddNumbers("1", "2");

			Assert.That(result, Is.EqualTo("Sum: 3"));
		}

		[Test]
		public void AddTwoNumbers_InvalidResult()
		{
			SumNumberPage sumNumberPage = new SumNumberPage(driver);
			sumNumberPage.OpenPage();

			string result = sumNumberPage.AddNumbers("hello", "world");
			Assert.That(result, Is.EqualTo("Sum: invalid input"));
		}

		[Test]
		public void ResetForm()
		{
			SumNumberPage sumNumberPage = new SumNumberPage(driver);
			sumNumberPage.OpenPage();

			string result = sumNumberPage.AddNumbers("1", "2");
			Assert.That(result, Is.EqualTo("Sum: 3"));

			sumNumberPage.ResetForm();
			Assert.IsTrue(sumNumberPage.isFormEmpty());
		}
	}
}