using OpenQA.Selenium;

namespace FoodyProject.Pages
{
	public class LoginPage : BasePage
	{
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public string Url = BaseUrl + "/User/Login";

		public IWebElement UsernameField => driver.FindElement(By.XPath("//input[@name='Username']"));

		public IWebElement PasswordField => driver.FindElement(By.XPath("//input[@name='Password']"));

		public IWebElement LoginButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-block fa-lg gradient-custom-2 mb-3']"));

		public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

		public void PerformLogin(string username, string password)
		{
			UsernameField.Clear();
			UsernameField.SendKeys(username);

			PasswordField.Clear();
			PasswordField.SendKeys(password);

			LoginButton.Click();
		}

	}
}
