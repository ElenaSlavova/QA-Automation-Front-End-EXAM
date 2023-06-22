

using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;

namespace AppiumMobileTests
{
    public class AppiumMobileTests
    {
        private const string UriString = "http://127.0.0.1:4723/wd/hub";
        private const string appLocation = @"C:\projects\contactbook-androidclient.apk";
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;

        [SetUp]
        public void PrepareApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", appLocation);
            // options.AddAdditionalCapability("appPackage", "vivino.web.app");
            // options.AddAdditionalCapability("appActivity", "com.sphinx_solution.activities.SplashActivity");

            this.driver = new AndroidDriver<AndroidElement>(new Uri(UriString), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(200);
        }
        [TearDown]
        public void CloseApp()
        {
            driver.Quit();
        }
        [Test]
        public void Test_ContactBook_VerifyName()
        {
            var contactBookApi = driver.FindElementById("contactbook.androidclient:id / editTextApiUrl");
            contactBookApi.Click();
            contactBookApi.Clear();
            contactBookApi.SendKeys("https://contactbook-1.slavova1.repl.co/api");


            var searchField = driver.FindElementById("com.android.example.github:id/input");
            searchField.Click();
            searchField.SendKeys("steve");

            //ENTER
            driver.PressKeyCode(AndroidKeyCode.Enter);
            // driver.PressKeyCode(66);

            var textSelenium = driver.FindElementByXPath("//android.view.ViewGroup/android.widget.TextView[2]");
            textSelenium.Click();

            var steveText = driver.FindElementByXPath("//android.widget.FrameLayout[2]/android.view.ViewGroup/android.widget.TextView");
            steveText.Click();

            var steveJobsText = driver.FindElementByXPath("//android.widget.TextView[@content-desc='user name']");

            Assert.That(steveJobsText.Text, Is.EqualTo("Steve Jobs"));
        }


    }
}