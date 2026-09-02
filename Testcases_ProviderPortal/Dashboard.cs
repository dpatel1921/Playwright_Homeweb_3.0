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
    public class Dashboard : PageTest
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
                    loginData.ProviderUsername, loginData.ProviderPassword, loginData.Url, loginData.SecurityAnswer);
            }
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task Login(string ProviderUsername, string ProviderPassword, string Url, string SecurityAnswer)
        {
            try
            {
                // --- NAVIGATE TO PROVIDER PORTAL ---
                await Page.GotoAsync(Url);
                var providerportal = new ProviderPortalDashboard(Page);

                await Expect(providerportal.LoginButton).ToBeVisibleAsync();

                // --- LOGIN SEQUENCE ---
                await providerportal.LoginButton.ClickAsync();
                await providerportal.ProviderName.FillAsync(ProviderUsername);

                // Sequential typing to trigger UI validation
                await providerportal.ProviderPassword.PressSequentiallyAsync(ProviderPassword, new LocatorPressSequentiallyOptions { Delay = 50 });
                await providerportal.Login.ClickAsync();

                // --- SECURITY QUESTION ---
                await providerportal.RadioSelect.ClickAsync();
                await Page.Locator("select#securityQuestion").SelectOptionAsync(new SelectOptionValue { Index = 1 });

                // Sequential typing to trigger UI validation (Using variable instead of hardcoded string)
                await providerportal.SecurityAnswer.PressSequentiallyAsync(SecurityAnswer, new LocatorPressSequentiallyOptions { Delay = 50 });
                await providerportal.SecurityLogin.ClickAsync();

                // Verify dashboard loaded properly after login
                await Expect(providerportal.Pendingcases).ToBeVisibleAsync();

                // --- DASHBOARD NAVIGATION (Click & Back) ---
                await providerportal.Pendingcases.ClickAsync();
                await Page.GoBackAsync();

                await providerportal.Casesawaiting.ClickAsync();
                await Page.GoBackAsync();

                await providerportal.Requestcaseext.ClickAsync();
                await Page.GoBackAsync();

                await providerportal.PendingDraft.ClickAsync();
                await Page.GoBackAsync();

                await providerportal.OpenCases.ClickAsync();
                await Page.GoBackAsync();

                await providerportal.Ecounseling.ClickAsync();
                await Page.GoBackAsync();

                // --- ADDITIONAL TABS (Direct Clicks) ---
                // Notice how clean this is without the manual ScrollAndClick methods!
                await providerportal.Schedules.ClickAsync();
                await providerportal.Calendar.ClickAsync();
                await providerportal.ServiceEvent.ClickAsync();

                await providerportal.Contactinfo.ClickAsync();
                await providerportal.Messages.ClickAsync();
                await providerportal.Profile.ClickAsync();
                await providerportal.Invoice.ClickAsync();
                await providerportal.Documents.ClickAsync();
                await providerportal.Support.ClickAsync();
                await providerportal.Archive.ClickAsync();

                // --- LOGOUT ---
                await providerportal.Logout.ClickAsync();

                // Verify logout success by checking if the login button is visible again
                await Expect(providerportal.LoginButton).ToBeVisibleAsync();

                test.Log(Status.Pass, "Dashboard navigation test completed successfully.");
            }
            catch (Exception ex)
            {
                test?.Log(Status.Fail, $"Exception during Dashboard test: {ex.Message}");

                // Capture screenshot on failure
                var screenshotPath = $"Dashboard_Failure_{time:yy_dd_h_mm_ss}.png";
                await Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });
                test?.AddScreenCaptureFromPath(screenshotPath);

                // Re-throw exception to properly fail the NUnit test
                throw;
            }
        }

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