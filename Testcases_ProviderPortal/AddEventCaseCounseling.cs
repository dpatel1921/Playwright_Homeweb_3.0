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
    public class AddEventCounseling : PageTest
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
            Page.SetDefaultTimeout(30000); // 30-second global timeout for the form
        }

        public class TestCaseJsonData
        {
            public string ProviderUsername { get; set; } = string.Empty;
            public string ProviderPassword { get; set; } = string.Empty;
            public string Url { get; set; } = string.Empty;
            public string SecurityAnswer { get; set; } = string.Empty;
            public string CaseID1 { get; set; } = string.Empty;
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
                    loginData.SecurityAnswer, loginData.CaseID1, loginData.SessionDate,
                    loginData.SessionTime, loginData.Textarea1);
            }
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task Login(string ProviderUsername, string ProviderPassword, string Url, string SecurityAnswer, string CaseID1,
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

                // Sequential typing for frontend validation
                await providerportal.ProviderPassword.PressSequentiallyAsync(ProviderPassword, new LocatorPressSequentiallyOptions { Delay = 50 });
                await providerportal.Login.ClickAsync();

                // --- SECURITY QUESTION ---
                await providerportal.RadioSelect.ClickAsync();
                await Page.Locator("select#securityQuestion").SelectOptionAsync(new SelectOptionValue { Index = 1 });

                // Sequential typing for frontend validation
                // Using the SecurityAnswer variable instead of the hardcoded "test1234"
                await providerportal.SecurityAnswer.PressSequentiallyAsync(SecurityAnswer, new LocatorPressSequentiallyOptions { Delay = 50 });
                await providerportal.SecurityLogin.ClickAsync();

                // Verify dashboard loaded
                await Expect(providerportal.ServiceEvent).ToBeVisibleAsync();

                // --- CASE SEARCH ---
                await providerportal.ServiceEvent.ClickAsync();
                await providerportal.Servicesearch.FillAsync(CaseID1);
                await providerportal.Searchclick.ClickAsync();

                await Expect(providerportal.OpenCase).ToBeVisibleAsync();

                // --- SWITCH TO CASE TAB ---
                var newTabPage = await Context.RunAndWaitForPageAsync(async () =>
                {
                    await providerportal.OpenCase.ClickAsync();
                });

                var portalNewTab = new ProviderPortalDashboard(newTabPage);

                await Expect(portalNewTab.AddForm).ToBeVisibleAsync();
                await portalNewTab.AddForm.ClickAsync();

                // --- ADD COUNSELLING EVENT ---
                await portalNewTab.Csessionform.ClickAsync();
                await portalNewTab.AddCounselling.ClickAsync();

                await Expect(portalNewTab.SendDate).ToBeVisibleAsync();

                // --- SESSION DETAILS ---
                await portalNewTab.SendDate.FillAsync(SessionDate);
                await portalNewTab.SendTimeCouns.ClickAsync();
                await portalNewTab.SelectTime.ClickAsync();

                // --- ATTENDANCE + DELIVERY ---
                await portalNewTab.Attendance.ClickAsync();
                await portalNewTab.CAttendanceStatus.ClickAsync();

                await portalNewTab.DeliveryMethod.ClickAsync();
                await portalNewTab.CDeliveryMethodType.ClickAsync();

                // --- RADIO + TEXT FIELDS ---
                await portalNewTab.CRadio1.ClickAsync();
                await portalNewTab.CRadio2.ClickAsync();
                await portalNewTab.CRadio3.ClickAsync();

                await portalNewTab.Cselect.ClickAsync();
                await portalNewTab.Cselect1.ClickAsync();

                await Expect(portalNewTab.Textarea17).ToBeVisibleAsync();
                await portalNewTab.Textarea17.FillAsync(Textarea1);

                await portalNewTab.Textarea18.FillAsync(Textarea1);
                await portalNewTab.Textarea19.FillAsync(Textarea1);

                await portalNewTab.CCheckbox.ClickAsync();

                await portalNewTab.Textarea20.FillAsync(Textarea1);
                await portalNewTab.Textarea21.FillAsync(Textarea1);
                await portalNewTab.Textarea22.FillAsync(Textarea1);
                await portalNewTab.Textarea23.FillAsync(Textarea1);
                await portalNewTab.Textarea24.FillAsync(Textarea1);
                await portalNewTab.Textarea25.FillAsync(Textarea1);
                await portalNewTab.Textarea26.FillAsync(Textarea1);

                await portalNewTab.CCheckbox1.ClickAsync();

                // --- RISK + COMMUNITY ---
                await portalNewTab.Criskidentified.ClickAsync();
                await portalNewTab.Cselect2.ClickAsync();
                await portalNewTab.CRisk.ClickAsync();
                await portalNewTab.Cselect3.ClickAsync();

                await portalNewTab.Textarea27.FillAsync(Textarea1);
                await portalNewTab.Textarea28.FillAsync(Textarea1);
                await portalNewTab.Textarea29.FillAsync(Textarea1);

                // Replaced Selenium Actions with Playwright Keyboard commands
                // Simulates: action.SendKeys(Keys.Tab + Keys.Tab + Keys.Tab + Keys.ArrowDown).Perform();
                await newTabPage.Keyboard.PressAsync("Tab");
                await newTabPage.Keyboard.PressAsync("Tab");
                await newTabPage.Keyboard.PressAsync("Tab");
                await newTabPage.Keyboard.PressAsync("ArrowDown");

                // Added a brief explicit wait to allow UI to react to keyboard events if needed
                await newTabPage.WaitForTimeoutAsync(1000);

                // Simulates: action.SendKeys(Keys.Tab + Keys.ArrowDown).Perform();
                await newTabPage.Keyboard.PressAsync("Tab");
                await newTabPage.Keyboard.PressAsync("ArrowDown");
                await newTabPage.WaitForTimeoutAsync(1000);

                await portalNewTab.CCommunity.ClickAsync();
                await portalNewTab.CHHIResources.ClickAsync();

                // --- SAVE FORM ---
                await Expect(portalNewTab.CSave).ToBeVisibleAsync();
                await portalNewTab.CSave.ClickAsync();

                test.Log(Status.Pass, "Login test (AddEventCounseling) completed successfully.");
            }
            catch (Exception ex)
            {
                test?.Log(Status.Fail, $"Exception during AddEventCounseling test: {ex.Message}");

                // Full-page screenshot on the active tab
                var screenshotPath = $"AddEventCouns_Failure_{time:yy_dd_h_mm_ss}.png";
                var pageToScreenshot = Page.Context.Pages.Count > 1 ? Page.Context.Pages[1] : Page;
                await pageToScreenshot.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });
                test?.AddScreenCaptureFromPath(screenshotPath);

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