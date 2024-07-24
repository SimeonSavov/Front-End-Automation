namespace SwagLabsPOM.Tests
{
	public class LoginTest : BaseTest
	{
		[Test]
		public void TestLogin_WithValidCredentials()
		{
			Login("standard_user", "secret_sauce");

			Assert.That(inventoryPage.IsPageLoaded(), Is.True, "The inventory page is not loaded after successfull login");
		}

		[Test]
		public void TestLogin_WithInvalidCredentials()
		{
			Login("invalid_user", "secret_sauce");

			string error = loginPage.GetErrorMessage();

			Assert.That(error.Contains("Username and password do not match any user in this service"), "Error message is not correct");
		}

		[Test]
		public void TestLogin_WithLockedOutUser()
		{
			Login("locked_out_user", "secret_sauce");

			string error = loginPage.GetErrorMessage();

			Assert.That(error.Contains("Sorry, this user has been locked out."), "Error message is not correct");
		}
	}
}
