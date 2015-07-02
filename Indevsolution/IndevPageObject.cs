using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indevsolution
{
    class IndevPageObject
    {
        //Initialize the IndevPageObject by using a constructor
        public IndevPageObject()
        {
            PageFactory.InitElements(Properties.driver, this);  
        }

        //Identify the Search elements in the Indev home page
        [FindsBy(How = How.Name, Using = "q")]
        public IWebElement txtSearchKeyword { get; set; }
        [FindsBy(How = How.ClassName, Using = "icon-search")]
        public IWebElement btnSearch { get; set; }
        [FindsBy(How = How.LinkText, Using = "In development projects")]
        public IWebElement IndevProjectTab { get; set; }
        [FindsBy(How = How.TagName, Using = "p")]
        public IWebElement searchResultsHeading { get; set; }
        [FindsBy(How = How.LinkText, Using = "NEW PROJECT")]
        public IWebElement NewProjectButton { get; set; }
        [FindsBy(How = How.ClassName, Using = "partner-brand")]
        public IWebElement InnerTophatText { get; set; }

        //Perform Search function and return nothing
        public void Search(string SearchKeyword)
        {
            txtSearchKeyword.Clear();
            txtSearchKeyword.SendKeys(SearchKeyword);
            btnSearch.Clicks();
            Properties.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }
    }
}
