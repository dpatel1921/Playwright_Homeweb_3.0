using AventStack.ExtentReports;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Homeweb_3._0_Tests.Objects;

namespace Homeweb_3._0_Tests.TestCases
{
    [TestFixture]
    public class HomewebLSO : PageTest
    {
        private ExtentReports extent;
        private ExtentTest test;
        private DateTime time = DateTime.Now;

        [OneTimeSetUp]
        public void SetUp()
        {
            extent = ExtentManager.GetReporter();
        }

        [SetUp]
        public void Initialize()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            // PageTest handles browser and context initialization natively
        }

        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions()
            {
                ViewportSize = ViewportSize.NoViewport
            };
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
            public string LSOUsername { get; set; }
            public string LSOPassword { get; set; }
            public string Search { get; set; }
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task LSORegistration(string Url, string RegFirstName, string RegLastName, string RegPassword,
            string RegMonth, string RegDay, string RegYear, string RegGender, string RegPronoun)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);

            var homePage = new HomewebLoginObjects(Page);
           

            await homePage.Register.ScrollIntoViewIfNeededAsync();
            await homePage.Register.ClickAsync();

            // Fill registration form
            await homePage.Firstname.FillAsync(RegFirstName);
            await homePage.LastName.FillAsync(RegLastName);

            string uniqueEmail = GenerateUniqueEmail();
            await homePage.Email.FillAsync(uniqueEmail);

            await homePage.Password1.FillAsync(RegPassword);
            await Page.Keyboard.PressAsync("Enter");// Assuming Password1 is the reg field

            // Select Date of Birth
            await Page.Locator("select#dobMonth").SelectOptionAsync(new SelectOptionValue { Label = RegMonth });
            await Page.Locator("select#dobDay").SelectOptionAsync(new SelectOptionValue { Label = RegDay });
            await Page.Locator("select#dobYear").SelectOptionAsync(new SelectOptionValue { Label = RegYear });

            // Select Gender & Pronoun
            await Page.Locator("select#gender").SelectOptionAsync(new SelectOptionValue { Label = RegGender });
            await Page.Locator("select#pronoun").SelectOptionAsync(new SelectOptionValue { Label = RegPronoun });

            // Accept Policies
            await homePage.NextButton.ScrollIntoViewIfNeededAsync();

            await homePage.CheckPolicy.CheckAsync();
            await homePage.Marketing.CheckAsync();

            await homePage.NextButton.ClickAsync();

            // LSO Selection
            await homePage.LSOselect.ClickAsync();
            await homePage.Chevron.ClickAsync();

            // Selecting Region & Role (Mimicking Selenium Actions Hover -> Click)
            await homePage.LSOregion.HoverAsync();
            await homePage.LSOregion.ClickAsync();

            await homePage.LSOrole.HoverAsync();
            await homePage.LSOrole.ClickAsync();

            // Complete Registration
            await homePage.LSORegcomplete.ClickAsync();
        }

        [Test, TestCaseSource(nameof(LoginJsonData1))]
        public async Task LSOLandingPage(string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);
            await Context.ClearCookiesAsync();

            var homePage = new HomewebLoginObjects(Page);
         

            // Sign-up interactions
            await ClickAndNavigateBackAsync(homePage.Signup);
            await ClickAndNavigateBackAsync(homePage.Signup1);

            // Click on Homewood in footer
            await homePage.Homewood.ScrollIntoViewIfNeededAsync();
            await ClickAndSwitchNewTabAsync(homePage.Homewood);

            // Click on Terms of Service & Privacy Policy
            await homePage.Termsofservice.ScrollIntoViewIfNeededAsync();
            await ClickAndNavigateBackAsync(homePage.Termsofservice);
            await ClickAndNavigateBackAsync(homePage.Privacypolicy);
            await ClickAndNavigateBackAsync(homePage.Accessibility);

            // Click on LSO links
            await homePage.LSO1.ScrollIntoViewIfNeededAsync();
            await ClickAndSwitchNewTabAsync(homePage.LSO1);
            await ClickAndSwitchNewTabAsync(homePage.LSO2);
            await ClickAndSwitchNewTabAsync(homePage.LSO3);
            await ClickAndSwitchNewTabAsync(homePage.LSO4);
            await ClickAndSwitchNewTabAsync(homePage.LSO5);

            // Change Language and click French Sign-up options
            await homePage.LanChange.ScrollIntoViewIfNeededAsync();
            await homePage.LanChange.ClickAsync();

            await ClickAndNavigateBackAsync(homePage.SignupFR);
            await ClickAndNavigateBackAsync(homePage.Signup1FR);

            // Click on additional LSO links
            await homePage.LSO6.ScrollIntoViewIfNeededAsync();
            await ClickAndSwitchNewTabAsync(homePage.LSO6);
            await ClickAndSwitchNewTabAsync(homePage.LSO7);
            await ClickAndSwitchNewTabAsync(homePage.LSO8);
            await ClickAndSwitchNewTabAsync(homePage.LSO9);

            // Click on French Homewood
            await homePage.HomewoodFR.ScrollIntoViewIfNeededAsync();
            await ClickAndSwitchNewTabAsync(homePage.HomewoodFR);

            // Click on French Terms of Service & Privacy Policy
            await ClickAndNavigateBackAsync(homePage.TermsofserviceFR);
            await ClickAndNavigateBackAsync(homePage.PrivacypolicyFR);
            await ClickAndNavigateBackAsync(homePage.AccessibilityFR);
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task LSODashboard(string LSOUsername, string LSOPassword, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new HomewebLoginObjects(Page);
         

            // Login process
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(LSOUsername);
            await homePage.Next.ClickAsync();
            await homePage.Password.FillAsync(LSOPassword);
            await homePage.Submit.ClickAsync();

            // Dashboard interactions
            await homePage.Checkin.ClickAsync();
            await homePage.Gettingby.ClickAsync();

            await ClickAndSwitchNewTabAsync(homePage.Watchtutorial);

            await homePage.Continue.ClickAsync();
            await homePage.Moodselect.ClickAsync();
            await homePage.Moodselectcontinue.ClickAsync();

            await homePage.BacktoDashboard.ClickAsync();

            // Browse different dashboard sections
            await ClickAndNavigateBackAsync(homePage.Launchpathfinder);
            await ClickAndNavigateBackAsync(homePage.Browse);

            await homePage.Recommends.ScrollIntoViewIfNeededAsync();
            await ClickAndNavigateBackAsync(homePage.Recommends);

            // Scroll and click on LSO Dashboard elements
            await homePage.LSODashboard.ScrollIntoViewIfNeededAsync();
            await ClickAndNavigateBackAsync(homePage.LSODashboard);

               

            await homePage.Search.ClickAsync();
            await homePage.Searchbox.FillAsync(Search);
            await homePage.ClickSearch.ClickAsync();

            // Scroll and click on search result
            await homePage.ArticleSearch.ScrollIntoViewIfNeededAsync();
            await homePage.ArticleSearch.ClickAsync();

            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task LSOExternallinks(string LSOUsername, string LSOPassword, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new HomewebLoginObjects(Page);

            // Login process
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(LSOUsername);
            await homePage.Next.ClickAsync();
            await homePage.Password.FillAsync(LSOPassword);
            await homePage.Submit.ClickAsync();

            // Childcare Locator
            await homePage.Childcarelocator.ScrollIntoViewIfNeededAsync();
            await homePage.Childcarelocator.ClickAsync();

            await homePage.Childcarelocatorstart.ScrollIntoViewIfNeededAsync();
            await homePage.Childcarelocatorstart.ClickAsync();

            //await homePage.Childcarelocatoraccept.ClickAsync();
            await Page.GoBackAsync();
            await Page.GoBackAsync();
            await homePage.BacktoDashboard.ClickAsync();

            // Health and Wellness
            await homePage.HealthandWellness.ScrollIntoViewIfNeededAsync();
            await homePage.HealthandWellness.ClickAsync();

            await homePage.HealthandWellnesslibrary.ScrollIntoViewIfNeededAsync();
            await ClickAndSwitchNewTabAsync(homePage.HealthandWellnesslibrary);


            await homePage.BacktoDashboard.ClickAsync();

            // Health Risk Assessment
            await homePage.Healthriskassessment.ScrollIntoViewIfNeededAsync();
            await homePage.Healthriskassessment.ClickAsync();

            await homePage.Healthriskassessmentaccept.ScrollIntoViewIfNeededAsync();

            // Using Playwright's native popup handler instead of JS Executor
            //  var popup = await Page.RunAndWaitForPopupAsync(async () =>
            // {
            //    await homePage.Healthriskassessmentaccept.ClickAsync();
            // });

            // await popup.CloseAsync();
            await homePage.BacktoDashboard.ClickAsync();

            // Sentio
            await homePage.Sentio.ScrollIntoViewIfNeededAsync();
            await homePage.Sentio.ClickAsync();

            await homePage.SentioStart.ScrollIntoViewIfNeededAsync();
            await homePage.SentioStart.ClickAsync();


            //await homePage.Childcarelocatoraccept.ClickAsync();

            await Page.GoBackAsync();
            await homePage.BacktoDashboard.ClickAsync();

            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
        }
        

        // --- Helper Methods ---

        private static string GenerateUniqueEmail()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return $"testuser_{timestamp}@demo.com";
        }

        private async Task TakeScreenshotAsync(IPage targetPage, string filePath)
        {
            string fullPath = filePath + time.ToString("yy_dd_h_mm_ss") + ".png";
            await targetPage.ScreenshotAsync(new PageScreenshotOptions { Path = fullPath });
        }

        private async Task ClickAndSwitchNewTabAsync(ILocator locator)
        {
            var popup = await Page.RunAndWaitForPopupAsync(async () =>
            {
                await locator.ClickAsync();
            });
            await popup.WaitForLoadStateAsync(LoadState.Load);
            await popup.CloseAsync();
        }

        private async Task ClickAndNavigateBackAsync(ILocator locator)
        {
            await locator.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await Page.GoBackAsync();
        }

        // --- Data Providers ---

        public static IEnumerable<TestCaseData> LoginJsonData()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\QA-Homeweb-Automation-3.0\TestData\HomewebLoginData.json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.RegFirstName)
                                                        && !string.IsNullOrEmpty(data.RegLastName)
                                                        && data.Url == "https://homeweb.ca/map");

            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.Url, loginData.RegFirstName, loginData.RegLastName,
                    loginData.RegPassword, loginData.RegMonth, loginData.RegDay, loginData.RegYear,
                    loginData.RegGender, loginData.RegPronoun);
            }
        }

        public static IEnumerable<TestCaseData> LoginJsonData1()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\QA-Homeweb-Automation-3.0\TestData\HomewebLoginData.json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.RegFirstName)
                                                        && !string.IsNullOrEmpty(data.RegLastName)
                                                        && data.Url == "https://homeweb.ca/map");

            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.Url);
            }
        }

        public static IEnumerable<TestCaseData> LoginJsonData2()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\QA-Homeweb-Automation-3.0\TestData\HomewebLoginData.json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.LSOUsername)
                                                        && !string.IsNullOrEmpty(data.LSOPassword)
                                                        && data.Url == "https://homeweb.ca/map");

            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.LSOUsername, loginData.LSOPassword, loginData.Url, loginData.Search);
            }
        }

        // --- Teardown ---

        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
        }

        [TearDown]
        public void AfterTest()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stackTrace = TestContext.CurrentContext.Result.Message;

                if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    test.Fail("Test Failed");
                    test.Log(Status.Fail, "Test failed with logtrace: " + stackTrace);
                }
                else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
                {
                    test.Pass("Test Passed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during TearDown: {ex.Message}");
            }
        }
    }
}