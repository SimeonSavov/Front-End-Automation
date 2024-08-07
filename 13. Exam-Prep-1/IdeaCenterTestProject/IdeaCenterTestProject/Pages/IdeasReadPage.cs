using OpenQA.Selenium;

namespace IdeaCenterTestProject.Pages
{
	public class IdeasReadPage : BasePage
	{
        public IdeasReadPage(IWebDriver driver) : base(driver)
        {
        }

        public string Url = BaseUrl + "/Ideas/Read";

		public IWebElement IdeaTitle => _driver.FindElement(By.XPath("//h1[@class='mb-0 h4']"));

		public IWebElement IdeaDescription => _driver.FindElement(By.XPath("//p[@class='offset-lg-3 col-lg-6']"));
	}
}
