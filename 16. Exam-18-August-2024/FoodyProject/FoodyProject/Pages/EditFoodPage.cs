using OpenQA.Selenium;

namespace FoodyProject.Pages
{
	public class EditFoodPage : BasePage
	{
        public EditFoodPage(IWebDriver driver) : base(driver)
        {
        }

		public string Url = BaseUrl + "/Food/Edit";

		public IWebElement EditFoodNameField => driver.FindElement(By.XPath("//input[@name='Name']"));

		public IWebElement EditFoodDescriptionField => driver.FindElement(By.XPath("//input[@name='Description']"));

		public IWebElement EditFoodButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-block fa-lg gradient-custom-2 mb-3']"));

		public void OpenPage()
		{
			driver.Navigate().GoToUrl(Url);
		}
	}
}
