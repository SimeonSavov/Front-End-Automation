using OpenQA.Selenium;

namespace MovieCatalogProject.Pages
{
	public class AddMoviePage : BasePage
	{
        public AddMoviePage(IWebDriver driver) : base(driver)
        {
        }

		public string Url = BaseUrl + "/Catalog/Add#add";

		public IWebElement TitleInput => driver.FindElement(By.XPath("//input[@name='Title']"));

		public IWebElement DescriptionInput => driver.FindElement(By.XPath("//textarea[@name='Description']"));

		public IWebElement PosterURLInput => driver.FindElement(By.XPath("//input[@name='PosterUrl']"));

		public IWebElement YouTubeTrailerInput => driver.FindElement(By.XPath("//input[@name='TrailerLink']"));

		public IWebElement MarkAsWatchedCheckbox => driver.FindElement(By.XPath("//input[@class='form-check-input']"));

		public IWebElement AddButton => driver.FindElement(By.XPath("//button[@class='btn warning']"));

		public IWebElement ErrorMessage => driver.FindElement(By.XPath("//div[@class='toast-message']"));

		public void AssertEmptyTitleMessage()
		{
			Assert.That(ErrorMessage.Text.Trim(), Is.EqualTo("The Title field is required."), "Title error message was not expected");
		}

		public void AssertEmptyDescriptionMessage()
		{
			Assert.That(ErrorMessage.Text.Trim(), Is.EqualTo("The Description field is required."), "Description error message was not expected");
		}

		public void OpenPage()
		{
			driver.Navigate().GoToUrl(Url);
		}
	}
}
