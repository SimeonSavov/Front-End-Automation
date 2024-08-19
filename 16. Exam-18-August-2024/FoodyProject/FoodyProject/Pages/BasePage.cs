using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FoodyProject.Pages
{
	public class BasePage
	{
		protected IWebDriver driver;

		protected WebDriverWait wait;

		protected static string BaseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:85";

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

		public IWebElement LoginLink => driver.FindElement(By.XPath("//a[@class='nav-link' and text()='Log In']"));

		public IWebElement MyProfileLink => driver.FindElement(By.XPath("//a[@class='nav-link mx-3']"));

		public IWebElement AddFoodLink => driver.FindElement(By.XPath("//a[@class='nav-link' and text()='Add Food']"));

		public IWebElement LogoutLink => driver.FindElement(By.XPath("//a[@class='nav-link' and text()='Logout']"));
    }	
}
