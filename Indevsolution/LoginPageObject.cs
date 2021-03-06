﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indevsolution
{
    class LoginPageObject
    {

        //Initialize the LoginPageObject by using a constructor
        public LoginPageObject()
        {
            PageFactory.InitElements(Properties.driver, this);  
        }
        
        //Identify the elements in the Login page
        [FindsBy(How = How.Id, Using = "Email")]
        public IWebElement txtUserName {get; set;}

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement txtUserPassword {get; set;}

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement btnSubmit {get; set;}

        //Perform login operation and go to the Indev home page            
        public IndevPageObject Login(string UserName, string UserPassword)
        {
            txtUserName.EnterText(UserName);
            txtUserPassword.EnterText(UserPassword);
            if (SetMethods.Exist(btnSubmit))
            {
                btnSubmit.ButtonClick(); 
                return new IndevPageObject();
            }
            else
            {
                Console.WriteLine("The Login button is not found");
                return null;
            }
        }
    }
}
