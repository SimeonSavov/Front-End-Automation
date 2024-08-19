using FoodyProject.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;

namespace FoodyProject.Tests
{
	public class BaseTest
	{
		public IWebDriver driver;

		public LoginPage loginPage;

		public AddFoodPage addFoodPage;

		public AllFoodsPage allFoodPage;

		public EditFoodPage editFoodPage;

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
			addFoodPage = new AddFoodPage(driver);
			allFoodPage = new AllFoodsPage(driver);
			editFoodPage = new EditFoodPage(driver);

			// Login functionality
			loginPage.OpenPage();
			loginPage.PerformLogin("user12", "123456");
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			driver.Quit();
			driver.Dispose();
		}

		public string GenerateRandomFoodName()
		{
			var random = new Random();
			return "Food: " + random.Next(10000, 100000);
		}

		public string GenerateRandomFoodDescription()
		{
			var random = new Random();
			return "Description: " + random.Next(10000, 100000);
		}
	}
}
