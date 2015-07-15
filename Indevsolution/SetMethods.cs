using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Indevsolution
{
    public static class SetMethods
    {
        //Extended method for entering text
        //Enter Text
        public static void EnterText(this IWebElement element, string value)
        {
            element.Clear();
            element.SendKeys(value);
        }

        //Selecting a dropdown control
        public static void SelectDropDown(this IWebElement element, string value)
        {
            //element.Clear();
            new SelectElement(element).SelectByText(value);
        }

        //Click button, check box, option, etc..
        public static void ButtonClick(this IWebElement element)
        {
                if (Exist(element))
                {
                    element.Click();
                }
                else
                {
                    ImplicitWait(Properties.driver);
                }
        }

        //Click link, partial link, etc...
        public static void LinkClick(this IWebElement element, string linktext)
        {
            if (Exist(element))
            {
                element.Click();
            }
            else
            {
                ExplicitWait(Properties.driver, linktext);
            }
        }

        //Check if element exists
        public static Boolean Exist(this IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch(NoSuchElementException e)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        //ImplicitWait
        public static void ImplicitWait(this IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        //ExplicitWait
        public static void ExplicitWait(this IWebDriver driver, string linktext)
        {
            (new WebDriverWait(driver, TimeSpan.FromSeconds(10))).Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText(linktext)));
        }

        //Handle alerts
        public static void HandleHttpsAlert()
        {
            // alert pops up for non https
            var alert = Properties.driver.SwitchTo().Alert();
            alert.Accept();
        }

        //Handle Hover
        public static void Hover(this IWebDriver driver, IWebElement elementToHover, IWebElement elementToClick)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(elementToHover).Click(elementToClick).Build().Perform();
        }

        //Handle Hover and click
        public static void HoverAndClick(this IWebDriver driver,  IWebElement element)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();

        }

        //Switch Window
        public static void SwitchWindow(this IWebDriver driver)
        {
            string homePage = Properties.driver.CurrentWindowHandle;
            Thread.Sleep(2000);
            var windows = Properties.driver.WindowHandles;

            foreach (string handle in windows)
            {
                if (handle != homePage)
                {
                    Properties.driver.SwitchTo().Window(handle);
                    break;
                }
            }
        }
       
    }
}


