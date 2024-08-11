using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace RevueCraftersTestProject
{
	public class RevueTests
	{
		private readonly static string BaseUrl = "https://d3s5nxhwblsjbi.cloudfront.net";
		private IWebDriver driver;
		private Actions actions;

		private string lastCreatedTitle;
		private string lastCreatedDescription;

		[OneTimeSetUp]
		public void Setup()
		{
			var chromeOptions = new ChromeOptions();
			chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

			driver = new ChromeDriver(chromeOptions);
			driver.Manage().Window.Maximize();
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			// Log in to the application
			driver.Navigate().GoToUrl($"{BaseUrl}/Users/Login#loginForm");
			var loginForm = driver.FindElement(By.Id("loginForm"));

			// Scroll to the login form using Actions class
			Actions actions = new Actions(driver);
			actions.MoveToElement(loginForm).Perform();

			driver.FindElement(By.LinkText("Login")).Click();
			driver.FindElement(By.Id("form3Example3")).SendKeys("user@gmail.com");
			driver.FindElement(By.Id("form3Example4")).SendKeys("123456");
			driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-block.mb-4")).Click();
		}

		[OneTimeTearDown]
		public void Teardown()
		{
			driver.Quit();
			driver.Dispose();
		}

		[Test, Order(1)]
		public void CreateRevue_WithInvalidData_Test()
		{
			string invalidRevueTitle = "";
			string invalidRevueDescription = "";

			driver.Navigate().GoToUrl($"{BaseUrl}/Revue/Create#createRevue");

			var createForm = driver.FindElement(By.CssSelector("div.card-body.p-md-5"));
			Actions actions = new Actions(driver);
			actions.MoveToElement(createForm).Perform();

			driver.FindElement(By.Id("form3Example1c")).SendKeys(invalidRevueTitle);
			driver.FindElement(By.Id("form3Example4cd")).SendKeys(invalidRevueDescription);
			driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-lg")).Click();

			string currentUrl = driver.Url;
			Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Revue/Create#createRevue"), "The page should remain on the creation page with invalid data.");

			var mainErrorMessage = driver.FindElement(By.CssSelector(".validation-summary-errors li"));
			Assert.That(mainErrorMessage.Text.Trim(), Is.EqualTo("Unable to create new Revue!"), "The main error message is not displayed as expected.");
		}

		[Test, Order(2)]
		public void CreateRevue_WithRandomData_Test()
		{
			driver.Navigate().GoToUrl($"{BaseUrl}/Revue/Create#createRevue");

			var createForm = driver.FindElement(By.CssSelector("div.card-body.p-md-5"));
			Actions actions = new Actions(driver);
			actions.MoveToElement(createForm).Perform();

			lastCreatedTitle = GenerateRandomString(6);
			var titleInput = driver.FindElement(By.Id("form3Example1c"));
			titleInput.Clear();
			titleInput.SendKeys(lastCreatedTitle);

			lastCreatedDescription = GenerateRandomString(10);
			var descriptionInput = driver.FindElement(By.Id("form3Example4cd"));
			descriptionInput.Clear();
			descriptionInput.SendKeys(lastCreatedDescription);

			driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-lg")).Click();

			var currentUrl = driver.Url;
			Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Revue/MyRevues#createRevue"));

			var revues = driver.FindElements(By.CssSelector(".card.mb-4"));
			var lastRevueTitle = revues.Last().FindElement(By.CssSelector(".text-muted")).Text;

			Assert.That(lastRevueTitle, Is.EqualTo(lastCreatedTitle));
		}

		[Test, Order(3)]
		public void SearchRevue_ByTitle_Test()
		{
			driver.Navigate().GoToUrl($"{BaseUrl}/Revue/MyRevues#createRevue");

			var searchField = driver.FindElement(By.Id("keyword"));
			Actions actions = new Actions(driver);
			actions.MoveToElement(searchField).Perform();

			searchField.SendKeys(lastCreatedTitle);

			driver.FindElement(By.Id("search-button")).Click();

			var searchResultRevueTitle = driver.FindElement(By.CssSelector(".text-muted")).Text;
			Assert.That(searchResultRevueTitle, Is.EqualTo(lastCreatedTitle));
		}

		[Test, Order(4)]
		public void EditLastCreatedRevue_Test()
		{
			driver.Navigate().GoToUrl($"{BaseUrl}/Revue/MyRevues#createRevue");

			var revues = driver.FindElements(By.CssSelector(".card.mb-4"));
			Assert.That(revues.Count(), Is.GreaterThan(0));

			var lastRevue = revues.Last();

			Actions actions = new Actions(driver);
			actions.MoveToElement(lastRevue).Perform();

			driver.FindElement(By.XPath($"//div[text()='{lastCreatedTitle}']/..//a[text()='Edit']")).Click();

			var createForm = driver.FindElement(By.CssSelector("div.card-body.p-md-5"));
			actions.MoveToElement(createForm).Perform();

			lastCreatedTitle = GenerateRandomString(6) + "Edited!";
			var titleInput = driver.FindElement(By.Id("form3Example1c"));
			titleInput.Clear();
			titleInput.SendKeys(lastCreatedTitle);

			driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-lg")).Click();

			var currentUrl = driver.Url;
			Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Revue/MyRevues"));

			var revuesResult = driver.FindElements(By.CssSelector(".card.mb-4"));
			var lastRevueTitle = revuesResult.Last().FindElement(By.CssSelector(".text-muted")).Text;
			Assert.That(lastRevueTitle, Is.EqualTo(lastCreatedTitle));
		}

		[Test, Order(5)]
		public void DeleteLastCreatedRevue_Test()
		{
			driver.Navigate().GoToUrl($"{BaseUrl}/Revue/MyRevues#createRevue");

			var revues = driver.FindElements(By.CssSelector(".card.mb-4"));
			Assert.That(revues.Count(), Is.GreaterThan(0));

			var lastRevue = revues.Last();

			Actions actions = new Actions(driver);
			actions.MoveToElement(lastRevue).Perform();

			driver.FindElement(By.XPath($"//div[text()='{lastCreatedTitle}']/..//a[text()='Delete']")).Click();

			var currentUrl = driver.Url;
			Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Revue/MyRevues"));

			var revuesResult = driver.FindElements(By.CssSelector(".card.mb-4"));
			Assert.That(revuesResult.Count(), Is.LessThan(revues.Count()));

			var lastRevueTitle = revuesResult.Last().FindElement(By.CssSelector(".text-muted")).Text;
			Assert.That(lastRevueTitle, !Is.EqualTo(lastCreatedTitle));
		}

		[Test, Order(6)]
		public void SearchNonExistingRevue_Test()
		{
			driver.Navigate().GoToUrl($"{BaseUrl}/Revue/MyRevues");

			var searchField = driver.FindElement(By.Id("keyword"));
			Actions actions = new Actions(driver);
			actions.MoveToElement(searchField).Perform();

			searchField.SendKeys("Non existing Revue");

			driver.FindElement(By.Id("search-button")).Click();

			var noResultMessage = driver.FindElement(By.XPath("//span[@class='col-12 text-muted']")).Text;

			Assert.That(noResultMessage, Is.EqualTo("No Revues yet!"));
		}


		private string GenerateRandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var random = new Random();
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}