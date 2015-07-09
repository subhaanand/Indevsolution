using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Indevsolution
{
    class AdvicePageObject
    {
        //Initialize the AdvicePageObject by using a constructor
        public AdvicePageObject()
        {
            PageFactory.InitElements(Properties.driver, this);  
        }

        //Identify the elements in the Advice page
        [FindsBy(How = How.LinkText, Using = "Advice projects")]
        public IWebElement txtAdviceProjectsTab {get; set;}

        [FindsBy(How = How.LinkText, Using = "NEW ADVICE PROJECT")]
        public IWebElement txtNewAdviceProjectsButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//h3[@id='createadviceProjectModalLabel']")]
        public IWebElement txtCreateAdviceProjectModalLabel { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id ='create-advice-project']/div[@class='modal-body']/form[@class='form-horizontal']/div[@class='control-group']/div[@class='controls']/textarea[@id='Title']")]
        public IWebElement txtCreateAdviceProjectModalTitleTextArea { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@id='ProjectType']")]
        public IWebElement txtCreateAdviceProjectModalProjectTypeSelect { get; set; }

        [FindsBy(How = How.Name, Using = "PublishedDate")]
        public IWebElement txtCreateAdviceProjectModalPublishedDate { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='reset']")]
        public IWebElement txtCreateAdviceProjectModalCloseBtn { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@id='create-advice-project']/div[2]/form/div[4]/div/button[2]")]
        public IWebElement txtCreateAdviceProjectModalCreateBtn { get; set; }

        //Perform Advice create function and return nothing
        public void CreateAdviceProject(string AdviceProjectName)
        {
            txtAdviceProjectsTab.Click();
            string homePage = Properties.driver.CurrentWindowHandle;

            txtNewAdviceProjectsButton.Click();
            //SetMethods.ImplicitWait(Properties.driver);
            Thread.Sleep(2000);
            var windows = Properties.driver.WindowHandles;

            foreach (string handle in windows)
            {
                if (handle != homePage)
                {
                    Properties.driver.SwitchTo().Window(handle); break;
                }
            }
            if (SetMethods.Exist(txtCreateAdviceProjectModalLabel))
            {
                txtCreateAdviceProjectModalTitleTextArea.EnterText(AdviceProjectName);
                txtCreateAdviceProjectModalProjectTypeSelect.SelectDropDown("Key therapeutic topics");
                txtCreateAdviceProjectModalCreateBtn.ButtonClick();
            }    
            else
            {
                Console.WriteLine("The element is not found in New Advice Project Modal");
            }         
        
                     
        }
    }
}
