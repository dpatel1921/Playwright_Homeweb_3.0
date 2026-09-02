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
    public class HomewebSmoke : PageTest
    {
        private ExtentReports extent;
        private ExtentTest test;
        private DateTime time = DateTime.Now;

        [OneTimeSetUp]
        public void SetUp()
        {
            // Initialize ExtentReports instance (Maintains compatibility with your v2/v3 setup)
            extent = ExtentManager.GetReporter();
        }

        [SetUp]
        public void Initialize()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            // PageTest handles the browser and Page initialization automatically.
        }

        // To mimic driver.Manage().Window.Maximize();
        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions()
            {
                ViewportSize = ViewportSize.NoViewport
            };
        }

        public class TestCaseJsonData
        {
            public string HWLogin { get; set; }
            public string HWPassword { get; set; }
            public string Url { get; set; }
            public string Search { get; set; }
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task LandingPage(string HWLogin, string HWPassword, string Url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);
            await Context.ClearCookiesAsync(); // Clear cookies on the context

            var homePage = new HomewebLoginObjects(Page);

            // Sign-up interactions
            await ClickAndNavigateBackAsync(homePage.Signup);
            await ClickAndNavigateBackAsync(homePage.Signup1);

            // Click on Homewood in footer
            await homePage.Homewood.ScrollIntoViewIfNeededAsync();
            await ClickAndSwitchNewTabAsync(homePage.Homewood);

            await ClickAndNavigateBackAsync(homePage.Termsofservice);
            await ClickAndNavigateBackAsync(homePage.Privacypolicy);
            await ClickAndNavigateBackAsync(homePage.Accessibility);

        

            // Change Language and click French Sign-up options
            await homePage.LanChange.ScrollIntoViewIfNeededAsync();
            await homePage.LanChange.ClickAsync();

            await ClickAndNavigateBackAsync(homePage.SignupFR);
            await ClickAndNavigateBackAsync(homePage.Signup1FR);

    

            await homePage.HomewoodFR.ScrollIntoViewIfNeededAsync();
            await ClickAndSwitchNewTabAsync(homePage.HomewoodFR);

            await ClickAndNavigateBackAsync(homePage.TermsofserviceFR);
            await ClickAndNavigateBackAsync(homePage.PrivacypolicyFR);
            await ClickAndNavigateBackAsync(homePage.AccessibilityFR);
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task HomewebDashboard(string login, string password, string url, string Search)
        {
           
            await Page.SetViewportSizeAsync(1920, 1080);

            await Page.GotoAsync(url);
            var homePage = new HomewebLoginObjects(Page);
            //var alumniSignup = new HomewebAlumniObjects(Page); // Assuming you updated this POM too

            // Login process
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(login);
            await homePage.Next.ClickAsync();
            await homePage.Password.FillAsync(password);
            await homePage.Submit.ClickAsync();

            // Dashboard interactions
            await homePage.Checkin.ClickAsync();
            await homePage.Gettingby.ClickAsync();

            // Watch tutorial and handle new tab
            await ClickAndSwitchNewTabAsync(homePage.Watchtutorial);

            await homePage.Continue.ClickAsync();
            await homePage.Moodselect.ClickAsync();
            await homePage.Moodselectcontinue.ClickAsync();

            await homePage.BacktoDashboard.ClickAsync();
            await ClickAndNavigateBackAsync(homePage.Launchpathfinder);
            await ClickAndNavigateBackAsync(homePage.Browse);

            // Scroll and interact with features
            await homePage.Recommends.ScrollIntoViewIfNeededAsync();
            await ClickAndNavigateBackAsync(homePage.Recommends);

         
            await homePage.Resources.ScrollIntoViewIfNeededAsync();
            await ClickAndNavigateBackAsync(homePage.Resources);

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

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task HomewebExternallinks(string login, string password, string url, string Search)
        {
            await Page.GotoAsync(url);
            await Page.SetViewportSizeAsync(1920, 1080);


            var homePage = new HomewebLoginObjects(Page);

            // Login process
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(login);
            await homePage.Next.ClickAsync();
            await homePage.Password.FillAsync(password);
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

       // [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task HomewebResources(string login, string password, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);
            var homePage = new HomewebLoginObjects(Page);

            // Login process
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(login);
            await homePage.Next.ClickAsync();
            await homePage.Password.FillAsync(password);
            await homePage.Submit.ClickAsync();

                    

           // Return to resources
            await homePage.BacktoResources.ScrollIntoViewIfNeededAsync();
            await homePage.BacktoResources.ClickAsync();

            // Access Wellness sessions
            await homePage.Wellnesssessions.ClickAsync();
            await homePage.Beyondstigma.ScrollIntoViewIfNeededAsync();
            await homePage.Beyondstigma.ClickAsync();

            await homePage.BacktoResources.ScrollIntoViewIfNeededAsync();
            await homePage.BacktoResources.ClickAsync();

            // Build Resilience & Adapting to Change
            await homePage.Buildyourresi.ClickAsync();
            await homePage.Adpatingtochange.ScrollIntoViewIfNeededAsync();
            await homePage.Adpatingtochange.ClickAsync();

            await homePage.BacktoResources.ScrollIntoViewIfNeededAsync();
            await homePage.BacktoResources.ClickAsync();

            // Mental Health Podcast
            await homePage.MentalHealth.ClickAsync();
            await homePage.Childmentalpodcast.ScrollIntoViewIfNeededAsync();
            await homePage.Childmentalpodcast.ClickAsync();

            await homePage.Podcastplay.ScrollIntoViewIfNeededAsync();
            await homePage.Podcastplay.ClickAsync();

            await homePage.BacktoResources.ScrollIntoViewIfNeededAsync();
            await homePage.BacktoResources.ClickAsync();

            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
        }

        // --- Data Providers ---
        public static IEnumerable<TestCaseData> LoginJsonData()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\QA-Homeweb-Automation-3.0\TestData\HomewebLoginData.json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.HWLogin)
                                                        && !string.IsNullOrEmpty(data.HWPassword)
                                                        && data.Url == "https://homeweb.ca");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.HWLogin, loginData.HWPassword, loginData.Url, loginData.Search);
            }
        }

        // --- Playwright Helper Methods ---

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
            // Wait for the network to settle on the new page before going back
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await Page.GoBackAsync();
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
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
        }
    }
}