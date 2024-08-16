using MovieCatalogProject.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MovieCatalogProject.Tests
{
	public class BaseTest
	{
		public IWebDriver driver;

		public LoginPage loginPage;

		public AddMoviePage addMoviePage;

		public AllMoviesPage allMoviesPage;

		public EditMoviePage editMoviePage;

		public WatchedMoviesPage watchedMoviesPage;

		public DeleteMoviePage deleteMoviePage;

		[OneTimeSetUp]
		public void SetUp()
		{
			var chromeOptions = new ChromeOptions();
			chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
			chromeOptions.AddArgument("--disable-search-engine-choice-screen");

			driver = new ChromeDriver(chromeOptions);
			driver.Manage().Window.Maximize();
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			loginPage = new LoginPage(driver);
			addMoviePage = new AddMoviePage(driver);
			allMoviesPage = new AllMoviesPage(driver);
			editMoviePage = new EditMoviePage(driver);
			watchedMoviesPage = new WatchedMoviesPage(driver);
			deleteMoviePage = new DeleteMoviePage(driver);

			// Login functionality
			loginPage.OpenPage();
			loginPage.PerformLogin("test121@test.bg", "123456");
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			driver.Quit();
			driver.Dispose();
		}

		public string GenerateRandomTitle()
		{
			var random = new Random();
			return "TITLE: " + random.Next(10000, 100000);
		}

		public string GenerateRandomDescription()
		{
			var random = new Random();
			return "DESCRIPTION: " + random.Next(10000, 100000);
		}
	}
}
