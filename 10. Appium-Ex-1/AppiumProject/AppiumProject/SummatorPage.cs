using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace AppiumProject
{
	public class SummatorPage
	{
		private readonly AndroidDriver _driver;

        public SummatorPage(AndroidDriver driver)
        {
            _driver = driver;
        }

		public IWebElement field1 => _driver.FindElement(MobileBy.Id("editText1"));

		public IWebElement field2 => _driver.FindElement(MobileBy.Id("editText2"));

		public IWebElement calcButton => _driver.FindElement(MobileBy.Id("buttonCalcSum"));

		public IWebElement resultField => _driver.FindElement(MobileBy.Id("editTextSum"));

		public string Calculate(string number1, string number2)
		{
			ClearFields();

			field1.SendKeys(number1);
			field2.SendKeys(number2);
			calcButton.Click();

			return resultField.Text;
		}

		public void ClearFields()
		{
			field1.Clear();
			field2.Clear();
		}
	}
}
