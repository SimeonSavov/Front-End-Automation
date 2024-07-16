using StudentRegistryPOM.Pages;

namespace StudentRegistryPOM.Tests
{
	public class ViewStudentsPageTest : BaseTest
	{
		[Test]
		public void Test_ViewStudentsPage_Content()
		{
			ViewStudentsPage viewStudentsPage = new ViewStudentsPage(driver);
			viewStudentsPage.OpenPage();

			Assert.Multiple(() =>
			{
				Assert.That(viewStudentsPage.GetPageTitle(), Is.EqualTo("Students"));
				Assert.That(viewStudentsPage.GetPageHeading(), Is.EqualTo("Registered Students"));
			});

			var students = viewStudentsPage.GetRegisteredStudents();

            foreach (var student in students)
            {
				Assert.That(student.Contains("("), Is.True);
				Assert.That(student.LastIndexOf(")") == student.Length - 1, Is.True);
            }
        }

		[Test]
		public void Test_ViewStudentsPage_Links()
		{
			var page = new ViewStudentsPage(driver);
			page.OpenPage();
			Assert.IsTrue(new ViewStudentsPage(driver).IsPageOpen());

			page.OpenPage();
			page.HomeLink.Click();
			Assert.IsTrue(new HomePage(driver).IsPageOpen());

			page.OpenPage();
			page.AddStudentLink.Click();
			Assert.IsTrue(new AddStudentPage(driver).IsPageOpen());
		}
	}
}
