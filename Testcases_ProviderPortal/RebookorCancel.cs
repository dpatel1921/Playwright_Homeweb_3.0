using AventStack.ExtentReports;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using ProviderPortalSmokeTest.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProviderPortalSmokeTest.TestCases
{
    [TestFixture]
    public class RebookorCancel : PageTest
    {
        private ExtentReports extent;
        private ExtentTest test;
        private DateTime time = DateTime.Now;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            extent = ExtentManager.GetReporter();
        }

        [SetUp]
        public void Initialize()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            Page.SetDefaultTimeout(30000); // 30-second global timeout
        }

        public class TestCaseJsonData
        {
            public string ProviderUsername { get; set; } = string.Empty;
            public string ProviderPassword { get; set; } = string.Empty;
            public string Url { get; set; } = string.Empty;
            public string SecurityAnswer { get; set; } = string.Empty;
            public string CaseID { get; set; } = string.Empty;
            public string SessionDate { get; set; } = string.Empty;
            public string SessionTime { get; set; } = string.Empty;
            public string Textarea1 { get; set; } = string.Empty;
        }

        public static IEnumerable<TestCaseData> LoginJsonData()
        {
            string path = @"C:\Users\dpatel\source\repos\QA-ProviderPortal-Automation\TestData\DataFile.json";
            string jsonString = File.ReadAllText(path);
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.ProviderUsername)
                                                        && !string.IsNullOrEmpty(data.ProviderPassword)
                                                        && data.Url == "https://provider.homewoodhealth.com/");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(
                    loginData.ProviderUsername, loginData.ProviderPassword, loginData.Url,
                    loginData.SecurityAnswer, loginData.CaseID, loginData.SessionDate,
                    loginData.SessionTime, loginData.Textarea1);
            }
        }

      //  [Test, TestCaseSource(nameof(LoginJsonData)), Order(1)]
        public async Task ResendEmail(string ProviderUsername, string ProviderPassword, string Url, string SecurityAnswer, string CaseID,
            string SessionDate, string SessionTime, string Textarea1)
        {
            try
            {
                await Page.GotoAsync(Url);
                var providerportal = new ProviderPortalDashboard(Page);

                await PerformLoginAsync(providerportal, ProviderUsername, ProviderPassword, SecurityAnswer);
                var portalNewTab = await SearchAndOpenCaseAsync(providerportal, CaseID);

                // --- RESEND EMAIL SPECIFIC ACTIONS ---
                await portalNewTab.Rebooking.ClickAsync();
                await portalNewTab.Rebooking3.ClickAsync();
                await portalNewTab.Aptbooking4.ClickAsync();
                await portalNewTab.Rebooking4.ClickAsync();

                test.Log(Status.Pass, "ResendEmail test completed successfully.");
            }
            catch (Exception ex)
            {
                await HandleFailureAsync(ex, "ResendEmail_Failure_");
            }
        }

       // [Test, TestCaseSource(nameof(LoginJsonData)), Order(2)]
        public async Task RebookAppointment(string ProviderUsername, string ProviderPassword, string Url, string SecurityAnswer, string CaseID,
            string SessionDate, string SessionTime, string Textarea1)
        {
            try
            {
                await Page.GotoAsync(Url);
                var providerportal = new ProviderPortalDashboard(Page);

                await PerformLoginAsync(providerportal, ProviderUsername, ProviderPassword, SecurityAnswer);
                var portalNewTab = await SearchAndOpenCaseAsync(providerportal, CaseID);

                // --- REBOOK APPOINTMENT SPECIFIC ACTIONS ---
                await portalNewTab.Rebooking.ClickAsync();
                await portalNewTab.Rebooking1.ClickAsync();
                await portalNewTab.Rebooking2.ClickAsync();

                test.Log(Status.Pass, "RebookAppointment test completed successfully.");
            }
            catch (Exception ex)
            {
                await HandleFailureAsync(ex, "RebookAppointment_Failure_");
            }
        }

        [Test, TestCaseSource(nameof(LoginJsonData)), Order(3)]
        public async Task FilterBooking(string ProviderUsername, string ProviderPassword, string Url, string SecurityAnswer, string CaseID,
            string SessionDate, string SessionTime, string Textarea1)
        {
            try
            {
                await Page.GotoAsync(Url);
                var providerportal = new ProviderPortalDashboard(Page);

                await PerformLoginAsync(providerportal, ProviderUsername, ProviderPassword, SecurityAnswer);
                var portalNewTab = await SearchAndOpenCaseAsync(providerportal, CaseID);

                // --- FILTER BOOKING SPECIFIC ACTIONS ---
                await portalNewTab.Aptbooking.ClickAsync();
                await portalNewTab.Filter.ClickAsync();
                await portalNewTab.Filter1.ClickAsync();
                await portalNewTab.Filter2.ClickAsync();

                await portalNewTab.Aptbooking1.ClickAsync();
                await portalNewTab.Aptbooking2.ClickAsync();
                await portalNewTab.Aptbooking3.ClickAsync();
                await portalNewTab.Aptbooking4.ClickAsync();
                await portalNewTab.Aptbooking5.ClickAsync();
                await portalNewTab.Aptbooking6.ClickAsync();

                test.Log(Status.Pass, "FilterBooking test completed successfully.");
            }
            catch (Exception ex)
            {
                await HandleFailureAsync(ex, "FilterBooking_Failure_");
            }
        }

       // [Test, TestCaseSource(nameof(LoginJsonData)), Order(4)]
        public async Task HoldBooking(string ProviderUsername, string ProviderPassword, string Url, string SecurityAnswer, string CaseID,
            string SessionDate, string SessionTime, string Textarea1)
        {
            try
            {
                await Page.GotoAsync(Url);
                var providerportal = new ProviderPortalDashboard(Page);

                await PerformLoginAsync(providerportal, ProviderUsername, ProviderPassword, SecurityAnswer);
                var portalNewTab = await SearchAndOpenCaseAsync(providerportal, CaseID);

                // --- HOLD BOOKING SPECIFIC ACTIONS ---
                await portalNewTab.Aptbooking.ClickAsync();

                await portalNewTab.Hold.ClickAsync();
                await portalNewTab.Aptbooking2.ClickAsync();
                await portalNewTab.Aptbooking3.ClickAsync();

                await portalNewTab.Hold1.ClickAsync();
                await portalNewTab.Aptbooking6.ClickAsync();

                await portalNewTab.Hold2.ClickAsync();
                await portalNewTab.Rebooking2.ClickAsync();

                test.Log(Status.Pass, "HoldBooking test completed successfully.");
            }
            catch (Exception ex)
            {
                await HandleFailureAsync(ex, "HoldBooking_Failure_");
            }
        }

        [Test, TestCaseSource(nameof(LoginJsonData)), Order(5)]
        public async Task ProfileView(string ProviderUsername, string ProviderPassword, string Url, string SecurityAnswer, string CaseID,
            string SessionDate, string SessionTime, string Textarea1)
        {
            try
            {
                await Page.GotoAsync(Url);
                var providerportal = new ProviderPortalDashboard(Page);

                await PerformLoginAsync(providerportal, ProviderUsername, ProviderPassword, SecurityAnswer);
                var portalNewTab = await SearchAndOpenCaseAsync(providerportal, CaseID);

                // --- PROFILE VIEW SPECIFIC ACTIONS ---
                await portalNewTab.Profile1.ClickAsync();
                await portalNewTab.Profile2.ClickAsync();
                await portalNewTab.Profile3.ClickAsync();
                await portalNewTab.Profile4.ClickAsync();

                test.Log(Status.Pass, "ProfileView test completed successfully.");
            }
            catch (Exception ex)
            {
                await HandleFailureAsync(ex, "ProfileView_Failure_");
            }
        }

      //  [Test, TestCaseSource(nameof(LoginJsonData)), Order(6)] // Changed from Order 5 to 6 to prevent conflict
        public async Task ApptConfirm(string ProviderUsername, string ProviderPassword, string Url, string SecurityAnswer,
            string CaseID, string SessionDate, string SessionTime, string Textarea1)
        {
            try
            {
                await Page.GotoAsync(Url);
                var providerportal = new ProviderPortalDashboard(Page);

                // Note: This test does NOT open the case tab, it operates directly on the dashboard
                await PerformLoginAsync(providerportal, ProviderUsername, ProviderPassword, SecurityAnswer);

                // --- APPOINTMENT CONFIRMATION SPECIFIC ACTIONS ---
                await Expect(providerportal.AptConfirm).ToBeVisibleAsync();
                await providerportal.AptConfirm.ClickAsync();

                await Expect(providerportal.AptConfirm1).ToBeVisibleAsync();
                await providerportal.AptConfirm1.ClickAsync();

                await Expect(providerportal.AptConfirm2).ToBeVisibleAsync();
                await providerportal.AptConfirm2.ClickAsync();

                test.Log(Status.Pass, "ApptConfirm test completed successfully.");
            }
            catch (Exception ex)
            {
                await HandleFailureAsync(ex, "ApptConfirm_Failure_");
            }
        }

        // ==========================================
        // PRIVATE HELPER METHODS (Refactored Logic)
        // ==========================================

        private async Task PerformLoginAsync(ProviderPortalDashboard providerportal, string username, string password, string securityAnswer)
        {
            await Expect(providerportal.LoginButton).ToBeVisibleAsync();
            await providerportal.LoginButton.ClickAsync();

            await providerportal.ProviderName.FillAsync(username);
            await providerportal.ProviderPassword.PressSequentiallyAsync(password, new LocatorPressSequentiallyOptions { Delay = 50 });
            await providerportal.Login.ClickAsync();

            await providerportal.RadioSelect.ClickAsync();
            await Page.Locator("select#securityQuestion").SelectOptionAsync(new SelectOptionValue { Index = 1 });
            await providerportal.SecurityAnswer.PressSequentiallyAsync(securityAnswer, new LocatorPressSequentiallyOptions { Delay = 50 });
            await providerportal.SecurityLogin.ClickAsync();

            await Expect(providerportal.ServiceEvent).ToBeVisibleAsync();
        }

        private async Task<ProviderPortalDashboard> SearchAndOpenCaseAsync(ProviderPortalDashboard providerportal, string caseId)
        {
            await providerportal.ServiceEvent.ClickAsync();
            await providerportal.Servicesearch.FillAsync(caseId);
            await providerportal.Searchclick.ClickAsync();

            await Expect(providerportal.OpenCase).ToBeVisibleAsync();

            var newTabPage = await Context.RunAndWaitForPageAsync(async () =>
            {
                await providerportal.OpenCase.ClickAsync();
            });

            return new ProviderPortalDashboard(newTabPage);
        }

        private async Task HandleFailureAsync(Exception ex, string filePrefix)
        {
            test?.Log(Status.Fail, $"Exception during test: {ex.Message}");
            var screenshotPath = $"{filePrefix}{time:yy_dd_h_mm_ss}.png";

            // Checks if a new tab is open, and takes a screenshot of the active window
            var pageToScreenshot = Page.Context.Pages.Count > 1 ? Page.Context.Pages[1] : Page;
            await pageToScreenshot.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });

            test?.AddScreenCaptureFromPath(screenshotPath);
            throw ex;
        }

        // ==========================================
        // TEARDOWN METHODS
        // ==========================================

        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
        }

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
    }
}