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
    public class AddEventCaseClose : PageTest
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
            Page.SetDefaultTimeout(30000); // 30 second global timeout
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
        public async Task CaseClose(string ProviderUsername, string ProviderPassword, string Url, string SecurityAnswer, string CaseID5,
            string SessionDate, string SessionTime, string Textarea1)
        {
            try
            {
                // --- NAVIGATE TO PROVIDER PORTAL ---
                await Page.GotoAsync(Url);
                var providerportal = new ProviderPortalDashboard(Page);

                // Verify login page is loaded
                await Expect(providerportal.LoginButton).ToBeVisibleAsync();

                // --- LOGIN SEQUENCE ---
                await providerportal.LoginButton.ClickAsync();
                await providerportal.ProviderName.FillAsync(ProviderUsername);

                // FIX: Type the password sequentially to trigger UI validation
                await providerportal.ProviderPassword.PressSequentiallyAsync(ProviderPassword, new LocatorPressSequentiallyOptions { Delay = 50 });
                await providerportal.Login.ClickAsync();

                // --- SECURITY QUESTION SEQUENCE ---
                await providerportal.RadioSelect.ClickAsync();
                await Page.Locator("select#securityQuestion").SelectOptionAsync(new SelectOptionValue { Index = 1 });

                // FIX: Type the security answer sequentially to trigger UI validation
                await providerportal.SecurityAnswer.PressSequentiallyAsync(SecurityAnswer, new LocatorPressSequentiallyOptions { Delay = 50 });
                await providerportal.SecurityLogin.ClickAsync();

                // Verify dashboard loaded
                await Expect(providerportal.ServiceEvent).ToBeVisibleAsync();

                // --- SEARCH CASE ---
                await providerportal.ServiceEvent.ClickAsync();
                await providerportal.Servicesearch.FillAsync(CaseID5);
                await providerportal.Searchclick.ClickAsync();

                // Verify case search result visible
                await Expect(providerportal.OpenCase).ToBeVisibleAsync();

                // --- SWITCH TO CASE TAB ---
                var newTabPage = await Context.RunAndWaitForPageAsync(async () =>
                {
                    await providerportal.OpenCase.ClickAsync();
                });

                var portalNewTab = new ProviderPortalDashboard(newTabPage);

                // Verify Add Form button exists
                await Expect(portalNewTab.AddForm).ToBeVisibleAsync();
                await portalNewTab.AddForm.ClickAsync();

                // --- CASE CLOSE FORM ---
                await portalNewTab.CaseClose.ClickAsync();
                await portalNewTab.AddEMHC.ClickAsync();

                // Verify Case Close form elements
                await Expect(portalNewTab.SendDate).ToBeVisibleAsync();

                await portalNewTab.SendDate.FillAsync(SessionDate);
                await portalNewTab.SendTimeCouns.ClickAsync();
                await portalNewTab.SelectTime.ClickAsync();

                // Checkboxes / Radio Buttons
                await portalNewTab.CaseClose1.ClickAsync();
                await portalNewTab.CaseClose2.ClickAsync();

                // Note: I preserved the logic of skipping CaseClose 3, 4, 8, and 9 based on your original commented-out code
                await portalNewTab.CaseClose5.ClickAsync();
                await portalNewTab.CaseClose6.ClickAsync();
                await portalNewTab.CaseClose7.ClickAsync();

                // --- SAVE FORM ---
                await Expect(portalNewTab.Save).ToBeVisibleAsync();
                await portalNewTab.Save.ClickAsync();

                test.Log(Status.Pass, "CaseClose test completed successfully.");
            }
            catch (Exception ex)
            {
                test?.Log(Status.Fail, $"Exception during CaseClose test: {ex.Message}");

                // Native Playwright full-page screenshot on the active tab
                var screenshotPath = $"CaseClose_Failure_{time:yy_dd_h_mm_ss}.png";
                var pageToScreenshot = Page.Context.Pages.Count > 1 ? Page.Context.Pages[1] : Page;
                await pageToScreenshot.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });
                test?.AddScreenCaptureFromPath(screenshotPath);

                // Throw exception so NUnit knows the test failed
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