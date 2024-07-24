using OpenQA.Selenium;

namespace SwagLabsPOM.Pages
{
	public class HiddenMenuPage : BasePage
	{
        protected readonly By hamburgerMenuButton = By.Id("react-burger-menu-btn");

        protected readonly By logoutButton = By.XPath("//a[text()='Logout']");

        public HiddenMenuPage(IWebDriver driver) : base(driver)
        {  
        }

        public void CLickHamburgerMenuButton()
        {
            Click(hamburgerMenuButton);
        }

		public void CLickLogoutButton()
		{
			Click(logoutButton);
		}

        public bool IsMenuOpen()
        {
            return FindElement(logoutButton).Displayed;
        }
	}
}
