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
            SetMethods.ImplicitWait(Properties.driver);
            string URL = "http://dev.indev.nice.org.uk/";
            string FileLocation = "C:\\Users\\sanand\\Documents\\Selenium webdriver\\IndevData.xlsx";
            Properties.driver.Navigate().GoToUrl(URL);
            Properties.driver.Manage().Window.Maximize();
            ExcelLib.PopulateInCollection(FileLocation);
            Properties.TheTable = ExcelLib.ExcelToDataTable(FileLocation);
            Console.WriteLine("Opened URL");

            //Login to Indev application
            LoginPageObject LoginPage = new LoginPageObject();
            //Read the User name and password and perform login operation
            IndevPageObject IndevPage = LoginPage.Login(ExcelLib.ReadData(1, "UserName"), ExcelLib.ReadData(1, "UserPassword"));
            Assert.AreEqual("Guidance projects", IndevPage.IndevProjectTab.Text);
            Assert.IsTrue(IndevPage.NewProjectButton.Displayed);
            StringAssert.AreEqualIgnoringCase(IndevPage.InnerTophatText.Text, "Guidance projects");
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
        public void CreatEditAndDeleteAdviceProjectTest()
        {
            //Create Advice Project
            AdvicePageObject AdvicePage = new AdvicePageObject();
            AdvicePage.CreatEditAndDeleteAdviceProject(ExcelLib.ReadData(1, "AdviceProjectName"), ExcelLib.ReadData(1, "AdviceExpectedPublishedDate"));
        }

        [TearDown]
        public void CleanUp()
        {
            Properties.driver.Close();
            Console.WriteLine("Closed the browser");
        }
    }
}
