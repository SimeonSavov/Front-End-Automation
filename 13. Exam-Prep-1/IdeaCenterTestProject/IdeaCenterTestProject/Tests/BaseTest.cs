using IdeaCenterTestProject.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace IdeaCenterTestProject.Tests
{
	public class BaseTest
	{
		public IWebDriver _driver;

		public LoginPage _loginPage;

		public CreateIdeaPage _createIdeaPage;

		public MyIdeasPage _myIdeasPage;

		public IdeasReadPage _ideasReadPage;

		public IdeasEditPage _ideasEditPage;

		[OneTimeSetUp]
		public void SetUp()
		{
			var chromeOptions = new ChromeOptions();
			chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
			chromeOptions.AddArgument("--disable-search-engine-choice-screen");

			_driver = new ChromeDriver(chromeOptions);
			_driver.Manage().Window.Maximize();
			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			_loginPage = new LoginPage(_driver);
			_createIdeaPage = new CreateIdeaPage(_driver);
			_myIdeasPage = new MyIdeasPage(_driver);
			_ideasReadPage = new IdeasReadPage(_driver);
			_ideasEditPage = new IdeasEditPage(_driver);

			_loginPage.OpenPage();
			_loginPage.Login("user@gmail.com", "123456");
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			_driver.Quit();
			_driver.Dispose();
		}

		public string GenerateRandomString(int length)
		{
			const string chars = "abcdsffsadsadsa";
			var random = new Random();
			return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
