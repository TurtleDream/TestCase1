﻿using System;
using System.Linq;
using System.Windows.Forms;
using OpenQA.Selenium;

namespace TestCase1
{
    class Test
    {
        IWebDriver browser;

        public Test(IWebDriver driver)
        {
            this.browser = driver;
        }

        public void test()
        {
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            browser.Manage().Window.Maximize();//1.развернуть на весь экран

            browser.Navigate().GoToUrl("https://yandex.ru/");//2. Зайти на yandex.ru.

            browser.FindElement(By.XPath("//a[@href='https://market.yandex.ru/?clid=505&utm_source=face_abovesearch&utm_campaign=face_abovesearch']")).Click();//3. Перейти в яндекс маркет

            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            if (check("/html/body/div[4]/div[3]"))
            {
                browser.FindElement(By.XPath("/html/body/div[4]/div[3]/div/div/div[2]/div[1]")).Click();
                browser.FindElement(By.XPath("/html/body/div[1]/div/span/div[2]/noindex/div[2]/div/div/div/div[2]")).Click();//4. Выбрать раздел электроника
            }
            else browser.FindElement(By.XPath("/html/body/div[1]/div/span/div[2]/noindex/div[2]/div/div/div[2]")).Click();//4. Выбрать раздел электроника


            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            browser.FindElement(By.XPath("/html/body/div[1]/div[2]/div[7]/div/div/div[1]/div/div/div/div/div/div/div[3]/div[2]/ul/li[1]/div")).Click();//5. Выбрать раздел телевизоры

            browser.FindElement(By.XPath("//*[@id='search-prepack']/div/div/div[3]/div/div[3]/div/a")).Click();//6.Зайти в расширеный поиск

            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(12);
            browser.FindElement(By.XPath("//*[@id='glf-pricefrom-var']")).SendKeys("20000");//7. Задать параметр поиска от 20000 рублей

            browser.FindElement(By.XPath("/html/body/div[1]/div[5]/div/div[1]/div[1]/div[4]/div[2]/div/div[1]/div[8]/a/span")).Click();//8. Выбрать производителей Samsung

            browser.FindElement(By.XPath("/html/body/div[1]/div[5]/div/div[1]/div[1]/div[4]/div[2]/div/div[1]/div[4]/a/span")).Click();//8. и LG

            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
            browser.FindElement(By.XPath("/html/body/div[1]/div[5]/div/div[1]/div[5]/a[2]")).Click();//9. Нажать кнопку Применить

            String str = browser.FindElement(By.XPath("/html/body/div[1]/div[5]/div[2]/div[1]/div[2]/div/div[3]/span/button/span")).GetAttribute("textContent");

            if (!str.Contains("12"))
            {
                browser.FindElement(By.XPath("/html/body/div[1]/div[5]/div[2]/div[1]/div[2]/div/div[3]/span")).Click();
                browser.FindElement(By.ClassName("select__item")).Click();
            }//10. Проверить, что элементов на странице 12.

            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            str = browser.FindElements(By.ClassName("n-snippet-card2__title")).ElementAt(0).GetAttribute("textContent");//11. Запомнить первый элемент в списке.

            browser.FindElement(By.XPath("//*[@id='header-search']")).SendKeys(str);//12. В поисковую строку ввести запомненное значение.
            browser.FindElement(By.XPath("/html/body/div[1]/div[1]/noindex/div/div/div[2]/div/div[1]/form/span/span[2]/button")).Click();

            String title = browser.FindElement(By.XPath("/html/body/div[1]/div[5]/div[2]/div/div/div[1]/div[1]/div/h1")).GetAttribute("textContent");

            if (str.Equals(title))
            {
                MessageBox.Show("Success!");
            }
            else MessageBox.Show("Fail!");//13. Найти и проверить, что наименование товара соответствует запомненному значению.

        }

        bool check(String str)
        {
            try
            {
                browser.FindElement(By.XPath(str));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }

    class Program
    {      
        static void Main(string[] args)
        {
            IWebDriver browser = new OpenQA.Selenium.Firefox.FirefoxDriver();//1. Открыть браузер
            IWebDriver browser2 = new OpenQA.Selenium.Chrome.ChromeDriver();
                       
            Test test = new Test(browser);
            test.test();

            Test test2 = new Test(browser2);
            test2.test();
        }
    }
}
