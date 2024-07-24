namespace SwagLabsPOM.Tests
{
	public class CartTest : BaseTest
	{
		[SetUp]
		public void SetUp() 
		{
			Login("standard_user", "secret_sauce");
			inventoryPage.AddToCartByIndex(1);
			inventoryPage.ClickCartLink();
		}

		[Test]
		public void Test_CartItem_Displayed()
		{
			Assert.That(cartPage.IsCartItemDisplayed(), Is.True, "No added product in the cart");
		}

		[Test]
		public void Test_ClickCheckout_ShouldRedirectCorrectly()
		{
			cartPage.ClickCheckOutButton();
			Assert.That(checkoutPage.IsPageLoaded(), Is.True, "Not navigated to the checkout page!");
		}
	}
}
