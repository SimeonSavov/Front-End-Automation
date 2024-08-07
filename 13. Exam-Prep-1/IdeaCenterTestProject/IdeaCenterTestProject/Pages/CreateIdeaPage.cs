using OpenQA.Selenium;

namespace IdeaCenterTestProject.Pages
{
	public class CreateIdeaPage : BasePage
	{
        public CreateIdeaPage(IWebDriver driver) : base(driver)
        {
        }

        public string Url = BaseUrl + "/Ideas/Create";

        public IWebElement TitleInput => _driver.FindElement(By.XPath("//input[@name='Title']"));

        public IWebElement ImgInput => _driver.FindElement(By.XPath("//input[@name='Url']"));

        public IWebElement DescriptionInput => _driver.FindElement(By.XPath("//textarea[@name='Description']"));

        public IWebElement CreateButton => _driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg']"));

        public IWebElement MainMessage => _driver.FindElement(By.XPath("//div[@class='text-danger validation-summary-errors']//li"));

        public IWebElement TitleErrorMessage => _driver.FindElements(By.XPath("//span[@class='text-danger field-validation-error']"))[0];

        public IWebElement DescriptionErrorMessage => _driver.FindElements(By.XPath("//span[@class='text-danger field-validation-error']"))[1];

        public void CreateIdea(string title, string imageUrl, string description)
        {
            TitleInput.SendKeys(title);
            ImgInput.SendKeys(imageUrl);
            DescriptionInput.SendKeys(description);
            CreateButton.Click();
        }

        public void AssertErrorMessages()
        {
            Assert.True(MainMessage.Text.Equals("Unable to create new Idea!"), "Main error message is not as expected");

            Assert.True(TitleErrorMessage.Text.Equals("The Title field is required."), "Title error message is not as expected");

            Assert.True(DescriptionErrorMessage.Text.Equals("The Description field is required."), "Description error message is not as expected");
        }

        public void OpenPage()
        {
            _driver.Navigate().GoToUrl(Url);
        }
	}
}
