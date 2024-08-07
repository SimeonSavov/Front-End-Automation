using OpenQA.Selenium;

namespace IdeaCenterTestProject.Pages
{
	public class IdeasEditPage : BasePage
	{
        public IdeasEditPage(IWebDriver driver) : base(driver)
        {
        }

		public string Url = BaseUrl + "/Ideas/Edit";

		public IWebElement TitleInput => _driver.FindElement(By.XPath("//input[@name='Title']"));

		public IWebElement ImgInput => _driver.FindElement(By.XPath("//input[@name='Url']"));

		public IWebElement DescriptionInput => _driver.FindElement(By.XPath("//textarea[@name='Description']"));

		public IWebElement EditButton => _driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg']"));
	}
}
