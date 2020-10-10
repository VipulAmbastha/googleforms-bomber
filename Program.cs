using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FormsBomber
{
    class Program
    {
        public static bool firstimeonly = true;
        static void Main(string[] args)
        {
            //Declare Objects
            IWebDriver driver = new ChromeDriver();
            Random random = new Random();
            Names NameClass = new Names();
            StringBuilder strings = new StringBuilder();

            //Declare Variables
            int length = 10;
            char letter;
            Console.WriteLine("Enter the URL");
            string URL = Console.ReadLine();

        ResetPage:

            firstimeonly = true;
            driver.Navigate().GoToUrl(URL);
            int namesl = random.Next(0, 4000);
            int sectionsl = random.Next(1, 4);
            int rolls = random.Next(1, 60);
            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                strings.Append(letter);
            }

        NextPage:

            Thread.Sleep(300);

            try
            {
                ReadOnlyCollection<IWebElement> options = driver.FindElements(By.ClassName("appsMaterialWizToggleRadiogroupOffRadio"));
                ReadOnlyCollection<IWebElement> textinput = driver.FindElements(By.ClassName("quantumWizTextinputPaperinputInput"));
                ReadOnlyCollection<IWebElement> parainput = driver.FindElements(By.ClassName("quantumWizTextinputPapertextareaInput"));

                foreach (var item in options)
                {
                    item.Click();
                }
                foreach(var item in textinput)
                {
                    item.SendKeys(strings.ToString());
                }
                foreach (var item in parainput)
                {
                    item.SendKeys(strings.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (firstimeonly == true)
            {
                driver.FindElement(By.XPath("//*[@id='mG61Hd']/div[2]/div/div[3]/div[1]/div/div/span")).Click();
                firstimeonly = false;
                goto NextPage;
            }

            try
            {
                driver.FindElement(By.XPath("//*[@id='mG61Hd']/div[2]/div/div[3]/div[1]/div/div[2]/span/span")).Click();
                goto NextPage;
            }
            catch
            {
                goto ResetPage;
            }
        }
    }
}
