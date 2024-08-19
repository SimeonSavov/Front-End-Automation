using OpenQA.Selenium;

namespace FoodyProject.Pages
{
	public class AddFoodPage : BasePage
	{
        public AddFoodPage(IWebDriver driver) : base(driver)
        {
        }

		public string Url = BaseUrl + "/Food/Add";

		public IWebElement FoodNameField => driver.FindElement(By.XPath("//input[@name='Name']"));

		public IWebElement FoodDescriptionField => driver.FindElement(By.XPath("//input[@name='Description']"));

		public IWebElement AddFoodButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-block fa-lg gradient-custom-2 mb-3']"));

		public IWebElement MainErrorMessage => driver.FindElement(By.XPath("//div[@class='text-danger validation-summary-errors']"));

		public IWebElement NameFoodErrorMessage => driver.FindElement(By.XPath("//span[text()='The Name field is required.']"));

		public IWebElement DescriptionFoodErrorMessage => driver.FindElement(By.XPath("//span[text()='The Description field is required.']"));

		public void AssertEmptyNameErrorMessage()
		{
			Assert.That(NameFoodErrorMessage.Text.Trim(), Is.EqualTo("The Name field is required."), "Name error message was not correct");
		}
		public void AssertEmptyDescriptionErrorMessage()
		{
			Assert.That(DescriptionFoodErrorMessage.Text.Trim(), Is.EqualTo("The Description field is required."), "Description error message was not correct");
		}

		public void OpenPage()
		{
			driver.Navigate().GoToUrl(Url);
		}
	}
}
