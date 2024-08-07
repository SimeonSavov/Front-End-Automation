using System.Collections.Immutable;

namespace IdeaCenterTestProject.Tests
{
	public class IdeaCenterTests : BaseTest
	{
		public string lastCreatedIdeaTitle;
		public string lastCreatedIdeaDescription;

		[Test, Order(1)]
		public void CreateIdea_WithInvalidData_Test()
		{
			_createIdeaPage.OpenPage();

			_createIdeaPage.CreateIdea("", "", "");

			_createIdeaPage.AssertErrorMessages();
		}

		[Test, Order(2)]
		public void CreateIdea_WithRandomData_Test()
		{
			lastCreatedIdeaTitle = "Idea " + GenerateRandomString(5);
			lastCreatedIdeaDescription = "Desc " + GenerateRandomString(5);

			_createIdeaPage.OpenPage();

			_createIdeaPage.CreateIdea(lastCreatedIdeaTitle, "", lastCreatedIdeaDescription);

			Assert.That(_driver.Url, Is.EqualTo(_myIdeasPage.Url), "Url is not corrected.");

			Assert.That(_myIdeasPage.DescriptionLastIdea.Text.Trim(), Is.EqualTo(lastCreatedIdeaDescription), "Description not matched");
		}

		[Test, Order(3)]
		public void ViewLastCreatedIdea_Test()
		{
			_myIdeasPage.OpenPage();

			_myIdeasPage.ViewButtonLastIdea.Click();

			Assert.That(_ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(lastCreatedIdeaTitle), "Title not match");

			Assert.That(_ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(lastCreatedIdeaDescription), "Desc do not match");
		}

		[Test, Order(4)]
		public void EditIdeaTitle_Test()
		{
			_myIdeasPage.OpenPage();

			_myIdeasPage.EditButtonLastIdea.Click();

			string updatedTitle = "Edited Title: " + lastCreatedIdeaTitle;

			_ideasEditPage.TitleInput.Clear();
			_ideasEditPage.TitleInput.SendKeys(updatedTitle);
			_ideasEditPage.EditButton.Click();

			Assert.That(_driver.Url, Is.EqualTo(_myIdeasPage.Url), "Url not correct");

			_myIdeasPage.ViewButtonLastIdea.Click();

			Assert.That(_ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(updatedTitle), "Title not match");
		}

		[Test, Order(5)]
		public void EditIdeaDescription_Test()
		{
			_myIdeasPage.OpenPage();

			_myIdeasPage.EditButtonLastIdea.Click();

			string updatedDescription = "Edited Desc: " + lastCreatedIdeaDescription;

			_ideasEditPage.DescriptionInput.Clear();
			_ideasEditPage.DescriptionInput.SendKeys(updatedDescription);
			_ideasEditPage.EditButton.Click();

			Assert.That(_driver.Url, Is.EqualTo(_myIdeasPage.Url), "Url not correct");

			_myIdeasPage.ViewButtonLastIdea.Click();

			Assert.That(_ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(updatedDescription), "Description not match");
		}

		[Test, Order(6)]
		public void DeleteLastIdea_Test()
		{
			_myIdeasPage.OpenPage();

			_myIdeasPage.DeleteButtonLastIdea.Click();

			bool isIdeaDeleted = _myIdeasPage.IdeasCards.All(card => card.Text.Contains(lastCreatedIdeaDescription));

			Assert.IsFalse(isIdeaDeleted, "The Idea was not deleted");
		}
	}
}
