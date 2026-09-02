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

namespace Homeweb_3._0_Tests_New.TestCases
{
    [TestFixture]
    public class HomewebEQNew : PageTest
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
            // PageTest automatically initializes Context and Page here.
        }

        // Maximizes the viewport by removing the default fixed size
        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions()
            {
                ViewportSize = ViewportSize.NoViewport
            };
        }

        public class TestCaseJsonData
        {
            public string EQUsername { get; set; }
            public string EQPassword { get; set; }
            public string EQUsername1 { get; set; }
            public string EQPassword1 { get; set; }
            public string Url { get; set; }
            public string EQPolicy { get; set; }
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
            public string Search { get; set; }
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task EQRegistration(string EQPolicy, string Url, string RegFirstName, string RegLastName, string RegPassword,
            string RegMonth, string RegDay, string RegYear, string RegGender, string RegPronoun, string RegTitle, string RegStart)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);

            var homePage = new NewHomewebObjects(Page);


            // Start registration
            await homePage.Register.ScrollIntoViewIfNeededAsync();
            await homePage.Register.ClickAsync();

            await homePage.EQPolicyID.FillAsync(EQPolicy);
            await Page.Keyboard.PressAsync("Enter");


            await homePage.OrgSelect.ClickAsync();


            // Enter Personal Information
            await homePage.Firstname.FillAsync(RegFirstName);
            await homePage.LastName.FillAsync(RegLastName);

            string uniqueEmail = GenerateUniqueEmail();
            await homePage.Email.FillAsync(uniqueEmail);
            await homePage.Password1.FillAsync(RegPassword);
            await Page.Keyboard.PressAsync("Enter");

            // Select Date of Birth
            await Page.Locator("select#dobMonth").SelectOptionAsync(new SelectOptionValue { Label = RegMonth });
            await Page.Locator("select#dobDay").SelectOptionAsync(new SelectOptionValue { Label = RegDay });
            await Page.Locator("select#dobYear").SelectOptionAsync(new SelectOptionValue { Label = RegYear });

            await homePage.NextButton.ScrollIntoViewIfNeededAsync();

            // Select Gender & Pronoun
            await Page.Locator("select#gender").SelectOptionAsync(new SelectOptionValue { Label = RegGender });
            await Page.Locator("select#pronoun").SelectOptionAsync(new SelectOptionValue { Label = RegPronoun });

            // Accept Policies
            await homePage.CheckPolicy.CheckAsync();
            await homePage.Marketing.CheckAsync();

            await homePage.NextButton.ClickAsync();

            // Job Information
            await homePage.Employee.ClickAsync();
            await homePage.NextButton1.ClickAsync();

            await homePage.SelectPartOrg.ClickAsync();
            await homePage.JobTitle.FillAsync(RegTitle);

            await Page.Locator("select#startYear").SelectOptionAsync(new SelectOptionValue { Label = RegStart });

            await homePage.RegComplete.ClickAsync();
        }

        [Test, TestCaseSource(nameof(LoginJsonData1))]
        public async Task EQLandingPage(string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);
            await Context.ClearCookiesAsync();

            var homePage = new NewHomewebObjects(Page);

            // 1.Validate initial page load and visibility of English elements
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Is.EqualTo(Url), "Landing page URL is incorrect.");
                Assert.That(await homePage.Signup.IsVisibleAsync(), Is.True, "English Signup button is not visible.");
                Assert.That(await homePage.Signup1.IsVisibleAsync(), Is.True, "English Signup1 button is not visible.");
            });

            // Sign-up interactions
            await ClickAndNavigateBackAsync(homePage.Signup);
            await ClickAndNavigateBackAsync(homePage.Signup1);

            await homePage.Homewood.ScrollIntoViewIfNeededAsync();
            var popup1 = await Page.RunAndWaitForPopupAsync(async () =>
            {
                await homePage.Homewood.ClickAsync();
            });
            await popup1.WaitForLoadStateAsync(LoadState.Load);

            // 2.Validate the Homewood popup opened correctly
            Assert.Multiple(() =>
            {
                Assert.That(popup1.IsClosed, Is.False, "Homewood popup closed unexpectedly before manual intervention.");
                Assert.That(popup1.Url, Is.Not.Null.And.Not.Empty, "Homewood popup URL is empty.");
            });
            await popup1.CloseAsync();

            await homePage.Termsofservice.ClickAsync();
            await Page.GoBackAsync();

            await homePage.Privacypolicy.ClickAsync();
            await Page.GoBackAsync();

            // Change Language and click French Sign-up options
            await homePage.LanChange.ScrollIntoViewIfNeededAsync();
            await homePage.LanChange.ClickAsync();

            // 3.Validate language change toggled the French elements
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.SignupFR.IsVisibleAsync(), Is.True, "French Signup button is not visible after language change.");
                Assert.That(await homePage.Signup1FR.IsVisibleAsync(), Is.True, "French Signup1 button is not visible.");
            });

            await ClickAndNavigateBackAsync(homePage.SignupFR);
            await ClickAndNavigateBackAsync(homePage.Signup1FR);

            // Click on French Homewood
            await homePage.HomewoodFR.ScrollIntoViewIfNeededAsync();
            await ClickAndSwitchNewTabAsync(homePage.HomewoodFR);

            await ClickAndNavigateBackAsync(homePage.TermsofserviceFR);
            await ClickAndNavigateBackAsync(homePage.PrivacypolicyFR);
        }
        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task EQDashboard(string EQUsername, string EQPassword, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new NewHomewebObjects(Page);

            // Login
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(EQUsername);
            await Page.Keyboard.PressAsync("Enter");
            //await homePage.Next.ClickAsync();
            await homePage.Password.FillAsync(EQPassword);
            await homePage.Submit.ClickAsync();

            // 1.Validate successful login and dashboard load
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "URL indicates the user is still on the login screen.");
                Assert.That(await homePage.GetStarted1.IsVisibleAsync(), Is.True, "GetStarted1 button is not visible on the EQ dashboard.");
            });

            await homePage.GetStarted1.ClickAsync();
            await Page.GoBackAsync();
            await homePage.GetStarted2.ClickAsync();
            await Page.GoBackAsync();
            await homePage.GetStarted3.ClickAsync();
            await Page.GoBackAsync();

            // Top-level Navigation
            await homePage.Journey.ClickAsync();
            await homePage.Library.ClickAsync();
            await homePage.Messages.ClickAsync();
            await homePage.BacktoDashboard.ClickAsync();

            // 2.Validate successful return to dashboard after top navigation
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.CheckinEQ.IsVisibleAsync(), Is.True, "CheckinEQ button is missing after returning to the dashboard.");
            });

            // Check-in
            await homePage.CheckinEQ.ClickAsync();
            await homePage.Gettingby.ClickAsync();
            await homePage.Continue.ClickAsync();
            await homePage.Moodselect.ClickAsync();
            await homePage.Moodselectcontinue.ClickAsync();
            await homePage.BacktoDashboardwellness.ClickAsync();

            // 3.Validate successful completion of Check-in flow
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.SmartCare.IsVisibleAsync(), Is.True, "SmartCare option not visible after returning from wellness check-in.");
            });

            await homePage.SmartCare.ClickAsync();
            await homePage.BacktoDashboard.ClickAsync();

            // Scroll and interact with featured sections
            //await homePage.AlumniFeatured1.ScrollIntoViewIfNeededAsync();
            await homePage.AlumniFeatured1.ClickAsync();
            await homePage.BacktoDashboard.ClickAsync();

            // await homePage.AlumniFeatured2.ScrollIntoViewIfNeededAsync();
            await homePage.AlumniFeatured2.ClickAsync();
            await homePage.BacktoDashboard.ClickAsync();

            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
            await homePage.LogoutConfirm.ClickAsync();

            // 4.Validate successful logout
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Login.IsVisibleAsync(), Is.True, "Login button is not visible; logout may have failed.");
            });
        }

       

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task EQLibrary(string EQUsername, string EQPassword, string url, string Search)
        {
            try
            {
                await Page.SetViewportSizeAsync(1920, 1080);
                await Page.GotoAsync(url);

                var homePage = new NewHomewebObjects(Page);

                // Login
                await homePage.Login.ClickAsync();
                await homePage.UserName.FillAsync(EQUsername);
                await Page.Keyboard.PressAsync("Enter");
                //await homePage.Next.ClickAsync();
                await homePage.Password.FillAsync(EQPassword);
                await homePage.Submit.ClickAsync();
                await homePage.Library.ClickAsync();
                await homePage.Categories.ClickAsync();

                await homePage.GuidedSupport.ClickAsync();
                await homePage.Childcare.ClickAsync();
                await homePage.Childcarelocatorstart.ClickAsync();
                await Page.GoBackAsync();
                await Page.GoBackAsync();

                await homePage.HealthRisk.ClickAsync();
                var newPage1 = await Context.NewPageAsync();
                await newPage1.GotoAsync("https://www.healthycommunity.ca/lifestyles/Profile/UserProfile.aspx?RequiresUpdate=true&site=UpperThamesRiverConservationAuthority");
                await Page.BringToFrontAsync();
                await Page.GoBackAsync();
                await homePage.Sentio1.ClickAsync();
                var newPage = await Context.NewPageAsync();
                await newPage.GotoAsync("https://beta.sentioapp.com/app/en/dashboard");
                await Page.BringToFrontAsync();
                await Page.GoBackAsync();

                await homePage.Foryou.ClickAsync();
                await homePage.HealthandWellness.ClickAsync();
                await ClickAndSwitchNewTabAsync(homePage.HealthandWellnesslibrary);

                await homePage.Library.ClickAsync();
                await homePage.Explore.ClickAsync();

                // Profile and Logout
                await homePage.Profile.ClickAsync();
                await homePage.Logout.ClickAsync();
                await homePage.LogoutConfirm.ClickAsync();
            }
            catch (Exception ex) { }
        }
        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task EQHealthSnapshot(string EQUsername, string EQPassword, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new NewHomewebObjects(Page);

            // Login
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(EQUsername);
            await Page.Keyboard.PressAsync("Enter");
            //await homePage.Next.ClickAsync();
            await homePage.Password.FillAsync(EQPassword);
            await homePage.Submit.ClickAsync();

            // 1.Verify successful login before proceeding
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "URL indicates the user is still on the login screen.");
                Assert.That(await homePage.ViewAll.IsVisibleAsync(), Is.True, "ViewAll button is not visible after login.");
            });

            await homePage.ViewAll.ClickAsync();

            // 2.Validate all health snapshot tabs are present before clicking through them
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Wellnes.IsVisibleAsync(), Is.True, "Wellness tab is not visible.");
                Assert.That(await homePage.Worklife.IsVisibleAsync(), Is.True, "Worklife tab is not visible.");
                Assert.That(await homePage.Sessions.IsVisibleAsync(), Is.True, "Sessions tab is not visible.");
                Assert.That(await homePage.Plans.IsVisibleAsync(), Is.True, "Plans tab is not visible.");
            });

            await homePage.Wellnes.ClickAsync();
            await homePage.Worklife.ClickAsync();
            await homePage.Sessions.ClickAsync();
            await homePage.Plans.ClickAsync();

            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
            await homePage.LogoutConfirm.ClickAsync();

            // 3.Verify successful logout
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Login.IsVisibleAsync(), Is.True, "Login button is not visible; logout may have failed.");
            });
        }
        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task EQSidenavi(string EQUsername, string EQPassword, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new NewHomewebObjects(Page);

            // Login
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(EQUsername);
            await Page.Keyboard.PressAsync("Enter");
            //await homePage.Next.ClickAsync();
            await homePage.Password.FillAsync(EQPassword);
            await homePage.Submit.ClickAsync();

            // 1.Verify successful login before navigating the side menu
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "URL indicates the user is still on the login screen.");
                Assert.That(await homePage.Profile.IsVisibleAsync(), Is.True, "Profile menu button is not visible after login.");
            });

            await homePage.Profile.ClickAsync();
            await homePage.DropJourney.ClickAsync();

            await homePage.Profile.ClickAsync();
            await homePage.DropLib.ClickAsync();

            await homePage.Profile.ClickAsync();
            await homePage.DropMsg.ClickAsync();

            await homePage.Profile.ClickAsync();
            await ClickAndSwitchNewTabAsync(homePage.DropPro);

            // 2.Spot check that opening a new tab didn't break the main page state
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.DropFeed.IsVisibleAsync(), Is.True, "DropFeed button is not visible after returning to the main tab.");
            });

            // await homePage.Profile.ClickAsync();
            await homePage.DropFeed.ClickAsync();

            await homePage.Profile.ClickAsync();
            await homePage.DropTerms.ClickAsync();

            await homePage.Profile.ClickAsync();
            await homePage.DropPrivacy.ClickAsync();

            await homePage.Profile.ClickAsync();
            await ClickAndSwitchNewTabAsync(homePage.DropAbout);

            // Profile and Logout
            // (Assuming DropAbout opening in a new tab left the original page's profile menu open, 
            // or your tab-switching method handles returning focus back to where Logout is accessible)
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Logout.IsVisibleAsync(), Is.True, "Logout button is not visible in the menu.");
            });

            await homePage.Logout.ClickAsync();
            await homePage.LogoutConfirm.ClickAsync();

            // 3.Verify successful logout
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Login.IsVisibleAsync(), Is.True, "Login button is not visible; logout may have failed.");
            });
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
            // Playwright waits for the new popup explicitly, preventing race conditions
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
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\Playwright_Homeweb_3.0\Testdata\NewDataHomeweb.Json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.EQPolicy)
                                                        && data.Url == "https://homeweb.ca/en/equitable");

            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.EQPolicy, loginData.Url, loginData.RegFirstName,
                    loginData.RegLastName, loginData.RegPassword, loginData.RegMonth, loginData.RegDay,
                    loginData.RegYear, loginData.RegGender, loginData.RegPronoun, loginData.RegTitle, loginData.RegStart);
            }
        }

        public static IEnumerable<TestCaseData> LoginJsonData1()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\Playwright_Homeweb_3.0\Testdata\NewDataHomeweb.Json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.EQPolicy)
                                                        && data.Url == "https://homeweb.ca/en/equitable");

            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.Url);
            }
        }

        public static IEnumerable<TestCaseData> LoginJsonData2()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\Playwright_Homeweb_3.0\Testdata\NewDataHomeweb.Json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.EQUsername)
                                                        && !string.IsNullOrEmpty(data.EQPassword)
                                                        && data.Url == "https://homeweb.ca/en/equitable");

            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.EQUsername, loginData.EQPassword, loginData.Url, loginData.Search);
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