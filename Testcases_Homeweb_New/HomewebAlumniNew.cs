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
    public class HomewebAlumniNew : PageTest
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

      //  [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task AlumniRegistration(string RegFirstName, string RegLastName, string Url, string RegYear,
            string RegPassword, string RegCode, string RegProvince, string RegCity, string RegFacility,
            string RegStartYear, string RegTreatmentName)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);

            var homePage = new NewHomewebObjects(Page);


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

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task AlumniLandingPage(string AlumniUsername, string AlumniPassword, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);
            await Context.ClearCookiesAsync();

            var homePage = new NewHomewebObjects(Page);

            // 1.Validate initial page load and visibility of English elements
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Is.EqualTo(url), "Landing page URL is incorrect.");
                Assert.That(await homePage.Signup.IsVisibleAsync(), Is.True, "English Signup button is not visible.");
                Assert.That(await homePage.Signup1.IsVisibleAsync(), Is.True, "English Signup1 button is not visible.");
            });

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

            // 2.Validate the Homewood popup opened correctly
            Assert.Multiple(() =>
            {
                Assert.That(popup1.IsClosed, Is.False, "Homewood popup closed unexpectedly.");
                Assert.That(popup1.Url, Is.Not.Null.And.Not.Empty, "Homewood popup URL is empty.");
            });

            await popup1.CloseAsync();

            await homePage.Termsofservice.ClickAsync();
            await Page.GoBackAsync();

            await homePage.Privacypolicy.ClickAsync();
            await Page.GoBackAsync();

            // Change Language
            await homePage.LanChange.ScrollIntoViewIfNeededAsync();
            await homePage.LanChange.ClickAsync();

            // 3.Validate language change toggled the French elements
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.SignupFR.IsVisibleAsync(), Is.True, "French Signup button is not visible after language change.");
                Assert.That(await homePage.SignupFR1.IsVisibleAsync(), Is.True, "French Signup1 button is not visible.");
            });

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

            // 4.Validate the French Homewood popup opened correctly
            Assert.Multiple(() =>
            {
                Assert.That(popup2.IsClosed, Is.False, "French Homewood popup closed unexpectedly.");
                Assert.That(popup2.Url, Is.Not.Null.And.Not.Empty, "French Homewood popup URL is empty.");
            });

            await popup2.CloseAsync();

            await homePage.TermsofserviceFR.ClickAsync();
            await Page.GoBackAsync();

            await homePage.PrivacypolicyFR.ClickAsync();
            await Page.GoBackAsync();
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task AlumniMoreLandingPage(string AlumniUsername, string AlumniPassword, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);
            await Context.ClearCookiesAsync();

            var homePage = new NewHomewebObjects(Page);

            // 1.Validate the landing page loaded correctly
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Is.EqualTo(url), "Initial landing page URL is incorrect.");
                Assert.That(await homePage.Signup1.IsVisibleAsync(), Is.True, "Signup1 button is not visible.");
            });

            await homePage.Signup1.ClickAsync();

            // 2.Validate toggles and links are visible before interacting
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.SwitchTheme.IsVisibleAsync(), Is.True, "Switch Theme button is not visible.");
                Assert.That(await homePage.Switchlan.IsVisibleAsync(), Is.True, "Switch Language button is not visible.");
                Assert.That(await homePage.Privacypolicyalumni.IsVisibleAsync(), Is.True, "Privacy Policy link is not visible.");
                Assert.That(await homePage.Termsofservicealumni.IsVisibleAsync(), Is.True, "Terms of Service link is not visible.");
            });

            await homePage.SwitchTheme.ClickAsync();
            await homePage.SwitchTheme.ClickAsync(); // Toggling back to original state

            await homePage.Switchlan.ClickAsync();
            await homePage.Switchlan.ClickAsync(); // Toggling back to original state

            await ClickAndSwitchNewTabAsync(homePage.Privacypolicyalumni);
            await ClickAndSwitchNewTabAsync(homePage.Termsofservicealumni);
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task AlumniDashboard(string AlumniUsername, string AlumniPassword, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new NewHomewebObjects(Page);

            // Login
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(AlumniUsername);
            await Page.Keyboard.PressAsync("Enter");
            //await homePage.Next.ClickAsync();
            await homePage.Password.FillAsync(AlumniPassword);
            await homePage.Submit.ClickAsync();

            // 1.Validate successful login and dashboard load
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "URL indicates the user is still on the login screen.");
              
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
                Assert.That(await homePage.Checkin.IsVisibleAsync(), Is.True, "Checkin button is missing after returning to the dashboard.");
            });

            // Check-in flow
            await homePage.Checkin.ClickAsync();
            await homePage.Gettingby.ClickAsync();
            await homePage.Continue.ClickAsync();
            await homePage.Moodselect.ClickAsync();
            await homePage.Moodselectcontinue.ClickAsync();

            await homePage.BacktoDashboardwellness.ClickAsync();

            // 3.Validate successful completion of Check-in flow
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Browse.IsVisibleAsync(), Is.True, "Browse option not visible after returning from wellness check-in.");
            });

            await homePage.Browse.ClickAsync();
            await homePage.BacktoDashboard.ClickAsync();

            // Scroll and interact with featured alumni sections
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
        public async Task AlumniHealthSnapshot(string AlumniUsername, string AlumniPassword, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new NewHomewebObjects(Page);

            // Login
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(AlumniUsername);
            await Page.Keyboard.PressAsync("Enter");
            //await homePage.Next.ClickAsync();
            await homePage.Password.FillAsync(AlumniPassword);
            await homePage.Submit.ClickAsync();

            // 1.Verify successful login before proceeding
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "URL indicates the user is still on the login screen.");
              
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
        public async Task AlumniLibrary(string AlumniUsername, string AlumniPassword, string url, string Search)
        {
            try
            {
                await Page.SetViewportSizeAsync(1920, 1080);
                await Page.GotoAsync(url);

                var homePage = new NewHomewebObjects(Page);

                // Login
                await homePage.Login.ClickAsync();
                await homePage.UserName.FillAsync(AlumniUsername);
                await Page.Keyboard.PressAsync("Enter");
                //await homePage.Next.ClickAsync();
                await homePage.Password.FillAsync(AlumniPassword);
                await homePage.Submit.ClickAsync();
                await homePage.Library.ClickAsync();
                await homePage.Categories.ClickAsync();

                await homePage.GuidedSupport.ClickAsync();
                await homePage.Childcare.ClickAsync();
                await homePage.Childcarelocatorstart.ClickAsync();
                await Page.GoBackAsync();
                await Page.GoBackAsync();

                await homePage.HealthRisk.ClickAsync();
                await homePage.Healthriskassessmentaccept.ClickAsync();
                await Page.GoBackAsync();
                await homePage.Library.ClickAsync();
                await homePage.Explore.ClickAsync();

                // Profile and Logout
                await homePage.Profile.ClickAsync();
                await homePage.Logout.ClickAsync();
                await homePage.LogoutConfirm.ClickAsync();
            }
            catch (Exception ex)
            {
            }
        }
        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task AlumniSidenavi(string AlumniUsername, string AlumniPassword, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new NewHomewebObjects(Page);

            // Login
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(AlumniUsername);
            await Page.Keyboard.PressAsync("Enter");
            //await homePage.Next.ClickAsync();
            await homePage.Password.FillAsync(AlumniPassword);
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

        // --- Data Providers ---

        public static IEnumerable<TestCaseData> LoginJsonData()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string jsonPath = Path.Combine(baseDir, "Testdata", "NewDataHomeweb.Json");
            string jsonString = File.ReadAllText(jsonPath);
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.RegFirstName)
                                                        && !string.IsNullOrEmpty(data.RegLastName)
                                                        && data.Url == "https://homeweb.ca/en/alumni");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.RegFirstName, loginData.RegLastName, loginData.Url, loginData.RegYear,
                    loginData.RegPassword, loginData.RegCode, loginData.RegProvince, loginData.RegCity,
                    loginData.RegFacility, loginData.RegStartYear, loginData.RegTreatmentName);
            }
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

        public static IEnumerable<TestCaseData> LoginJsonData2()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string jsonPath = Path.Combine(baseDir, "Testdata", "NewDataHomeweb.Json");
            string jsonString = File.ReadAllText(jsonPath);
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.AlumniUsername)
                                                        && !string.IsNullOrEmpty(data.AlumniPassword)
                                                        && data.Url == "https://homeweb.ca/en/alumni");
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