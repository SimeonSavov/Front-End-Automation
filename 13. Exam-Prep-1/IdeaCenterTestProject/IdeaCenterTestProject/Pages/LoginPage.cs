using OpenQA.Selenium;

namespace IdeaCenterTestProject.Pages
{
	public class LoginPage : BasePage
	{
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public string Url = BaseUrl + "/Users/Login";

        public IWebElement EmailInput => _driver.FindElement(By.XPath("//input[@name='Email']"));

        public IWebElement PasswordInput => _driver.FindElement(By.XPath("//input[@name='Password']"));

        public IWebElement SignInButton => _driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg btn-block']"));

        public void Login(string email, string password)
        {
            EmailInput.SendKeys(email);
            PasswordInput.SendKeys(password);
            SignInButton.Click();
        }

        public void OpenPage()
        {
            _driver.Navigate().GoToUrl(Url);
        }
    }
}
