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
    public class HomewebAlumni : PageTest
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
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            // PageTest automatically initializes the Page, Context, and Browser.
        }

        // Simulates driver.Manage().Window.Maximize();
        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions()
            {
                ViewportSize = ViewportSize.NoViewport
            };
        }

        public class TestCaseJsonData
        {
            public string AlumniUsername { get; set; }
            public string AlumniPassword { get; set; }
            public string Url { get; set; }
            public string RegFirstName { get; set; }
            public string RegLastName { get; set; }
            public string RegYear { get; set; }
            public string RegPassword { get; set; }
            public string RegCode { get; set; }
            public string RegProvince { get; set; }
            public string RegCity { get; set; }
            public string RegFacility { get; set; }
            public string RegStartYear { get; set; }
            public string RegTreatmentName { get; set; }
            public string Search { get; set; }
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task AlumniRegistration(string RegFirstName, string RegLastName, string Url, string RegYear,
            string RegPassword, string RegCode, string RegProvince, string RegCity, string RegFacility,
            string RegStartYear, string RegTreatmentName)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);

            var homePage = new HomewebLoginObjects(Page);
            

            await homePage.Register.ScrollIntoViewIfNeededAsync();
            await homePage.Register.ClickAsync();

            await homePage.Firstname.FillAsync(RegFirstName);
            await homePage.LastName.FillAsync(RegLastName);

            // Select Date of Birth Year
            await Page.Locator("#dobYear").SelectOptionAsync(new SelectOptionValue { Label = RegYear });

            await homePage.Email.FillAsync(GenerateUniqueEmail());
            await homePage.Password1.FillAsync(RegPassword);
            await Page.Keyboard.PressAsync("Enter");

            await homePage.Alumnireg.ScrollIntoViewIfNeededAsync();
            await homePage.Alumnireg.FillAsync(RegCode);

            // Select Province
            await Page.Locator("#province").SelectOptionAsync(new SelectOptionValue { Label = RegProvince });

            await homePage.Alumnicity.FillAsync(RegCity);

            await homePage.NextButton.ScrollIntoViewIfNeededAsync();
            await homePage.NextButton.ClickAsync();

            // Second form fields
            await Page.Locator("#treatmentFacility").SelectOptionAsync(new SelectOptionValue { Label = RegFacility });
            await Page.Locator("#startYear").SelectOptionAsync(new SelectOptionValue { Label = RegStartYear });
            await Page.Locator("#treatmentProgram").SelectOptionAsync(new SelectOptionValue { Label = RegTreatmentName });

            await homePage.Addtreatment.ScrollIntoViewIfNeededAsync();
            await homePage.Addtreatment.ClickAsync();

            await homePage.alumicompletereg.ScrollIntoViewIfNeededAsync();
            await homePage.alumicompletereg.ClickAsync();
        }

        [Test, TestCaseSource(nameof(LoginJsonData1))]
        public async Task AlumniLandingPage(string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);
            await Context.ClearCookiesAsync();

            var homePage = new HomewebLoginObjects(Page);
           
            // Sign-up navigations
            await homePage.Signup.ScrollIntoViewIfNeededAsync();
            await homePage.Signup.ClickAsync();
            await Page.GoBackAsync();

            await homePage.Signup1.ClickAsync();
            await Page.GoBackAsync();

            // Handle New Tab for Homewood
            await homePage.Homewood.ScrollIntoViewIfNeededAsync();
            var popup1 = await Page.RunAndWaitForPopupAsync(async () =>
            {
                await homePage.Homewood.ClickAsync();
            });
            await popup1.WaitForLoadStateAsync(LoadState.Load);
            await popup1.CloseAsync();

            await homePage.Termsofservice.ClickAsync();
            await Page.GoBackAsync();

            await homePage.Privacypolicy.ClickAsync();
            await Page.GoBackAsync();

            await homePage.Accessibility.ClickAsync();
            await Page.GoBackAsync();

            // Change Language
            await homePage.LanChange.ScrollIntoViewIfNeededAsync();
            await homePage.LanChange.ClickAsync();

            // Scroll and click on French Signups
            await homePage.SignupFR.ScrollIntoViewIfNeededAsync();
            await homePage.SignupFR.ClickAsync();
            await Page.GoBackAsync();

            await homePage.SignupFR1.ScrollIntoViewIfNeededAsync();
            await homePage.SignupFR1.ClickAsync();
            await Page.GoBackAsync();

            // Handle New Tab for HomewoodFR
            await homePage.HomewoodFR.ScrollIntoViewIfNeededAsync();
            var popup2 = await Page.RunAndWaitForPopupAsync(async () =>
            {
                await homePage.HomewoodFR.ClickAsync();
            });
            await popup2.WaitForLoadStateAsync(LoadState.Load);
            await popup2.CloseAsync();

            await homePage.TermsofserviceFR.ClickAsync();
            await Page.GoBackAsync();

            await homePage.PrivacypolicyFR.ClickAsync();
            await Page.GoBackAsync();

            await homePage.AccessibilityFR.ClickAsync();
            // Note: End of test implicitly cleans up context. No need to go back on the final step.
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task AlumniDashboard(string AlumniUsername, string AlumniPassword, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new HomewebLoginObjects(Page);
            
            // Login
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(AlumniUsername);
            await homePage.Next.ClickAsync();
            await homePage.Password.FillAsync(AlumniPassword);
            await homePage.Submit.ClickAsync();

            // Check-in
            await homePage.Checkin.ClickAsync();
            await homePage.Gettingby.ClickAsync();
            await homePage.Continue.ClickAsync();
            await homePage.Moodselect.ClickAsync();
            await homePage.Moodselectcontinue.ClickAsync();

            await homePage.BacktoDashboard.ClickAsync();
            await homePage.Browse.ClickAsync();
            await homePage.BacktoDashboard.ClickAsync();

            // Scroll and interact with featured alumni sections
            await homePage.AlumniFeatured1.ScrollIntoViewIfNeededAsync();
            await homePage.AlumniFeatured1.ClickAsync();
            await homePage.BacktoDashboard.ClickAsync();

            await homePage.AlumniFeatured2.ScrollIntoViewIfNeededAsync();
            await homePage.AlumniFeatured2.ClickAsync();
            await homePage.BacktoDashboard.ClickAsync();

        
            // Search Functionality
            await homePage.Search.ClickAsync();
            await homePage.Searchbox.FillAsync(Search);
            await homePage.ClickSearch.ClickAsync();

            await homePage.ArticleSearch.ScrollIntoViewIfNeededAsync();
            await homePage.ArticleSearch.ClickAsync();

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

        // --- Data Providers ---

        public static IEnumerable<TestCaseData> LoginJsonData()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\QA-Homeweb-Automation-3.0\TestData\HomewebLoginData.json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.RegFirstName)
                                                        && !string.IsNullOrEmpty(data.RegLastName)
                                                        && data.Url == "https://homeweb.ca/alumni");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.RegFirstName, loginData.RegLastName, loginData.Url, loginData.RegYear,
                    loginData.RegPassword, loginData.RegCode, loginData.RegProvince, loginData.RegCity,
                    loginData.RegFacility, loginData.RegStartYear, loginData.RegTreatmentName);
            }
        }

        public static IEnumerable<TestCaseData> LoginJsonData1()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\QA-Homeweb-Automation-3.0\TestData\HomewebLoginData.json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.RegFirstName)
                                                        && !string.IsNullOrEmpty(data.RegLastName)
                                                        && data.Url == "https://homeweb.ca/alumni");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.Url);
            }
        }

        public static IEnumerable<TestCaseData> LoginJsonData2()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\QA-Homeweb-Automation-3.0\TestData\HomewebLoginData.json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.AlumniUsername)
                                                        && !string.IsNullOrEmpty(data.AlumniPassword)
                                                        && data.Url == "https://homeweb.ca/alumni");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.AlumniUsername, loginData.AlumniPassword, loginData.Url, loginData.Search);
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

                    // Optional: Take a screenshot on failure directly to ExtentReports if desired, 
                    // or save it to your local disk.
                    // string path = $"C:/TestData/Homeweb/Failures/Fail_{time:yy_dd_h_mm_ss}.png";
                    // Page.ScreenshotAsync(new PageScreenshotOptions { Path = path }).GetAwaiter().GetResult();
                }
                else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
                {
                    test.Pass("Test Passed");
                }

                // Playwright NUnit PageTest handles closing the browser and context automatically here.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during TearDown: {ex.Message}");
            }
        }
    }
}