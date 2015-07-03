using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.Data;

namespace Indevsolution
{
    class Program
    {
        public static void Main(string[] args)
        {
            
        }

        [SetUp]
        public void Initialize()
        {
            //Navigate to the Indev application URL
            Properties.driver = new ChromeDriver();
            string URL = "http://dev.indev.nice.org.uk/";
            string FileLocation = "C:\\Users\\sanand\\Documents\\Selenium webdriver\\IndevData.xlsx";
            Properties.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            Properties.driver.Navigate().GoToUrl(URL);
            Properties.driver.Manage().Window.Maximize();
            Console.WriteLine("Opened URL");
            ExcelLib.PopulateInCollection(FileLocation);
            Properties.TheTable = ExcelLib.ExcelToDataTable(FileLocation);

            //Login to Indev application
            LoginPageObject LoginPage = new LoginPageObject();
            //Read the User name and password and perform login operation
            IndevPageObject IndevPage = LoginPage.Login(ExcelLib.ReadData(1, "UserName"), ExcelLib.ReadData(1, "UserPassword"));
            Assert.AreEqual("In development projects", IndevPage.IndevProjectTab.Text);
            Assert.IsTrue(IndevPage.NewProjectButton.Displayed);
            StringAssert.AreEqualIgnoringCase(IndevPage.InnerTophatText.Text, "In development projects");
        }

        [Test]
        public void SearchTest()
        {
            
            //Search for Indev projects in the home page
            IndevPageObject IndevPage = new IndevPageObject();
            for (int row = 1; row <= Properties.TheTable.Rows.Count; row++)
            {
                IndevPage.Search(ExcelLib.ReadData(row, "SearchKeyword"));
                Assert.IsTrue(IndevPage.searchResultsHeading.Text.Contains("results for"));
            }

        }

        [Test]
        public void NextTest()
        {
            

        }

        [TearDown]
        public void CleanUp()
        {
            Properties.driver.Close();
            Console.WriteLine("Closed the browser");
        }
    }
}
