using OpenQA.Selenium;

namespace FoodyProject.Pages
{
	public class AllFoodsPage : BasePage
	{
        public AllFoodsPage(IWebDriver driver) : base(driver)
        { 
        }

		public string Url = BaseUrl;
		public IReadOnlyCollection<IWebElement> AllFoods => driver.FindElements(By.XPath("//section[@id='scroll']"));

		public IWebElement LastFoodTitle => AllFoods.Last().FindElement(By.XPath(".//h2"));

		public IWebElement LastFoodEditButton => AllFoods.Last().FindElement(By.XPath(".//a[text()='Edit']"));

		public IWebElement LastFoodDeleteButton => AllFoods.Last().FindElement(By.XPath(".//a[text()='Delete']"));

		public IWebElement SearchInput => driver.FindElement(By.XPath("//input[@name='keyword']"));

		public IWebElement SearchButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary rounded-pill mt-5 col-2']"));

		public IWebElement NoFoodsMessage => driver.FindElement(By.XPath("//h2[@class='display-4']"));

		public IWebElement AddFoodButtonAfterSearch => driver.FindElement(By.XPath("//a[@class='btn btn-primary btn-xl rounded-pill mt-5']"));


		public void AssertNoFoodMessage()
		{
			Assert.That(NoFoodsMessage.Text.Trim, Is.EqualTo("There are no foods :("));
		}

		public void AssertAddFoodButtonIsPresent()
		{
			Assert.That(AddFoodButtonAfterSearch.Displayed, Is.True);
		}


		public void OpenPage()
		{
			driver.Navigate().GoToUrl(Url);
		}
	}
}
