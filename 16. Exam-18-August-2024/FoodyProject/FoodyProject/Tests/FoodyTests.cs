using OpenQA.Selenium.Interactions;

namespace FoodyProject.Tests
{
	public class FoodyTests : BaseTest
    {
        private string lastFoodName;
        private string lastFoodDescription;

        [Test, Order(1)]
        public void AddFood_WithInvalidData_Test()
        {
            addFoodPage.OpenPage();

            addFoodPage.FoodNameField.Clear();
            addFoodPage.FoodDescriptionField.Clear();
            addFoodPage.AddFoodButton.Click();

            addFoodPage.AssertEmptyNameErrorMessage();
            addFoodPage.AssertEmptyDescriptionErrorMessage();
        }

		[Test, Order(2)]
		public void AddFood_WithValidData_Test()
		{
            lastFoodName = GenerateRandomFoodName();
            lastFoodDescription = GenerateRandomFoodDescription();

			addFoodPage.OpenPage();

			addFoodPage.FoodNameField.Clear();
            addFoodPage.FoodNameField.SendKeys(lastFoodName);

			addFoodPage.FoodDescriptionField.Clear();
            addFoodPage.FoodDescriptionField.SendKeys(lastFoodDescription);

			addFoodPage.AddFoodButton.Click();

			string currentUrl = driver.Url;
			Assert.That(currentUrl, Is.EqualTo("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:85/"));

			Assert.That(allFoodPage.LastFoodTitle.Text.Trim, Is.EqualTo(lastFoodName), "The good name is not as expected");
		}

        [Test, Order(3)]
        public void EditFood_WithValidName_DoesNotChangeTheName_Test()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(allFoodPage.LastFoodEditButton).Perform();

            allFoodPage.LastFoodEditButton.Click();

			editFoodPage.EditFoodNameField.Clear();
			editFoodPage.EditFoodNameField.SendKeys("11111");

			editFoodPage.EditFoodButton.Click();

			string actualFoodName = allFoodPage.LastFoodTitle.Text.Trim();
			Assert.That(actualFoodName, Is.EqualTo(lastFoodName), "The name of the food is not as expected");

			if (actualFoodName == lastFoodName)
			{
				Console.WriteLine("Test passed: The title did not change, as expected due to incomplete functionality.");
			}
			else
			{
				Console.WriteLine("Test failed: The title changed unexpectedly.");
			}
		}

		[Test, Order(4)]
		public void SearchFood_ValidResult_Test()
		{
			allFoodPage.SearchInput.SendKeys(lastFoodName);
			allFoodPage.SearchButton.Click();

			Assert.That(allFoodPage.LastFoodTitle.Text.Trim, Is.EqualTo(lastFoodName), "The food with that name not existing");
		}

		[Test, Order(5)]
		public void DeleteFood_LastAdded_Test()
		{
			allFoodPage.OpenPage();

			var allFoodsCount = allFoodPage.AllFoods.Count;

			Actions actions = new Actions(driver);
			actions.MoveToElement(allFoodPage.LastFoodDeleteButton).Perform();

			allFoodPage.LastFoodDeleteButton.Click();

			var allFoodsCountUpdated = allFoodPage.AllFoods.Count;

			Assert.That(allFoodsCountUpdated, Is.EqualTo(allFoodsCount - 1), "The delete method is not performed");
		}

		[Test, Order(6)]
		public void SearchFood_NonExistingName_Test()
		{
			allFoodPage.OpenPage();

			allFoodPage.SearchInput.SendKeys(lastFoodName);
			allFoodPage.SearchButton.Click();

			allFoodPage.AssertNoFoodMessage();
			allFoodPage.AssertAddFoodButtonIsPresent();
		}
	}
}