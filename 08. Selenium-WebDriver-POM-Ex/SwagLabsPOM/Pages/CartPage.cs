using OpenQA.Selenium;

namespace SwagLabsPOM.Pages
{
	public class CartPage : BasePage
	{
        protected readonly By cartItem = By.CssSelector(".cart_item");

        protected readonly By checkOutButton = By.Id("checkout");

        public CartPage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsCartItemDisplayed()
        {
            return FindElement(cartItem).Displayed;
        }

        public void ClickCheckOutButton()
        {
            Click(checkOutButton);
        }
    }
}
