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

        [FindsBy(How = How.XPath, Using = ".//*[@id='create-advice-project']/div[2]/form/div[3]/div/input")]
        public IWebElement txtCreateAdviceProjectModalPublishedDate { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='reset']")]
        public IWebElement txtCreateAdviceProjectModalCloseBtn { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@id='create-advice-project']/div[2]/form/div[4]/div/button[2]")]
        public IWebElement txtCreateAdviceProjectModalCreateBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@class='btn btn-small pull-right edit-action']")]
        public IList <IWebElement> txtEditAdviceProjectModal { get; set; }


        //Perform Advice create function and return nothing
        public void CreatEditAndDeleteAdviceProject(string AdviceProjectName, string AdviceExpectedPublishedDate)
        {
            txtAdviceProjectsTab.Click();            

           txtNewAdviceProjectsButton.Click();

            SetMethods.SwitchWindow(Properties.driver);

            //Create an advice project
            if (SetMethods.Exist(txtCreateAdviceProjectModalLabel))
            {
                txtCreateAdviceProjectModalTitleTextArea.EnterText(AdviceProjectName);
                txtCreateAdviceProjectModalProjectTypeSelect.SelectDropDown("Key therapeutic topics");
                txtCreateAdviceProjectModalPublishedDate.SendKeys(AdviceExpectedPublishedDate);
                txtCreateAdviceProjectModalCreateBtn.ButtonClick();
                SetMethods.SwitchWindow(Properties.driver);
            }    
            else
            {
                Console.WriteLine("The element is not found in New Advice Project Modal");
            }

            //Edit the created advice project
            Thread.Sleep(5000);
            string LinkValue = txtEditAdviceProjectModal.Last().GetAttribute("href");
            txtEditAdviceProjectModal.Last().Click();
            SetMethods.SwitchWindow(Properties.driver);
            var ParentId = Properties.driver.FindElement(By.Id(LinkValue.Replace("#","")));
            var ChildIdTitle = ParentId.FindElement(By.Id("Title"));
            ChildIdTitle.EnterText("Test Advice Project");
            var ChildIdDate = ParentId.FindElement(By.Name("PublishedDate"));
            ChildIdDate.SendKeys("01122016");
            var buttons = ParentId.FindElements(By.TagName("button"));
            var saveButton = buttons.First(b => b.Text == "Save");
            saveButton.ButtonClick();
            SetMethods.SwitchWindow(Properties.driver);

            //Edit, but Do not Save, but just Close the modal
            Thread.Sleep(5000);
            string HrefLink = txtEditAdviceProjectModal.Last().GetAttribute("href");
            txtEditAdviceProjectModal.Last().Click();
            SetMethods.SwitchWindow(Properties.driver);
            var ParentDiv = Properties.driver.FindElement(By.Id(HrefLink.Replace("#", "")));
            var EditTitle = ParentDiv.FindElement(By.Id("Title"));
            EditTitle.EnterText("Test");
            var closeButton = ParentDiv.FindElements(By.TagName("button")).Where(b => b.Text == "Close").First();
            closeButton.ButtonClick();
            SetMethods.SwitchWindow(Properties.driver);

            //Delete the created advice project
            Thread.Sleep(5000);
            string HrefLinkValue = txtEditAdviceProjectModal.Last().GetAttribute("href");
            txtEditAdviceProjectModal.Last().Click();
            SetMethods.SwitchWindow(Properties.driver);
            var ParentDivId = Properties.driver.FindElement(By.Id(HrefLinkValue.Replace("#", "")));
            var deleteButton = ParentDivId.FindElements(By.TagName("button")).Where(b => b.Text == "Delete").First();
            deleteButton.ButtonClick();
            SetMethods.SwitchWindow(Properties.driver);     
                     
        }
    }
}
