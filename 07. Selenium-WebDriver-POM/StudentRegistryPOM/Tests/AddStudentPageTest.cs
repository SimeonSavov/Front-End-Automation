using StudentRegistryPOM.Pages;

namespace StudentRegistryPOM.Tests
{
	public class AddStudentPageTest : BaseTest
	{
		[Test]
		public void Test_AddStudentPage_Content()
		{
			AddStudentPage addStudentsPage = new AddStudentPage(driver);
			addStudentsPage.OpenPage();

			Assert.Multiple(() =>
			{
				Assert.That(addStudentsPage.GetPageTitle(), Is.EqualTo("Add Student"));
				Assert.That(addStudentsPage.GetPageHeading(), Is.EqualTo("Register New Student"));
			});

			Assert.That(addStudentsPage.FieldName.Text, Is.EqualTo(""));
			Assert.That(addStudentsPage.FieldEmail.Text, Is.EqualTo(""));
			Assert.That(addStudentsPage.AddButton.Text, Is.EqualTo("Add"));

		}

		[Test]
		public void Test_TestAddStudentPage_Links()
		{
			var page = new AddStudentPage(driver);
			page.OpenPage();
			Assert.IsTrue(new AddStudentPage(driver).IsPageOpen());

			page.OpenPage();
			page.HomeLink.Click();
			Assert.IsTrue(new HomePage(driver).IsPageOpen());

			page.OpenPage();
			page.ViewStudentsLink.Click();
			Assert.IsTrue(new ViewStudentsPage(driver).IsPageOpen());
		}

		[Test]
		public void Test_AddStudentPage_AddValidStudent()
		{
			AddStudentPage addStudentsPage = new AddStudentPage(driver);
			addStudentsPage.OpenPage();

			string name = GenerateRandomName();
			string email = GenerateRandomEmail(name);

			addStudentsPage.AddStudent(name, email);

			ViewStudentsPage viewStudentsPage = new ViewStudentsPage(driver);
			Assert.That(viewStudentsPage.IsPageOpen(), Is.True);

			var students = viewStudentsPage.GetRegisteredStudents();

			string newStudentFullString = name + " (" + email + ")";
			Assert.True(students.Contains(newStudentFullString));
		}

		[Test]
		public void Test_AddStudentPage_TryWithInvalidStudent()
		{
			AddStudentPage addStudentsPage = new AddStudentPage(driver);
			addStudentsPage.OpenPage();

			addStudentsPage.AddStudent("", "petur@gmail.com");

			Assert.That(addStudentsPage.IsPageOpen(), Is.True);
			Assert.That(addStudentsPage.GetErrorMessage(), Is.EqualTo("Cannot add student. Name and email fields are required!"));
		}

		private string GenerateRandomName()
		{
			var random = new Random();
			string[] names = { "Ivan", "Petur", "Todor", "Simeon" };

			return names[random.Next(names.Length)] + random.Next(999, 9999).ToString();
		}

		private string GenerateRandomEmail(string name)
		{
			var random = new Random();

			return name.ToLower() + random.Next(999, 9999).ToString() + "@gmail.com";
		}
	}
}
