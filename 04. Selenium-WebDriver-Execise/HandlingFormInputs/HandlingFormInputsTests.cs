using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V124.Runtime;
using OpenQA.Selenium.Support.UI;

namespace HandlingFormInputs
{
	public class HandlingFormInputsTests
	{
		private WebDriver driver;

		[SetUp]
		public void Setup()
		{
			driver = new ChromeDriver();
			driver.Navigate().GoToUrl("https://practice.bpbonline.com/");
		}

		[TearDown]
		public void TearDown() 
		{
			driver.Quit();
			driver.Dispose();
		}

		[Test]
		public void HandlingFormInputs()
		{
			// click on My Account button
			var myAccountButton = driver.FindElements(By.XPath("//span[@class='ui-button-text']"))[2];
			myAccountButton.Click();

			// click Continue button
			driver.FindElement(By.LinkText("Continue")).Click();

			// select Male radio button
			driver.FindElement(By.XPath("//input[@type='radio'][@value='m']")).Click();

			// type in first name field
			driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='firstname']")).SendKeys("Simeon");

			// type in last name field
			driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='lastname']")).SendKeys("Savov");

			// type date field
			driver.FindElement(By.Id("dob")).SendKeys("01/21/1999");

			// generate random email and type in the email field
			Random random = new Random();
			int randomNum = random.Next(1000, 9999);
			String email = $"test{randomNum.ToString()}@test.com";

			driver.FindElement(By.Name("email_address")).SendKeys(email);

			// type company field
			driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='company']")).SendKeys("SoftUni");

			// type street address field
			driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='street_address']")).SendKeys("Boulevard Vitosha 12");

			// type suburb field
			driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='suburb']")).SendKeys("Sofia");

			// type post code field
			driver.FindElement(By.Name("postcode")).SendKeys("1000");

			// type city field
			driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='city']")).SendKeys("Sofia");

			// type state field
			driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='state']")).SendKeys("Sofia");

			// locate dropdown and select country
			new SelectElement(driver.FindElement(By.Name("country"))).SelectByText("Bulgaria");

			// type telephone number field
			driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='telephone']")).SendKeys("0899955678");

			// check newsletter checkbox
			driver.FindElement(By.XPath("//input[@name='newsletter']")).Click();

			// type password field
			driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='password']")).SendKeys("123456");

			// type re-password field
			driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='confirmation']")).SendKeys("123456");

			// submit the form
			driver.FindElements(By.XPath("//span[@class='ui-button-icon-primary ui-icon ui-icon-person']//following-sibling::span"))[1].Click();

			// assert confirmation title for success registration
			Assert.That(driver.FindElement(By.XPath("//div[@id='bodyContent']//h1")).Text, Is.EqualTo("Your Account Has Been Created!"));

			// click log off button
			driver.FindElement(By.LinkText("Log Off")).Click();

			// click continue to log off
			driver.FindElement(By.LinkText("Continue")).Click();

            // print in the console
            Console.WriteLine("User created successfully!");
        }
	}
}