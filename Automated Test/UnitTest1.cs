using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Text;

namespace Automated_Test
{
    [TestClass]
    public class UnitTest1
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            var option = new ChromeOptions()
            {
                BinaryLocation = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
            };

            //option.AddArgument("--headdless");
            driver = new ChromeDriver(option);
            //driver = new ChromeDriver();
        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            try
            {
                //driver.Quit();// quit does not close the window
                driver.Close();
                driver.Dispose();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [TestInitialize]
        public void InitializeTest()
        {
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            Assert.AreEqual("", verificationErrors.ToString());
        }

        //Primeira parte

        //Funcionalidade: Busca no Banco de Questões
        //Cenário: Busca por questão inexistente
        //Dado que navego para a página de busca do banco de questões
        //E digito 'Science: Computers' no campo de busca
        //Quando clico no botão de buscar
        //Então visualizo uma mensagem de erro com o texto 'No questions found.'

        [TestMethod]
        public void NoQuestionsFoundTest()
        {
            driver.Navigate().GoToUrl("https://opentdb.com/");
            driver.FindElement(By.LinkText("BROWSE")).Click();
            driver.FindElement(By.XPath("//body[@id='page-top']/div[2]")).Click();
            driver.FindElement(By.Id("query")).Click();
            driver.FindElement(By.Id("query")).Clear();
            driver.FindElement(By.Id("query")).SendKeys("Science: Computers");
            driver.FindElement(By.XPath("//body[@id='page-top']/div/form/div/button")).Click();

            ITakesScreenshot camera = driver as ITakesScreenshot;
            Screenshot screenshot = camera.GetScreenshot();
            string cas = DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");

            screenshot.SaveAsFile("C:/Projetos/Una/Evidence/" + cas + ".png");

        }



        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
