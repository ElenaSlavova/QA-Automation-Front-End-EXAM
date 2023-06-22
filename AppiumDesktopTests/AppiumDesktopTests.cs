
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumDesktopTests
{
    public class AppiumDesktopTests
    {
        private WindowsDriver<WindowsElement> driver;
        private WindowsDriver<WindowsElement> secoundDriver;
        private AppiumOptions options;
        private AppiumOptions secondOptions;

        private const string appLocation = @"C:\projects\ContactBook-DesktopClient.NET6\ContactBook-DesktopClient.exe";
        private const string appiumServer = "http://127.0.0.1:4723/wd/hub";
        private const string appServer = "https://contactbook-1.slavova1.repl.co/api";

        [SetUp]
        public void PrepareApp()
        {
            this.options = new AppiumOptions();
            options.AddAdditionalCapability("app", appLocation);
            this.driver = new WindowsDriver<WindowsElement>(new Uri(appiumServer), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(200);
            secondOptions = new AppiumOptions();
            secondOptions.AddAdditionalCapability("app", "Root");
            secondOptions.AddAdditionalCapability("platformName", "Windows");
            secoundDriver = new WindowsDriver<WindowsElement>(new Uri(appiumServer), secondOptions);
            secoundDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }
        [TearDown]
        public void CloseApp()
        {
            driver.Quit();
        }
        [Test]
        public void Test_VerifySearchResult()
        {
          

            var textBox = driver.FindElementByAccessibilityId("textBoxApiUrl");
            textBox.Clear();
            textBox.SendKeys(appServer);



           var buttonConnect = driver.FindElementByAccessibilityId("buttonConnect");
            buttonConnect.Click();

            Thread.Sleep(2000);
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            var fieldSearch = driver.FindElementByAccessibilityId("textBoxSearch");
             fieldSearch.SendKeys("steve");

            var buttonSearch = driver.FindElementByAccessibilityId("buttonSearch");
             buttonSearch.Click();


             var firstName = driver.FindElementByName("FirstName Row 0, Not sorted.");
             var lName = driver.FindElementByName("LastName Row 0, Not sorted.");

            Assert.That(firstName.Text, Is.EqualTo("Steve"));
            Assert.That(lName.Text, Is.EqualTo("Jobs"));

         

        }
    }


}
