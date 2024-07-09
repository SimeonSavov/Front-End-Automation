using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DataDrivenCalc
{
	public class CalculatorTests
	{
		private WebDriver driver;
		private IWebElement textBoxNumber1;
		private IWebElement textBoxNumber2;
		private IWebElement dropdownOperations;
		private IWebElement calcButton;
		private IWebElement resetButton;
		private IWebElement divResult;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			driver = new ChromeDriver();
			driver.Navigate().GoToUrl("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/");
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
		}

		[SetUp]
		public void Setup()
		{
			textBoxNumber1 = driver.FindElement(By.Id("number1"));
			textBoxNumber2 = driver.FindElement(By.XPath("//input[@id='number2']"));
			dropdownOperations = driver.FindElement(By.XPath("//label[@for='operation']//following-sibling::select"));
			calcButton = driver.FindElement(By.Id("calcButton"));
			resetButton = driver.FindElement(By.Id("resetButton"));
			divResult = driver.FindElement(By.Id("result"));
		}

		[OneTimeTearDown]
		public void OneTimeTeardown()
		{
			driver.Quit();
			driver.Dispose();
		}

		public void PerformTestLogic(string firstNumber, string operation, string secondNumber, string expected)
		{
			// click reset button
			resetButton.Click();

			// check value of firstNumber if is empty or null
			if (!string.IsNullOrEmpty(firstNumber))
			{
				textBoxNumber1.SendKeys(firstNumber);
			}

			// check value of secondNumber if is empty or null
			if (!string.IsNullOrEmpty(secondNumber))
			{
				textBoxNumber2.SendKeys(secondNumber);
			}

			// check value of operation if is empty or null
			if (!string.IsNullOrEmpty(operation))
			{
				new SelectElement(dropdownOperations).SelectByText(operation);
			}

			// click the calculate button
			calcButton.Click();

			// assert the result
			Assert.That(divResult.Text, Is.EqualTo(expected));
		}

		[Test]
		[TestCase("5", "+ (sum)", "10", "Result: 15")]
		[TestCase("3.5", "- (subtract)", "1.2", "Result: 2.3")]
		[TestCase("2e2", "* (multiply)", "1.5", "Result: 300")]
		[TestCase("5", "/ (divide)", "0", "Result: Infinity")]
		[TestCase("invalid", "+ (sum)", "10", "Result: invalid input")]
		public void TestTheLogicOfCalculator(string firstNumber, string operation, string secondNumber, string expected)
		{
			PerformTestLogic(firstNumber, operation, secondNumber, expected);
		}
	}
}