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
    public class SmartCareNavigation : PageTest
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
            // PageTest handles browser context and page initialization automatically.
        }

        // Maximizes the viewport by removing the default fixed size constraints
        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions()
            {
                ViewportSize = ViewportSize.NoViewport
            };
        }

        public class TestCaseJsonData
        {
            public string PFLogin { get; set; }
            public string PFPassword { get; set; }
            public string Url { get; set; }
            public string PFLogin1 { get; set; }
            public string PFLogin2 { get; set; }
        }

        [Test, TestCaseSource(nameof(LoginJsonData)), Order(1)]
        public async Task Booking(string PFLogin, string PFPassword, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);
            var homePage = new NewHomewebObjects(Page);

            // Login process
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(PFLogin);
            await Page.Keyboard.PressAsync("Enter");
            await homePage.Password.FillAsync(PFPassword);
            await homePage.Submit.ClickAsync();

            // 1.Validate successful login and dashboard load
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "URL indicates the user is still on the login screen.");
                Assert.That(await homePage.SmartCare.IsVisibleAsync(), Is.True, "SmartCare button is not visible after login.");
            });

            await homePage.SmartCare.ClickAsync();

            await homePage.Problemissue.ClickAsync();
            await homePage.Problemissue1.ClickAsync();
            await homePage.PFAssessment.ClickAsync();
            await homePage.PFAssessment1.ClickAsync();
            await homePage.PFAssessment2.ClickAsync();
            await homePage.PFAssessment3.ClickAsync();
            await homePage.PFAssessment4.ClickAsync();

            await homePage.PFAssessmentbutton.ClickAsync();

            // 2. Validate transition to the patient selection screen
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Yourself.IsVisibleAsync(), Is.True, "'Yourself' selection option is not visible after the assessment.");
            });

            await homePage.Yourself.ClickAsync();

            //await homePage.NextPF.ScrollIntoViewIfNeededAsync();
            await homePage.NextPF.ClickAsync();
            await homePage.SelectTime.ClickAsync();

            // Select Appointment Modality
            await Page.Locator("//SELECT[@id='appointmentModality']").SelectOptionAsync(new SelectOptionValue { Label = "Phone" });

            await homePage.SelectMode.ClickAsync();
            await homePage.SelectYes.ClickAsync();
            await homePage.SelectYes1.ClickAsync();
            await homePage.SelectText.ClickAsync();
            await homePage.SelectCheck.ClickAsync();
            await homePage.SelectNext.ClickAsync();

         
            await homePage.SelectDashboard.ClickAsync();

            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
            await homePage.LogoutConfirm.ClickAsync();

            
        }

        [Test, TestCaseSource(nameof(LoginJsonData)), Order(2)]
        public async Task Cancel(string PFLogin, string PFPassword, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);
            var homePage = new NewHomewebObjects(Page);

            // Login process
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(PFLogin);
            await Page.Keyboard.PressAsync("Enter");
            await homePage.Password.FillAsync(PFPassword);
            await homePage.Submit.ClickAsync();

            // 1.Validate successful login and dashboard load
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "URL indicates the user is still on the login screen.");
                Assert.That(await homePage.Journey.IsVisibleAsync(), Is.True, "Journey button is not visible after login.");
            });

            // Cancel Flow
            await homePage.Journey.ClickAsync();

            // 2.Validate Journey page load before viewing details
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.ViewDetails.IsVisibleAsync(), Is.True, "ViewDetails button is not visible on the Journey page.");
            });

            await homePage.ViewDetails.ClickAsync();
            await homePage.Cancel.ClickAsync();
            await homePage.CancelYes.ClickAsync();

            // 3.Validate cancellation completion (modal closed)
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.CancelYes.IsVisibleAsync(), Is.False, "Cancel confirmation modal did not close after clicking Yes.");
            });

            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
            await homePage.LogoutConfirm.ClickAsync();

          
        }

        [Test, TestCaseSource(nameof(LoginJsonData)), Order(3)]
        public async Task EndServices(string PFLogin, string PFPassword, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);
            var homePage = new NewHomewebObjects(Page);

            // Login process
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(PFLogin);
            await Page.Keyboard.PressAsync("Enter");
            await homePage.Password.FillAsync(PFPassword);
            await homePage.Submit.ClickAsync();

            // 1.Validate successful login
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "URL indicates the user is still on the login screen.");
                Assert.That(await homePage.Journey.IsVisibleAsync(), Is.True, "Journey button is not visible after login.");
            });

            // End Services Flow
            await homePage.Journey.ClickAsync();

            // 2.Validate Journey page loaded before attempting to end services
            Assert.Multiple(async () =>
            {
                // Adjust this assertion if EndServices isn't always expected to be visible (since you use TryClickAsync)
                Assert.That(await homePage.Profile.IsVisibleAsync(), Is.True, "Profile button is missing; page may not have loaded correctly.");
            });

            await TryClickAsync(homePage.EndServices);
            await TryClickAsync(homePage.YesDone);
            await TryClickAsync(homePage.Reason);

            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
            await homePage.LogoutConfirm.ClickAsync();

           
        }

        [Test, TestCaseSource(nameof(LoginJsonData1)), Order(4)]
        public async Task Booking1(string PFLogin1, string PFPassword, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);
            var homePage = new NewHomewebObjects(Page);

            // Login process
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(PFLogin1);
            await Page.Keyboard.PressAsync("Enter");
            await homePage.Password.FillAsync(PFPassword);
            await homePage.Submit.ClickAsync();
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "URL indicates the user is still on the login screen.");
                Assert.That(await homePage.SmartCare.IsVisibleAsync(), Is.True, "SmartCare button is not visible after login.");
            });
            await homePage.SmartCare.ClickAsync();

            await homePage.Problemissue.ClickAsync();
            await homePage.Problemissue1.ClickAsync();
            await homePage.PFAssessment.ClickAsync();
            await homePage.PFAssessment1.ClickAsync();
            await homePage.PFAssessment2.ClickAsync();
            await homePage.PFAssessment3.ClickAsync();
            await homePage.PFAssessment4.ClickAsync();

            await homePage.PFAssessmentbutton.ClickAsync();
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Yourself.IsVisibleAsync(), Is.True, "'Yourself' selection option is not visible after the assessment.");
            });
            await homePage.Yourself.ClickAsync();

            //await homePage.NextPF.ScrollIntoViewIfNeededAsync();
            await homePage.NextPF.ClickAsync();
            await homePage.SelectTime.ClickAsync();

            // Select Appointment Modality
            await Page.Locator("//SELECT[@id='appointmentModality']").SelectOptionAsync(new SelectOptionValue { Label = "Phone" });

            await homePage.SelectMode.ClickAsync();
            await homePage.SelectYes.ClickAsync();
            await homePage.SelectYes1.ClickAsync();
            await homePage.SelectText.ClickAsync();
            await homePage.SelectCheck.ClickAsync();
            await homePage.SelectNext.ClickAsync();
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.SelectDashboard.IsVisibleAsync(), Is.True, "Return to Dashboard button is not visible; booking confirmation may have failed.");
            });
            await homePage.SelectDashboard.ClickAsync();

            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
            await homePage.LogoutConfirm.ClickAsync();
           
        }

        [Test, TestCaseSource(nameof(LoginJsonData1)), Order(5)]
        public async Task Cancel1(string PFLogin1, string PFPassword, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);
            var homePage = new NewHomewebObjects(Page);

            // Login process
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(PFLogin1);
            await Page.Keyboard.PressAsync("Enter");
            await homePage.Password.FillAsync(PFPassword);
            await homePage.Submit.ClickAsync();
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "URL indicates the user is still on the login screen.");
                Assert.That(await homePage.Journey.IsVisibleAsync(), Is.True, "Journey button is not visible after login.");
            });
            // Cancel Flow
            await homePage.Journey.ClickAsync();
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.ViewDetails.IsVisibleAsync(), Is.True, "ViewDetails button is not visible on the Journey page.");
            });
            await homePage.ViewDetails.ClickAsync();
            await homePage.Cancel.ClickAsync();
            await homePage.CancelYes.ClickAsync();
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.CancelYes.IsVisibleAsync(), Is.False, "Cancel confirmation modal did not close after clicking Yes.");
            });

            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
            await homePage.LogoutConfirm.ClickAsync();
         
        }

        [Test, TestCaseSource(nameof(LoginJsonData1)), Order(6)]
        public async Task EndServices1(string PFLogin1, string PFPassword, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);
            var homePage = new NewHomewebObjects(Page);

            // Login process
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(PFLogin1);
            await Page.Keyboard.PressAsync("Enter");
            await homePage.Password.FillAsync(PFPassword);
            await homePage.Submit.ClickAsync();
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "URL indicates the user is still on the login screen.");
                Assert.That(await homePage.Journey.IsVisibleAsync(), Is.True, "Journey button is not visible after login.");
            });

            // End Services Flow
            await homePage.Journey.ClickAsync();
            Assert.Multiple(async () =>
            {
                // Adjust this assertion if EndServices isn't always expected to be visible (since you use TryClickAsync)
                Assert.That(await homePage.Profile.IsVisibleAsync(), Is.True, "Profile button is missing; page may not have loaded correctly.");
            });
            await TryClickAsync(homePage.EndServices);


            await TryClickAsync(homePage.YesDone);
            await TryClickAsync(homePage.Reason);


            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
            await homePage.LogoutConfirm.ClickAsync();
           
        }

        [Test, TestCaseSource(nameof(LoginJsonData2)), Order(7)]
        public async Task Booking2(string PFLogin2, string PFPassword, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);
            var homePage = new NewHomewebObjects(Page);

            // Login process
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(PFLogin2);
            await Page.Keyboard.PressAsync("Enter");
            await homePage.Password.FillAsync(PFPassword);
            await homePage.Submit.ClickAsync();
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "URL indicates the user is still on the login screen.");
                Assert.That(await homePage.SmartCare.IsVisibleAsync(), Is.True, "SmartCare button is not visible after login.");
            });
            await homePage.SmartCare.ClickAsync();

            await homePage.Problemissue.ClickAsync();
            await homePage.Problemissue1.ClickAsync();
            await homePage.PFAssessment.ClickAsync();
            await homePage.PFAssessment1.ClickAsync();
            await homePage.PFAssessment2.ClickAsync();
            await homePage.PFAssessment3.ClickAsync();
            await homePage.PFAssessment4.ClickAsync();

            await homePage.PFAssessmentbutton.ClickAsync();
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.Yourself.IsVisibleAsync(), Is.True, "'Yourself' selection option is not visible after the assessment.");
            });
            await homePage.Yourself.ClickAsync();

            //await homePage.NextPF.ScrollIntoViewIfNeededAsync();
            await homePage.NextPF.ClickAsync();
            await homePage.SelectTime.ClickAsync();

            // Select Appointment Modality
            await Page.Locator("//SELECT[@id='appointmentModality']").SelectOptionAsync(new SelectOptionValue { Label = "Phone" });

            await homePage.SelectMode.ClickAsync();
            await homePage.SelectYes.ClickAsync();
            await homePage.SelectYes1.ClickAsync();
            await homePage.SelectText.ClickAsync();
            await homePage.SelectCheck.ClickAsync();
            await homePage.SelectNext.ClickAsync();
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.SelectDashboard.IsVisibleAsync(), Is.True, "Return to Dashboard button is not visible; booking confirmation may have failed.");
            });
            await homePage.SelectDashboard.ClickAsync();

            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
            await homePage.LogoutConfirm.ClickAsync();
       
        }

        [Test, TestCaseSource(nameof(LoginJsonData2)), Order(8)]
        public async Task Cancel2(string PFLogin2, string PFPassword, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);
            var homePage = new NewHomewebObjects(Page);

            // Login process
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(PFLogin2);
            await Page.Keyboard.PressAsync("Enter");
            await homePage.Password.FillAsync(PFPassword);
            await homePage.Submit.ClickAsync();
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "URL indicates the user is still on the login screen.");
                Assert.That(await homePage.Journey.IsVisibleAsync(), Is.True, "Journey button is not visible after login.");
            });

            // Cancel Flow
            await homePage.Journey.ClickAsync();
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.ViewDetails.IsVisibleAsync(), Is.True, "ViewDetails button is not visible on the Journey page.");
            });
            await homePage.ViewDetails.ClickAsync();
            await homePage.Cancel.ClickAsync();
            await homePage.CancelYes.ClickAsync();
            Assert.Multiple(async () =>
            {
                Assert.That(await homePage.CancelYes.IsVisibleAsync(), Is.False, "Cancel confirmation modal did not close after clicking Yes.");
            });


            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
            await homePage.LogoutConfirm.ClickAsync();
           
        }

        [Test, TestCaseSource(nameof(LoginJsonData2)), Order(9)]
        public async Task EndServices2(string PFLogin2, string PFPassword, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(Url);
            var homePage = new NewHomewebObjects(Page);

            // Login process
            await homePage.Login.ClickAsync();
            await homePage.UserName.FillAsync(PFLogin2);
            await Page.Keyboard.PressAsync("Enter");
            await homePage.Password.FillAsync(PFPassword);
            await homePage.Submit.ClickAsync();
            Assert.Multiple(async () =>
            {
                Assert.That(Page.Url, Does.Not.Contain("login"), "URL indicates the user is still on the login screen.");
                Assert.That(await homePage.Journey.IsVisibleAsync(), Is.True, "Journey button is not visible after login.");
            });

            // End Services Flow
            await homePage.Journey.ClickAsync();
            Assert.Multiple(async () =>
            {
                // Adjust this assertion if EndServices isn't always expected to be visible (since you use TryClickAsync)
                Assert.That(await homePage.Profile.IsVisibleAsync(), Is.True, "Profile button is missing; page may not have loaded correctly.");
            });
            await TryClickAsync(homePage.EndServices);


            await TryClickAsync(homePage.YesDone);
            await TryClickAsync(homePage.Reason);


            // Profile and Logout
            await homePage.Profile.ClickAsync();
            await homePage.Logout.ClickAsync();
            await homePage.LogoutConfirm.ClickAsync();
          
        }

        // --- Helper Methods ---

        /// <summary>
        /// Attempts to click an element with a short timeout. 
        /// Equivalent to the old TryClick that caught NoSuchElementException.
        /// </summary>
        private async Task TryClickAsync(ILocator locator)
        {
            try
            {
                // Wait up to 3 seconds for the element to appear before giving up.
                await locator.ClickAsync(new LocatorClickOptions { Timeout = 3000 });
            }
            catch (TimeoutException)
            {
                // Element not found or not clickable in time, do nothing and proceed
            }
        }

        private async Task TakeScreenshotAsync(IPage targetPage, string filePath)
        {
            string fullPath = filePath + time.ToString("yy_dd_h_mm_ss") + ".png";
            await targetPage.ScreenshotAsync(new PageScreenshotOptions { Path = fullPath });
        }

        public static void SafeAssert(Action assertion)
        {
            try
            {
                assertion();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // Log the error message but continue execution
            }
        }

        // --- Data Providers ---

        public static IEnumerable<TestCaseData> LoginJsonData()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\Playwright_Homeweb_3.0\Testdata\NewDataHomeweb.Json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.PFLogin)
                                                        && !string.IsNullOrEmpty(data.PFPassword)
                                                        && data.Url == "https://homeweb.ca/en");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.PFLogin, loginData.PFPassword, loginData.Url);
            }
        }

        public static IEnumerable<TestCaseData> LoginJsonData1()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\Playwright_Homeweb_3.0\Testdata\NewDataHomeweb.Json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.PFLogin1)
                                                        && !string.IsNullOrEmpty(data.PFPassword)
                                                        && data.Url == "https://homeweb.ca/en");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.PFLogin1, loginData.PFPassword, loginData.Url);
            }
        }

        public static IEnumerable<TestCaseData> LoginJsonData2()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\Playwright_Homeweb_3.0\Testdata\NewDataHomeweb.Json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.PFLogin2)
                                                        && !string.IsNullOrEmpty(data.PFPassword)
                                                        && data.Url == "https://homeweb.ca/en");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.PFLogin2, loginData.PFPassword, loginData.Url);
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
                    test.Log(Status.Fail, $"Test failed with logtrace: {stackTrace}");
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