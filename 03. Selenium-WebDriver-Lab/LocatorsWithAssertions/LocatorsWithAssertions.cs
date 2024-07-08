using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LocatorsWithAssertions
{
	public class LocatorsWithAssertions
	{
		private IWebDriver driver;
		private string baseUrl = "file:///C:/path/to/the/file/SimpleForm/Locators.html";

		[OneTimeSetUp]
		public void Setup()
		{
			var options = new ChromeOptions();
			options.AddArgument("--headless=new");
			options.AddArgument("--window-size=1920,1200");
			driver = new ChromeDriver(options);

			driver.Url = baseUrl;
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			driver.Quit();
		}

		[Test]
		public void TestBasicLocators()
		{
			// Locate the "Last Name" input field and verify its value
			var lastNameField = driver.FindElement(By.Id("lname"));
			Assert.That(lastNameField.GetAttribute("value"), Is.EqualTo("Vega"));

			// Locate the "Newsletter" checkbox and verify it is not selected
			var newsLetterCheckbox = driver.FindElement(By.Name("newsletter"));
			Assert.That(newsLetterCheckbox.Selected, Is.False);

			// Locate the anchor tag and verify its text
			var linkElement = driver.FindElement(By.TagName("a"));
			Assert.That(linkElement.Text, Is.EqualTo("Softuni Official Page"));

			// Locate the element with class name "information" and verify its background color
			var informationElement = driver.FindElement(By.ClassName("information"));
			var backgroundColor = informationElement.GetCssValue("background-color");
			Assert.That(backgroundColor, Is.EqualTo("rgba(255, 255, 255, 1)")); //This value may vary base on the browser
		}

		[Test]
		public void TestTextLinkLocators()
		{
			// Locate the link by its full text and verify its href attribute
			var fullLinkText = driver.FindElement(By.LinkText("Softuni Official Page"));
			Assert.That(fullLinkText.GetAttribute("href"), Is.EqualTo("http://www.softuni.bg/"));

			// Locate the link by partial text and verify its displayed text
			var partialLinkText = driver.FindElement(By.PartialLinkText("Official Page"));
			Assert.That(partialLinkText.Text, Is.EqualTo("Softuni Official Page"));
		}

		[Test]
		public void TestCssSelectors()
		{
			// Locate the "First name" input field by ID and verify its value
			var firstNameField = driver.FindElement(By.CssSelector("#fname"));
			Assert.That(firstNameField.GetAttribute("value"), Is.EqualTo("Vincent"));

			// Locate the "First name" input field by name attribute and verify its value
			var firstNameFieldByName = driver.FindElement(By.CssSelector("input[name='fname']"));
			Assert.That(firstNameFieldByName.GetAttribute("value"), Is.EqualTo("Vincent"));

			// Locate the "Phone Number" input field by CSS selector and verify it is displayed
			var phoneNumberField = driver.FindElement(By.CssSelector("div.additional-info > p > input[type='text']"));
			Assert.That(phoneNumberField.Displayed, Is.True);

			// Locate the "Phone Number" input field using a more specific CSS selector and verify it is displayed
			var phoneNumberFieldSpecific = driver.FindElement(By.CssSelector("form div.additional-info input[type='text']"));
			Assert.That(phoneNumberFieldSpecific.Displayed, Is.True);
		}

		[Test]
		public void TestXPath()
		{
			// Locate the male radio button using absolute XPath and verify its value attribute
			var maleRadioButton = driver.FindElement(By.XPath("/html/body/form/input[1]"));
			Assert.That(maleRadioButton.GetAttribute("value"), Is.EqualTo("m"));

			// Locate the male radio button using relative XPath and verify its value attribute
			var maleRadioButtonRelative = driver.FindElement(By.XPath("//input[@value='m']"));
			Assert.That(maleRadioButtonRelative.GetAttribute("value"), Is.EqualTo("m"));

			// Locate the last name input field using relative XPath and verify its value
			var lastNameField = driver.FindElement(By.XPath("//input[@name='lname']"));
			Assert.That(lastNameField.GetAttribute("value"), Is.EqualTo("Vega"));

			// Locate the newsletter checkbox using relative XPath and verify its type attribute
			var newsletterCheckbox = driver.FindElement(By.XPath("//input[@type='checkbox']"));
			Assert.That(newsletterCheckbox.GetAttribute("type"), Is.EqualTo("checkbox"));

			// Locate the submit button using relative XPath and verify its value attribute
			var submitButton = driver.FindElement(By.XPath("//input[@class='button']"));
			Assert.That(submitButton.GetAttribute("value"), Is.EqualTo("Submit"));

			// Locate the phone number input field within additional info using relative XPath and verify it is displayed
			var phoneNumberField = driver.FindElement(By.XPath("//div[@class='additional-info']//input[@type='text']"));
			Assert.That(phoneNumberField.Displayed, Is.True);
		}
	}
}