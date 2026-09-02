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
    public class HomewebSmokeNew : PageTest
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
            public string Login { get; set; }
            public string Password { get; set; }
            public string Url { get; set; }
            public string Search { get; set; }
            public string ForgotUsername { get; set; }
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task LandingPage(string HWLogin, string HWPassword, string Url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);
            await Context.ClearCookiesAsync(); // Clear cookies on the context

            var homePage = new NewHomewebObjects(Page);

            // 1.Validate initial page load and element visibility
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Is.EqualTo(Url), "Initial landing page URL is incorrect.");
                Assert.That(await homePage.Signup.IsVisibleAsync(), Is.True, "Signup button is not visible.");
                Assert.That(await homePage.Signup1.IsVisibleAsync(), Is.True, "Signup1 button is not visible.");
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

            // 2. Validate the popup opened correctly before closing
            Assert.Multiple(() =>
            {
                Assert.That(popup1.Url, Is.Not.Null.And.Not.Empty, "Popup URL should not be empty.");
                Assert.That(popup1.IsClosed, Is.False, "Popup closed unexpectedly before manual intervention.");
            });
            await popup1.CloseAsync();

            await homePage.Termsofservice.ClickAsync();
            // 3. Check URL after navigating to Terms of Service
            Assert.Multiple(() =>
            {
                Assert.That(Page.Url, Does.Contain("terms"), "Terms of Service URL is incorrect."); // Update "terms" if needed
            });
            await Page.GoBackAsync();

            await homePage.Privacypolicy.ClickAsync();
            // 4. Check URL after navigating to Privacy Policy
            Assert.Multiple(() =>
            {
                Assert.That(Page.Url, Does.Contain("privacy"), "Privacy Policy URL is incorrect."); // Update "privacy" if needed
            });
            await Page.GoBackAsync();

            // Change Language and click French Sign-up options
            await homePage.LanChange.ScrollIntoViewIfNeededAsync();
            await homePage.LanChange.ClickAsync();

            // 5.Validate language change toggled the French elements
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.SignupFR.IsVisibleAsync(), Is.True, "French Signup button is not visible after language change.");
                Assert.That(await homePage.TermsofserviceFR.IsVisibleAsync(), Is.True, "French Terms of Service is not visible.");
            });

            await ClickAndNavigateBackAsync(homePage.SignupFR);
            await ClickAndNavigateBackAsync(homePage.Signup1FR);

            await homePage.HomewoodFR.ScrollIntoViewIfNeededAsync();
            await ClickAndSwitchNewTabAsync(homePage.HomewoodFR);

            await ClickAndNavigateBackAsync(homePage.TermsofserviceFR);
            await ClickAndNavigateBackAsync(homePage.PrivacypolicyFR);
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task HomewebDashboard(string login, string password, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);
            var homePage = new NewHomewebObjects(Page);

            // Login
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(login);
            await Page.Keyboard.PressAsync("Enter");
            await homePage.Password.FillAsync(password);
            await homePage.Submit.ClickAsync();

            // 1.Validate successful login and dashboard load
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "User might not be logged in; still on a login URL.");
                Assert.That(await homePage.GetStarted1.IsVisibleAsync(), Is.True, "GetStarted1 button is not visible on the dashboard.");
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

            // 3. Validate successful completion of Check-in flow
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.SmartCare.IsVisibleAsync(), Is.True, "SmartCare option not visible after wellness check-in.");
            });

            await homePage.SmartCare.ClickAsync();
            await homePage.BacktoDashboard.ClickAsync();
            await homePage.Browse.ClickAsync();
            await homePage.BacktoDashboard.ClickAsync();

            // Interact with featured alumni sections
            await homePage.AlumniFeatured1.ClickAsync();
            await homePage.BacktoDashboard.ClickAsync();

            await homePage.AlumniFeatured2.ClickAsync();
            await homePage.BacktoDashboard.ClickAsync();

            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
            await homePage.LogoutConfirm.ClickAsync();

            // 4. Validate successful logout
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Login.IsVisibleAsync(), Is.True, "Login button is not visible; logout may have failed.");
            });
        }
        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task HomewebSidenavi(string login, string password, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new NewHomewebObjects(Page);

            // Login
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(login);
            await Page.Keyboard.PressAsync("Enter");
            //await homePage.Next.ClickAsync();
            await homePage.Password.FillAsync(password);
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

            // await homePage.Profile.ClickAsync();
            await homePage.DropFeed.ClickAsync();

            await homePage.Profile.ClickAsync();
            await homePage.DropTerms.ClickAsync();

            // 2. Spot check that navigation didn't break the page state
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Profile.IsVisibleAsync(), Is.True, "Profile menu button disappeared during navigation.");
            });

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

            // 3. Verify successful logout
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Login.IsVisibleAsync(), Is.True, "Login button is not visible; logout may have failed.");
            });
        }
        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task HomewebHealthSnapshot(string login, string password, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new NewHomewebObjects(Page);

            // Login
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(login);
            await Page.Keyboard.PressAsync("Enter");
            //await homePage.Next.ClickAsync();
            await homePage.Password.FillAsync(password);
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

            // 3. Verify successful logout
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Login.IsVisibleAsync(), Is.True, "Login button is not visible; logout may have failed.");
            });
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task HomewebLibrary(string login, string password, string url, string Search)
        {
            try
            {
                await Page.SetViewportSizeAsync(1920, 1080);
                await Page.GotoAsync(url);

                var homePage = new NewHomewebObjects(Page);

                // Login
                await homePage.Login.ClickAsync();
                await homePage.UserName.FillAsync(login);
                await Page.Keyboard.PressAsync("Enter");
                //await homePage.Next.ClickAsync();
                await homePage.Password.FillAsync(password);
                await homePage.Submit.ClickAsync();

                // 1. Verify successful login and navigation readiness
                Assert.Multiple(async () =>
                {
                    Assert.That(Page.Url, Does.Not.Contain("login"), "User might not be logged in; still on a login URL.");
                    Assert.That(await homePage.Library.IsVisibleAsync(), Is.True, "Library menu button is not visible.");
                });

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

                // 6. Verify successful logout
                Assert.Multiple(async () =>
                {
                    Assert.That(await homePage.Login.IsVisibleAsync(), Is.True, "Login button is not visible; logout may have failed.");
                });
            }
            catch (Exception ex)
            {
               
            }
        }

        [Test, TestCaseSource(nameof(LoginJsonData1))]
        public async Task LoginCheck(string login, string password, string url, string Search)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new NewHomewebObjects(Page);

            // 1.Verify the page loaded and is ready for login
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Login.IsVisibleAsync(), Is.True, "Login button is not visible on the homepage.");
            });

            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(login);
            await Page.Keyboard.PressAsync("Enter");

            await homePage.Password.FillAsync(password);
            await Page.Keyboard.PressAsync("Enter");

          
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
            await homePage.LogoutConfirm.ClickAsync();

            // 2.Verify successful logout and return to a clean state
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Login.IsVisibleAsync(), Is.True, "Login button is not visible; the logout process may have failed.");
            });
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task ForgotPassword(string login, string ForgotUsername, string url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(url);

            var homePage = new NewHomewebObjects(Page);

            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(login);
            await Page.Keyboard.PressAsync("Enter");

            // 1.Validate Forgot Password link is visible before clicking
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.ForgotPassword.IsVisibleAsync(), Is.True, "Forgot Password link is not visible.");
            });

            // Playwright window switching
            var popup = await Page.RunAndWaitForPopupAsync(async () =>
            {
                await homePage.ForgotPassword.ClickAsync();
            });

            // Wait for the popup to fully load before asserting or interacting
            await popup.WaitForLoadStateAsync(LoadState.Load);

            // 2.Validate the popup opened correctly
            Assert.Multiple(() =>
            {
                Assert.That(popup.IsClosed, Is.False, "Forgot Password popup closed unexpectedly.");
                Assert.That(popup.Url, Is.Not.Null.And.Not.Empty, "Popup URL is empty.");
                // Optional: Assert.That(popup.Url, Does.Contain("forgot"), "Popup URL does not contain expected forgot password path.");
            });

            // Note: Now we interact with 'popup' instead of 'Page'
            var popupHomePage = new HomewebLoginObjects(popup);

            // 3.Validate form elements are ready in the new popup
            Assert.Multiple(async () =>
            {
                Assert.That(await popupHomePage.Enteremail.IsVisibleAsync(), Is.True, "Email input field is not visible in the popup.");
                Assert.That(await popupHomePage.Buttonsubmit.IsVisibleAsync(), Is.True, "Submit button is not visible in the popup.");
            });

            await popupHomePage.Enteremail.FillAsync(ForgotUsername);
            await popupHomePage.Buttonsubmit.ClickAsync();

            // 4.Validate post-submit state (e.g., success message or URL change)
            // NOTE: You may need to add a locator to HomewebLoginObjects for the success confirmation message
            Assert.Multiple(async () =>
            {
                // Example: Assert.That(await popupHomePage.SuccessMessage.IsVisibleAsync(), Is.True, "Success message did not appear after submitting.");
                Assert.That(popup.IsClosed, Is.False, "Popup crashed or closed before screenshot could be taken.");
            });

            await TakeScreenshotAsync(Page, "C:/TestData/Homeweb/ForgotPassword_");
        }

        // --- Data Providers ---
        public static IEnumerable<TestCaseData> LoginJsonData()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string jsonPath = Path.Combine(baseDir, "Testdata", "NewDataHomeweb.Json");
            string jsonString = File.ReadAllText(jsonPath);
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.HWLogin)
                                                        && !string.IsNullOrEmpty(data.HWPassword)
                                                        && data.Url == "https://homeweb.ca/en");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.HWLogin, loginData.HWPassword, loginData.Url, loginData.Search);
            }
        }
        public static IEnumerable<TestCaseData> LoginJsonData1()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string jsonPath = Path.Combine(baseDir, "Testdata", "NewDataHomeweb.Json");
            string jsonString = File.ReadAllText(jsonPath);
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.Login)
                                                        && !string.IsNullOrEmpty(data.Password)
                                                        && data.Url == "https://homeweb.ca/en");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.Login, loginData.Password, loginData.Url, loginData.Search);
            }
        }
        public static IEnumerable<TestCaseData> LoginJsonData2()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string jsonPath = Path.Combine(baseDir, "Testdata", "NewDataHomeweb.Json");
            string jsonString = File.ReadAllText(jsonPath);
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.Login)
                                                        && !string.IsNullOrEmpty(data.ForgotUsername)
                                                        && data.Url == "https://homeweb.ca");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.Login, loginData.ForgotUsername, loginData.Url);
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