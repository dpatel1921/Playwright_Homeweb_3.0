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
    public class AddEventTravelExpense : PageTest
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
            public string CaseID5 { get; set; } = string.Empty;
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
                    loginData.SecurityAnswer, loginData.CaseID5, loginData.SessionDate,
                    loginData.SessionTime, loginData.Textarea1);
            }
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task TravelExpense(string ProviderUsername, string ProviderPassword, string Url, string SecurityAnswer, string CaseID5,
            string SessionDate, string SessionTime, string Textarea1)
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

                // Simulate human typing to trigger frontend validation
                await providerportal.ProviderPassword.PressSequentiallyAsync(ProviderPassword, new LocatorPressSequentiallyOptions { Delay = 50 });
                await providerportal.Login.ClickAsync();

                // --- SECURITY QUESTION ---
                await providerportal.RadioSelect.ClickAsync();
                await Page.Locator("select#securityQuestion").SelectOptionAsync(new SelectOptionValue { Index = 1 });

                // Simulate human typing to trigger frontend validation
                // Using the SecurityAnswer variable instead of hardcoded string
                await providerportal.SecurityAnswer.PressSequentiallyAsync(SecurityAnswer, new LocatorPressSequentiallyOptions { Delay = 50 });
                await providerportal.SecurityLogin.ClickAsync();

                // Verify dashboard loaded
                await Expect(providerportal.ServiceEvent).ToBeVisibleAsync();

                // --- CASE SEARCH ---
                await providerportal.ServiceEvent.ClickAsync();
                await providerportal.Servicesearch.FillAsync(CaseID5);
                await providerportal.Searchclick.ClickAsync();

                // Verify case results visible
                await Expect(providerportal.OpenCase).ToBeVisibleAsync();

                // --- SWITCH TO CASE TAB ---
                var newTabPage = await Context.RunAndWaitForPageAsync(async () =>
                {
                    await providerportal.OpenCase.ClickAsync();
                });

                var portalNewTab = new ProviderPortalDashboard(newTabPage);

                await Expect(portalNewTab.AddForm).ToBeVisibleAsync();
                await portalNewTab.AddForm.ClickAsync();

                // --- ADD TRAVEL EXPENSE FORM ---
                await portalNewTab.TravelExpense.ClickAsync();

                await Expect(portalNewTab.AddEMHC).ToBeVisibleAsync();
                await portalNewTab.AddEMHC.ClickAsync();

                // Verify form loads before interacting
                await Expect(portalNewTab.SendDate).ToBeVisibleAsync();

                // --- FILL EXPENSE DETAILS ---
                await portalNewTab.SendDate.FillAsync(SessionDate);
                await portalNewTab.SendTimeCouns.ClickAsync();
                await portalNewTab.SelectTime.ClickAsync();

                await Expect(portalNewTab.TravelExpense1).ToBeVisibleAsync();
                await portalNewTab.TravelExpense1.FillAsync(Textarea1);

                await Expect(portalNewTab.TravelExpense2).ToBeVisibleAsync();
                // Preserved the hardcoded "150" from your original script
                await portalNewTab.TravelExpense2.FillAsync("150");

                // --- SAVE FORM ---
                await Expect(portalNewTab.Save).ToBeVisibleAsync();
                await portalNewTab.Save.ClickAsync();

                test.Log(Status.Pass, "TravelExpense test completed successfully.");
            }
            catch (Exception ex)
            {
                test?.Log(Status.Fail, $"Exception during TravelExpense test: {ex.Message}");

                // Full-page screenshot targeting the active tab
                var screenshotPath = $"TravelExpense_Failure_{time:yy_dd_h_mm_ss}.png";
                var pageToScreenshot = Page.Context.Pages.Count > 1 ? Page.Context.Pages[1] : Page;
                await pageToScreenshot.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });
                test?.AddScreenCaptureFromPath(screenshotPath);

                // Re-throw so NUnit correctly fails the test run
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