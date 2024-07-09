using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace WorkingWithWebTables
{
	public class WebTablesTests
	{
		private WebDriver driver;

		[SetUp]
		public void Setup()
		{
			driver = new ChromeDriver();
			driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
		}

		[TearDown]
		public void Teardown()
		{
			driver.Quit();
			driver.Dispose();
		}

		[Test]
		public void WorkingWithTableElements()
		{
			// locate the table
			IWebElement productsTable = driver.FindElement(By.XPath("//div[@class='contentText']//table"));

			// find all rows in the table
			ReadOnlyCollection<IWebElement> tableRows = productsTable.FindElements(By.XPath("//tbody//tr"));

			// Path to CSV file
			string path = System.IO.Directory.GetCurrentDirectory() + "/productinformation.csv";

			// check if exists and delete it
			if (File.Exists(path))
			{
				File.Delete(path);
			}

			// foreach loop each row
			foreach (var row in tableRows)
			{
				ReadOnlyCollection<IWebElement> tableData = row.FindElements(By.XPath(".//td"));

                foreach (var tData in tableData)
                {
					string data = tData.Text;
					string[] productInfo = data.Split("\n");

					File.AppendAllText(path, productInfo[0].Trim() + ", " + productInfo[1].Trim() + "\n");
                }
            }

			// check if the file exists
			Assert.IsTrue(File.Exists(path));
			Assert.IsTrue(new FileInfo(path).Length > 0);
		}
	}
}