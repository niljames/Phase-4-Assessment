using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace TestProject__Nunit_
{
    public class Tests
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(@"C:\DoverCorp");
            Thread.Sleep(1000);

            string url = "http://phase-4.azurewebsites.net";
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);
        }

        String currentWindowHandle;
        IWebElement element1, element2, element3;

        [Test]
        public void Test1()
        {
            currentWindowHandle = driver.CurrentWindowHandle;
            driver.SwitchTo().Window(currentWindowHandle);
            element1 = driver.FindElement(By.Id("1002"));
            element1.SendKeys(Keys.Enter);
            Thread.Sleep(1000);

            String pizzaWindowHandle = driver.CurrentWindowHandle;
            driver.SwitchTo().Window(pizzaWindowHandle);
            Thread.Sleep(2000);
            element2 = driver.FindElement(By.Name("Add"));

            element2.SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            String cartWindowHandle = driver.CurrentWindowHandle;
            driver.SwitchTo().Window(cartWindowHandle);
            element3 = driver.FindElement(By.Name("back"));
            element3.SendKeys(Keys.Enter);
            Thread.Sleep(1000);

            driver.SwitchTo().Window(currentWindowHandle);
            element1 = driver.FindElement(By.Id("1005"));
            element1.SendKeys(Keys.Enter);
            Thread.Sleep(1000);


            pizzaWindowHandle = driver.CurrentWindowHandle;
            driver.SwitchTo().Window(pizzaWindowHandle);
            Thread.Sleep(2000);
            element2 = driver.FindElement(By.Name("Add"));

            element2.SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            driver.SwitchTo().Window(cartWindowHandle);
            element3 = driver.FindElement(By.Name("back"));

            element3.SendKeys(Keys.Enter);
            Thread.Sleep(1000);

            driver.SwitchTo().Window(currentWindowHandle);
            element1 = driver.FindElement(By.Name("buy 1002"));
            element1.SendKeys(Keys.Enter);
            Thread.Sleep(2000);


            driver.SwitchTo().Window(cartWindowHandle);
            element3 = driver.FindElement(By.Name("checkout"));

            element3.SendKeys(Keys.Enter);
            Thread.Sleep(1000);

            String checkoutWindowHandle = driver.CurrentWindowHandle;
            driver.SwitchTo().Window(checkoutWindowHandle);
            Thread.Sleep(1000);

            IWebElement element4 = driver.FindElement(By.XPath("//*[text()='Pay with Card']"));

            element4.Click();
            Thread.Sleep(1000);

            driver.SwitchTo().Frame(0);
            Thread.Sleep(1000);



            element1 = driver.FindElement(By.Id("email"));
            element1.SendKeys("noel@yahoo.com");
            Thread.Sleep(1000);
            element1 = driver.FindElement(By.Id("card_number"));
            for (int i = 0; i < 4; i++)
                element1.SendKeys("4242");
            Thread.Sleep(1000);
            element1 = driver.FindElement(By.Id("cc-exp"));
            element1.SendKeys("09");
            element1.SendKeys("24");
            Thread.Sleep(1000);
            element1 = driver.FindElement(By.Id("cc-csc"));
            element1.SendKeys("123");
            Thread.Sleep(1000);
            element1 = driver.FindElement(By.Id("submitButton"));
            element1.Click();

            Thread.Sleep(2000);
           
        }
    }
}
