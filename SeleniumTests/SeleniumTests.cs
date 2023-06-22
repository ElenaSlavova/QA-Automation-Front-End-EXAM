

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;

namespace SeleniumTests
{
    public class SeleniumTests
    {

        private WebDriver driver;
        private const string baseUrl = "https://contactbook-1.slavova1.repl.co/";

        [SetUp]
        public void OpenWebApp()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);
            driver.Url = baseUrl;
        }

        [TearDown]
        public void CloseWebApp()
        {
            driver.Quit();
        }

        [Test]
        public void Test_VerifyFirstContactName()
        {
            var linkContacts = driver.FindElement(By.CssSelector("aside > ul > li:nth-child(2) > a"));
            linkContacts.Click();

            var firstNameContact1 = driver.FindElement(By.CssSelector("#contact1 > tbody > tr.fname > td")).Text;
            var lastNameContact1 = driver.FindElement(By.CssSelector("#contact1 > tbody > tr.lname > td")).Text;

            Assert.That(firstNameContact1, Is.EqualTo("Steve"));
            Assert.That(lastNameContact1, Is.EqualTo("Jobs"));

        }

        [Test]
        public void Test_VerifySearchResultContactName()
        {
            var linkSearch = driver.FindElement(By.CssSelector("aside > ul > li:nth-child(4) > a"));
            linkSearch.Click();

            var fieldSearch = driver.FindElement(By.Id("keyword"));
            fieldSearch.SendKeys("albert");
            var buttonSearch = driver.FindElement(By.Id("search"));
            buttonSearch.Click();

            var firstNameSearchResultContact1 = driver.FindElement(By.CssSelector("#contact3 > tbody > tr.fname > td")).Text;
            var lastNameSearchResultContact1 = driver.FindElement(By.CssSelector("#contact3 > tbody > tr.lname > td")).Text;

            Assert.That(firstNameSearchResultContact1, Is.EqualTo("Albert"));
            Assert.That(lastNameSearchResultContact1, Is.EqualTo("Einstein")); 

        }

        [Test]
        public void Test_VerifyNoSearchResult()
        {
            var linkSearch = driver.FindElement(By.CssSelector("aside > ul > li:nth-child(4) > a"));
            linkSearch.Click();

            var fieldSearch = driver.FindElement(By.Id("keyword"));
            fieldSearch.SendKeys("missing alabala");
            var buttonSearch = driver.FindElement(By.Id("search"));
            buttonSearch.Click();

            var fieldSearchResultMessage = driver.FindElement(By.Id("searchResult")).Text;
            
            Assert.That(fieldSearchResultMessage, Is.EqualTo("No contacts found."));
            
        }


        [Test]
        public void Test_CreateContactWithInvalidFirstName()
        {
            var linkCreate = driver.FindElement(By.CssSelector("aside > ul > li:nth-child(3) > a"));
            linkCreate.Click();

            var inputFirstName = driver.FindElement(By.Id("firstName"));
            var inputLastName = driver.FindElement(By.Id("lastName"));
            inputLastName.SendKeys("testlastname");
            var inputPhone = driver.FindElement(By.Id("phone"));
            inputPhone.SendKeys("0123456789");
            var inputEmail = driver.FindElement(By.Id("email"));
            inputEmail.SendKeys("test@test.com");
            var inputComments = driver.FindElement(By.Id("comments"));
            inputComments.SendKeys("this is a test comment");

            var buttonCreate = driver.FindElement(By.Id("create"));
            buttonCreate.Click();

            var fieldCreateResultMessage = driver.FindElement(By.ClassName("err"));

            Assert.IsTrue(fieldCreateResultMessage.Displayed);
            Assert.That(fieldCreateResultMessage.Text, Is.EqualTo("Error: First name cannot be empty!"));
        }

        [Test]
        public void Test_CreateContactValidData()
        {
            var linkCreate = driver.FindElement(By.CssSelector("aside > ul > li:nth-child(3) > a"));
            linkCreate.Click();

            var inputFirstName = driver.FindElement(By.Id("firstName"));
            inputFirstName.SendKeys("John");
            var inputLastName = driver.FindElement(By.Id("lastName"));
            inputLastName.SendKeys("Smith");
            var inputEmail = driver.FindElement(By.Id("email"));
            inputEmail.SendKeys("test@test.com");
          

            var buttonCreate = driver.FindElement(By.Id("create"));
            buttonCreate.Click();

            var newCreatedContact = driver.FindElements(By.CssSelector(".contacts-grid a")).Last();

            var newlyCreatedContactFirstName = newCreatedContact.FindElement(By.CssSelector(".fname td")).Text;
            var newlyCreatedContactLastName = newCreatedContact.FindElement(By.CssSelector(".lname td")).Text;
            var newlyCreatedContactEmail = newCreatedContact.FindElement(By.CssSelector(".email td")).Text;

            Assert.IsTrue(newCreatedContact.Displayed);
            Assert.That(newlyCreatedContactFirstName, Is.EqualTo("John"));
            Assert.That(newlyCreatedContactLastName, Is.EqualTo("Smith"));
            Assert.That(newlyCreatedContactEmail, Is.EqualTo("test@test.com"));
        }

    }
}