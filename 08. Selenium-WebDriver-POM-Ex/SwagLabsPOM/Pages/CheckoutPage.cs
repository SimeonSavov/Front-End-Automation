using OpenQA.Selenium;

namespace SwagLabsPOM.Pages
{
	public class CheckoutPage : BasePage
	{
        protected readonly By firstNameInput = By.XPath("//input[@id='first-name']");

        protected readonly By lastNameInput = By.XPath("//input[@id='last-name']");

        protected readonly By postalCodeInput = By.XPath("//input[@id='postal-code']");

        protected readonly By continueButton = By.XPath("//input[@type='submit']");

        protected readonly By finishButton = By.CssSelector("#finish");

        protected readonly By completeHeader = By.ClassName("complete-header");

        public CheckoutPage(IWebDriver driver) : base(driver)
        {
        }

        public void FillFirstName(string firstName)
        {
            Type(firstNameInput, firstName);
        }

        public void FillLastName(string lastName)
        {
            Type(lastNameInput, lastName);
        }

        public void FillPostalCode(string postalCode)
        {
            Type(postalCodeInput, postalCode);
        }

        public void ClickContinueButton()
        {
            Click(continueButton);
        }

        public void ClickFinishButton()
        {
            Click(finishButton);
        }

        public bool IsPageLoaded()
        {
            return driver.Url.Contains("checkout-step-one.html") || driver.Url.Contains("checkout-step-two.html");
        }

        public bool IsCheckoutComplete()
        {
            return GetText(completeHeader) == "Thank you for your order!";
        }
    }
}
