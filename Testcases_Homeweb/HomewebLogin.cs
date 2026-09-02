using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Homeweb_3._0_Tests.Objects; // Assuming you updated this for Playwright

namespace Homeweb_3._0_Tests.TestCases
{
    [TestFixture]
    public class HomewebLogin : PageTest
    {
        private ExtentReports extent;
        private ExtentTest test;
        private DateTime time = DateTime.Now;

        [OneTimeSetUp]
        public void SetUp()
        {
            // Initialize ExtentReports instance
            extent = ExtentManager.GetReporter();
        }

        [SetUp]
        public void Initialize()
        {
            // Create a new Extent test for the current NUnit test
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            // Note: In Playwright NUnit, the "Page" property is automatically initialized 
            // for every test because we inherit from PageTest. No need to initialize the driver here.
        }
      
        public class TestCaseJsonData
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public string Url { get; set; }
            public string ForgotUsername { get; set; }
            public string Regname { get; set; }
            public string Regcode { get; set; }
            public string RegFirstName { get; set; }
            public string RegLastName { get; set; }
            public string RegPassword { get; set; }
            public string RegMonth { get; set; }
            public string RegDay { get; set; }
            public string RegYear { get; set; }
            public string RegGender { get; set; }
            public string RegPronoun { get; set; }
            public string RegTitle { get; set; }
            public string RegStart { get; set; }
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task LoginCheck(string login, string password, string url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new HomewebLoginObjects(Page);

            // Playwright auto-waits, so Assert.IsNotNull is generally unnecessary if using strict locators,
            // but kept structurally aligned with your script.
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(login);
            await homePage.Next.ClickAsync();

            await homePage.Password.FillAsync(password);

            // Replaced .Submit() with a click (or you could use await homePage.Password.PressAsync("Enter"))
            await homePage.Submit.ClickAsync();

            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
        }

        [Test, TestCaseSource(nameof(LoginJsonData1))]
        public async Task ForgotPassword(string login, string ForgotUsername, string url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new HomewebLoginObjects(Page);

            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(login);
            await homePage.Next.ClickAsync();

            // Playwright window switching
            var popup = await Page.RunAndWaitForPopupAsync(async () =>
            {
                await homePage.ForgotPassword.ClickAsync();
            });

            // Note: Now we interact with 'popup' instead of 'Page'
            var popupHomePage = new HomewebLoginObjects(popup);

            await popupHomePage.Enteremail.FillAsync(ForgotUsername);
            await popupHomePage.Buttonsubmit.ClickAsync();

            await TakeScreenshotAsync(Page, "C:/TestData/Homeweb/ForgotPassword_");
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task Registration(string regName, string regCode, string url, string regFirstName,
            string regLastName, string regPassword, string regMonth, string regDay,
            string regYear, string regGender, string regPronoun, string regTitle, string regStart)
        {

            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);
            var homePage = new HomewebLoginObjects(Page);

            await homePage.Register.ClickAsync();

            await homePage.Orgsearch.PressSequentiallyAsync(regName);
            await Page.Keyboard.PressAsync("Enter");
            await homePage.Searchbutton.ClickAsync();
            await homePage.Selectitem.ClickAsync();

            await homePage.Companycode.PressSequentiallyAsync(regCode);
            await Page.Keyboard.PressAsync("Enter");
            //await homePage.Nextstep.ClickAsync();
      
            await homePage.Firstname.PressSequentiallyAsync(regFirstName);
            await homePage.LastName.FillAsync(regLastName);

            string uniqueEmail = GenerateUniqueEmail();
            await homePage.Email.FillAsync(uniqueEmail);
            await homePage.Password1.FillAsync(regPassword);
            await Page.Keyboard.PressAsync("Enter");

            // Select Dropdowns
            await Page.Locator("select#dobMonth").SelectOptionAsync(new SelectOptionValue { Label = regMonth });
            await Page.Locator("select#dobDay").SelectOptionAsync(new SelectOptionValue { Label = regDay });
            await Page.Locator("select#dobYear").SelectOptionAsync(new SelectOptionValue { Label = regYear });

            await Page.Locator("select#gender").SelectOptionAsync(new SelectOptionValue { Label = regGender });
            await Page.Locator("select#pronoun").SelectOptionAsync(new SelectOptionValue { Label = regPronoun });

            // Accept Policies
            await homePage.NextButton.ScrollIntoViewIfNeededAsync();

            await homePage.CheckPolicy.CheckAsync(); // .CheckAsync() is safer for checkboxes
            await homePage.Marketing.CheckAsync();
            await homePage.NextButton.ClickAsync();

            // Job Information
            await homePage.Employee.ClickAsync();
            await homePage.NextButton1.ClickAsync();

            await homePage.JobTitle.FillAsync(regTitle);
            await Page.Locator("select#startYear").SelectOptionAsync(new SelectOptionValue { Label = regStart });

            // Complete Registration
            await homePage.RegComplete.ClickAsync();
        }

        // --- Data Providers ---

        public static IEnumerable<TestCaseData> LoginJsonData()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\Playwright_Homeweb_3.0\Testdata\HomewebLoginData.json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.Login)
                                                        && !string.IsNullOrEmpty(data.Password)
                                                        && data.Url == "https://homeweb.ca");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.Login, loginData.Password, loginData.Url);
            }
        }

        public static IEnumerable<TestCaseData> LoginJsonData1()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\Playwright_Homeweb_3.0\Testdata\HomewebLoginData.json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.Login)
                                                        && !string.IsNullOrEmpty(data.ForgotUsername)
                                                        && data.Url == "https://homeweb.ca");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.Login, loginData.ForgotUsername, loginData.Url);
            }
        }

        public static IEnumerable<TestCaseData> LoginJsonData2()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\Playwright_Homeweb_3.0\Testdata\HomewebLoginData.json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.Regname)
                                                        && !string.IsNullOrEmpty(data.Regcode)
                                                        && data.Url == "https://homeweb.ca");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.Regname, loginData.Regcode, loginData.Url, loginData.RegFirstName,
                    loginData.RegLastName, loginData.RegPassword, loginData.RegMonth, loginData.RegDay,
                    loginData.RegYear, loginData.RegGender, loginData.RegPronoun, loginData.RegTitle, loginData.RegStart);
            }
        }

        // --- Helper Methods ---

        private async Task TakeScreenshotAsync(IPage page, string filePath)
        {
            string fullPath = filePath + time.ToString("yy_dd_h_mm_ss") + ".png";
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = fullPath });
        }

        private static string GenerateUniqueEmail()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return $"testuser_{timestamp}@demo.com";
        }

        // --- Teardown ---

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.Message;

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Fail("Test Failed");
                test.Log(Status.Fail, $"Test failed with logtrace: {stackTrace}");
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                test.Pass("Test Passed");
            }
            // PageTest automatically cleans up the Page and BrowserContext at the end of each test.
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            // Flush the extent reports
            extent.Flush();
        }
    }
}