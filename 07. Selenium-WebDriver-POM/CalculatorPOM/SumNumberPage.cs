using OpenQA.Selenium;

namespace CalculatorPOM
{
	public class SumNumberPage
	{
		private readonly IWebDriver driver; // THIS.DRIVER

		public SumNumberPage(IWebDriver driver)
		{
			this.driver = driver;

			driver.Manage().Timeouts().ImplicitWait = 
				TimeSpan.FromSeconds(5);
		}

		public const string PageURL = "https://b7ed03fb-8278-4987-8830-d67fe696c3e7-00-3vx1qeba6rxme.janeway.replit.dev/";

		public IWebElement FieldNum1 => driver.FindElement(By.XPath("//input[@name='number1']"));
		
		public IWebElement FieldNum2 => driver.FindElement(By.XPath("//input[@name='number2']"));

		public IWebElement CalcButton => driver.FindElement(By.XPath("//div[@class='buttons-bar']//input[@value='Calculate']"));
		
		public IWebElement ResetButton => driver.FindElement(By.XPath("//div[@class='buttons-bar']//input[@value='Reset']"));
		
		public IWebElement ResultDiv => driver.FindElement(By.XPath("//body//form//div[@id='result']"));

		public void OpenPage()
		{
			driver.Navigate().GoToUrl(PageURL);
		}

		public void ResetForm()
		{
			ResetButton.Click();
		}

		public bool isFormEmpty()
		{
			return FieldNum1.Text + FieldNum2.Text + ResultDiv.Text == "";
		}

		public string AddNumbers(string num1, string num2)
		{
			FieldNum1.SendKeys(num1);
			FieldNum2.SendKeys(num2);
			CalcButton.Click();
			return ResultDiv.Text;
		}
	}
}
