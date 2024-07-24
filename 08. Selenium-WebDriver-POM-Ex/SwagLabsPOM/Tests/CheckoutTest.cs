namespace SwagLabsPOM.Tests
{
	public class CheckoutTest : BaseTest
	{
		[SetUp]
		public void SetUp()
		{
			Login("standard_user", "secret_sauce");
			inventoryPage.AddToCartByIndex(1);
			inventoryPage.ClickCartLink();
			cartPage.ClickCheckOutButton();
		}

		[Test]
		public void Test_CheckoutPageLoaded_Correctly()
		{
			Assert.That(checkoutPage.IsPageLoaded(), Is.True, "Checkout page not loaded!");
		}

		[Test]
		public void Test_Checkout_ContinueToNextStep()
		{
			checkoutPage.FillFirstName("Simo");
			checkoutPage.FillLastName("Savov");
			checkoutPage.FillPostalCode("9700");

			checkoutPage.ClickContinueButton();
			Assert.That(driver.Url.Contains("checkout-step-two.html"), Is.True, "Not navigated to the correct checkout page!");
		}

		[Test]
		public void Test_Checkout_CompleteOrder()
		{
			checkoutPage.FillFirstName("Simo");
			checkoutPage.FillLastName("Savov");
			checkoutPage.FillPostalCode("9700");
			checkoutPage.ClickContinueButton();
			checkoutPage.ClickFinishButton();

			Assert.That(checkoutPage.IsCheckoutComplete(), Is.True, "Checkout was not completed!");
		}

	}
}
