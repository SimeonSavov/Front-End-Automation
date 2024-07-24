namespace SwagLabsPOM.Tests
{
	public class InventoryTest : BaseTest
	{
		[SetUp]
		public void SetUp()
		{
			Login("standard_user", "secret_sauce");
		}

		[Test]
		public void Test_InventoryPage_Displayed()
		{
			Assert.That(inventoryPage.IsInventoryPageHasItemsDisplayed(), Is.True, "Inventory page has no items displayed");
		}

		[Test]
		public void Test_AddToCart_ByIndex()
		{
			inventoryPage.AddToCartByIndex(1);

			inventoryPage.ClickCartLink();

			Assert.That(cartPage.IsCartItemDisplayed(), Is.True, "Cart item was not added in the cart!");
		}

		[Test]
		public void Test_AddToCart_ByName()
		{
			inventoryPage.AddToCartByName("Sauce Labs Fleece Jacket");

			inventoryPage.ClickCartLink();

			Assert.That(cartPage.IsCartItemDisplayed(), Is.True, "Cart item was not added in the cart!");
		}

		[Test]
		public void Test_PageTitle_IsPresent()
		{
			Assert.That(inventoryPage.IsPageLoaded(), Is.True, "Inventory page not loaded correctly!");
		}
	}
}
