namespace MovieCatalogProject.Tests
{
    public class MovieCatalogTests : BaseTest
    {
        private string lastMovieTitle;
        private string lastMovieDescription;

        [Test, Order(1)]
        public void AddMovie_WithoutTitle_Test()
        {
            addMoviePage.OpenPage();

            addMoviePage.TitleInput.Clear();
            addMoviePage.AddButton.Click();

            addMoviePage.AssertEmptyTitleMessage();
        }

		[Test, Order(2)]
		public void AddMovie_WithoutDescription_Test()
		{
            lastMovieTitle = GenerateRandomTitle();

			addMoviePage.OpenPage();

			addMoviePage.TitleInput.Clear();
            addMoviePage.TitleInput.SendKeys(lastMovieTitle);
			addMoviePage.AddButton.Click();

			addMoviePage.AssertEmptyDescriptionMessage();
		}

		[Test, Order(3)]
		public void AddMovie_ValidMovie_Test()
		{
			lastMovieTitle = GenerateRandomTitle();
			lastMovieDescription = GenerateRandomDescription();

			addMoviePage.OpenPage();

			addMoviePage.TitleInput.Clear();
			addMoviePage.TitleInput.SendKeys(lastMovieTitle);

			addMoviePage.DescriptionInput.Clear();
			addMoviePage.DescriptionInput.SendKeys(lastMovieDescription);

			addMoviePage.AddButton.Click();

			allMoviesPage.NavigateToLastPage();

			Assert.That(allMoviesPage.LastMovieTitle.Text.Trim(), Is.EqualTo(lastMovieTitle), "The title is not as expected");
		}

		[Test, Order(4)]
		public void EditLastMovie_ValidResult_Test()
		{
			lastMovieTitle = GenerateRandomTitle() + "EDITED!";
			lastMovieDescription = GenerateRandomDescription();

			addMoviePage.OpenPage();
			allMoviesPage.NavigateToLastPage();
			allMoviesPage.LastMovieEditButton.Click();

			editMoviePage.TitleInput.Clear();
			editMoviePage.TitleInput.SendKeys(lastMovieTitle);

			editMoviePage.DescriptionInput.Clear();
			editMoviePage.DescriptionInput.SendKeys(lastMovieDescription);

			editMoviePage.EditButton.Click();

			editMoviePage.AssertRecordEdited();
		}

		[Test, Order(5)]
		public void MarkLastMovieAsWatched_Test()
		{
			allMoviesPage.OpenPage();
			allMoviesPage.NavigateToLastPage();
			allMoviesPage.LastMovieMarkAsWatchedButton.Click();

			watchedMoviesPage.OpenPage();
			watchedMoviesPage.NavigateToLastPage();

			Assert.That(watchedMoviesPage.LastMovieTitle.Text.Trim(), Is.EqualTo(lastMovieTitle), "The movie was not added to watched!");
		}

		[Test, Order(6)]
		public void DeleteLastMovie_Test()
		{
			allMoviesPage.OpenPage();
			allMoviesPage.NavigateToLastPage();
			allMoviesPage.LastMovieDeleteButton.Click();

			deleteMoviePage.YesButton.Click();

			Assert.That(deleteMoviePage.ToastMessage.Text.Trim(), Is.EqualTo("The Movie is deleted successfully!"), "The movie was not deleted");
		}
	}
}