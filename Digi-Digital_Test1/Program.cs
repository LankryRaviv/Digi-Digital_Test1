using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;



namespace Digi_Digital_Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get details from user
            Console.WriteLine("Enter Your Phone Number Or Email");
            var username = Console.ReadLine();
            Console.WriteLine("Enter Your Password");
            var password = Console.ReadLine();

            //Set-Up
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://www.facebook.com");

            //Login
            driver.FindElement(By.XPath(".//*[@id='email']")).SendKeys(username);               
            driver.FindElement(By.XPath(".//*[@id='pass']")).SendKeys(password);                     
            driver.FindElement(By.XPath(".//*[@name='login']")).Click();
            Thread.Sleep(4000);

            //Check if the login is successful
            if (driver.FindElements(By.XPath(".//*[@id='pass']")).Count() > 0)
            {
                Console.WriteLine("Login Faild");
            }
            else
            {
                Console.WriteLine("Login Successfully");

                //Click like
                var p = driver.FindElement(By.XPath(".//div[@role='feed']/div[starts-with(@data-pagelet,'FeedUnit')]//div[@aria-label='Like']"));
                p.Click();
                
                //Find number of likes
                var likeString = driver.FindElement(By.XPath(".//div[starts-with(@aria-label,'Like:')]")).GetAttribute("aria-label");
                var numberOfLikes = likeString.Split(' ')[1];

                //Find number of comments
                var commentString = driver.FindElements(By.XPath(".//*[@class='gtad4xkn']"))[0].Text;
                var numberOfComment = commentString.Split(' ')[0];

                //Show number of likes and comments
                Console.WriteLine("Number of Likes Is: " + numberOfLikes);
                Console.WriteLine("Number of Comments Is: " + numberOfComment);
            }
        }
    }
}
