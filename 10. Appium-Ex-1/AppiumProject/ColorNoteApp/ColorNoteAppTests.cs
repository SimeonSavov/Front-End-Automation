using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace ColorNoteApp
{
	public class ColorNoteAppTests
	{
		private AndroidDriver _driver;
		private AppiumLocalService _appiumLocalService;

		[OneTimeSetUp]
		public void Setup()
		{
			_appiumLocalService = new AppiumServiceBuilder()
				.WithIPAddress("127.0.0.1")
				.UsingPort(4723)
				.Build();
			_appiumLocalService.Start();

			var androidOptions = new AppiumOptions()
			{
				PlatformName = "Android",
				AutomationName = "UiAutomator2",
				DeviceName = "Pixel 7",
				App = @"C:\\SoftUni - Automation\\10. Appium-Ex\\Notepad.apk",
				PlatformVersion = "14",
			};
			androidOptions.AddAdditionalAppiumOption("autoGrantPermissions", true);

			_driver = new AndroidDriver(_appiumLocalService, androidOptions);

			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			try
			{
				var skipTutorialButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/btn_start_skip"));
				skipTutorialButton.Click();
			}
			catch (NoSuchElementException)
			{

			}
		}

		[OneTimeTearDown]
		public void Teardown()
		{
			_driver?.Quit();
			_driver?.Dispose();
			_appiumLocalService?.Dispose();
		}

		[Test, Order(1)]
		public void Test_CreateNewNote()
		{
			var newNoteButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
			newNoteButton.Click();

			var createNoteText = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
			createNoteText.Click();

			var noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
			noteTextField.SendKeys("Test_1");

			var backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
			backButton.Click();
			backButton.Click();

			var createdNote = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/title"));

			Assert.That(createdNote, Is.Not.Null, "Note was not created");

			Assert.That(createdNote.Text, Is.EqualTo("Test_1"), "Note text is not the same");
		}

		[Test, Order(2)]
		public void Test_EditANote()
		{
			var newNoteButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
			newNoteButton.Click();

			var createNoteText = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
			createNoteText.Click();

			var noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
			noteTextField.SendKeys("Test_2");

			var backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
			backButton.Click();
			backButton.Click();

			var note = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Test_2\")"));
			note.Click();

			var editButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_btn"));
			editButton.Click();

			noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
			noteTextField.Click();
			noteTextField.Clear();
			noteTextField.SendKeys("Edited");


			backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
			backButton.Click();
			backButton.Click();

			var editedNote = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Edited\")"));

			Assert.That(editedNote.Text, Is.EqualTo("Edited"), "Note was not edited");
		}

		[Test, Order(3)]
		public void Test_DeleteANote()
		{
			var newNoteButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
			newNoteButton.Click();

			var createNoteText = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
			createNoteText.Click();

			var noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
			noteTextField.SendKeys("NoteForDelete");

			var backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
			backButton.Click();
			backButton.Click();

			var note = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"NoteForDelete\")"));
			note.Click();

			var menuButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/menu_btn"));
			menuButton.Click();

			var deleteButton = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Delete\")"));
			deleteButton.Click();

			var okButton = _driver.FindElement(MobileBy.Id("android:id/button1"));
			okButton.Click();

			var deletedNote = _driver.FindElements(MobileBy.XPath("//android.widget.TextView[@text=\"NoteForDelete\"]"));

			Assert.That(deletedNote, Is.Empty, "Note was not deleted");
		}
	}
}