using OpenQA.Selenium;

namespace SwagLabsPOM.Pages
{
	public class LoginPage : BasePage
	{
        private readonly By usernameField = By.XPath("//input[@id='user-name']");

		private readonly By passwordField = By.XPath("//input[@id='password']");

		private readonly By loginButton = By.XPath("//input[@id='login-button']");

		private readonly By errorMessage = By.XPath("//div[@class='error-message-container error']//h3");

		public LoginPage(IWebDriver driver) : base(driver)
        {
        }

		public void FillUsername(string username)
		{
			Type(usernameField, username);
		}
		public void FillPassword(string password)
		{
			Type(passwordField, password);
		}

		public void ClickLoginButton()
		{
			Click(loginButton);
		}

		public string GetErrorMessage()
		{
			return GetText(errorMessage);
		}

		public void LoginUser(string username, string password)
		{
			FillUsername(username);
			FillPassword(password);
			ClickLoginButton();

			// Check if no error message is visible
		}	  
    }
}
