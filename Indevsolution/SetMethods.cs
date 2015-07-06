using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        //Selecting a dropdown control
        public static void SelectDropDown   (this IWebElement element, string value)
        {
            element.Clear();
            new SelectElement(element).SelectByText(value);
        }
    }
}
