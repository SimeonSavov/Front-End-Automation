namespace SwagLabsPOM.Tests
{
	public class HiddenMenuTest : BaseTest
	{
		[SetUp]
		public void SetUp()
		{
			Login("standard_user", "secret_sauce");
		}

		[Test]
		public void Test_OpenHamburgerMenu()
		{
			hiddenMenuPage.CLickHamburgerMenuButton();

			Assert.That(hiddenMenuPage.IsMenuOpen(), Is.True, "Menu is not open");  
		}

		[Test]
		public void Test_Logout_Correctly()
		{
			hiddenMenuPage.CLickHamburgerMenuButton();
			hiddenMenuPage.CLickLogoutButton();

			Assert.That(driver.Url.Equals("https://www.saucedemo.com/"), "User was not logged out correctly!");
		}
	}
}
