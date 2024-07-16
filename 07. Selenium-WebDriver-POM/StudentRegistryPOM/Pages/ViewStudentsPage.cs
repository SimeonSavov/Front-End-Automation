﻿using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace StudentRegistryPOM.Pages
{
	public class ViewStudentsPage : BasePage
	{
        public ViewStudentsPage(IWebDriver driver) : base(driver)
        {  
        }

		public override string PageUrl => "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:82/students";

		public ReadOnlyCollection<IWebElement> StudentsListItems => driver.FindElements(By.CssSelector("body > ul > li"));

		public string[] GetRegisteredStudents()
		{
			var elementsStudents = this.StudentsListItems.Select(s => s.Text).ToArray();
			return elementsStudents;
		}
	}
}
