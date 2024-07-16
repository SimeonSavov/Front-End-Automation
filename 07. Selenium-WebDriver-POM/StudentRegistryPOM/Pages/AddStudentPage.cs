using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V124.Storage;

namespace StudentRegistryPOM.Pages
{
	public class AddStudentPage : BasePage
	{
        public AddStudentPage(IWebDriver driver) : base(driver)
        {
        }

		public override string PageUrl => "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:82/add-student";

		public IWebElement FieldName => driver.FindElement(By.XPath("//form//div//input[@id='name']"));

		public IWebElement FieldEmail => driver.FindElement(By.XPath("//form//div//input[@id='email']"));

		public IWebElement AddButton => driver.FindElement(By.XPath("//form//button[@type='submit']"));

		public IWebElement ErrorMessage => driver.FindElement(By.CssSelector("body > div"));

		public void AddStudent(string name, string email)
		{
			this.FieldName.SendKeys(name);
			this.FieldEmail.SendKeys(email);
			this.AddButton.Click();
		}

		public string GetErrorMessage()
		{
			return ErrorMessage.Text;
		}
	}
}
