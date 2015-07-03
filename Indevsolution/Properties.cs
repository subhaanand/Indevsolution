using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Indevsolution
{
    enum PropertyType
    {
        Id,
        Name,
        Type,
        Text,
        LinkText,
        CssName,
        ClassName

    }
    
    class Properties
    {
        //Auto-implemented properties
        
        public static IWebDriver driver { get; set; }
        public static string URL { get; set; }
        public static string FileLocation { get; set; }
        public static DataTable TheTable { get; set; }

        
    }
}
