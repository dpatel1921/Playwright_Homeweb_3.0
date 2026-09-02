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
using ProviderPortalSmokeTest.Objects;

namespace ProviderPortalSmokeTest.TestCases
{
    [TestFixture]
    public class AddandDeleteFiles : PageTest
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
            public string CaseID4 { get; set; } = string.Empty;
            public string SessionDate { get; set; } = string.Empty;
            public string SessionTime { get; set; } = string.Empty;
            public string Textarea1 { get; set; } = string.Empty;
            public string CaseID5 { get; set; }
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
                    loginData.SecurityAnswer, loginData.CaseID4, loginData.SessionDate,
                    loginData.SessionTime, loginData.Textarea1,loginData.CaseID5);
            }
        }
        public static IEnumerable<TestCaseData> LoginJsonData1()
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
                    loginData.SecurityAnswer, loginData.CaseID5,loginData.SessionDate,
                    loginData.SessionTime, loginData.Textarea1);
            }
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task AddDoc(string ProviderUsername, string ProviderPassword, string Url, string SecurityAnswer,
                                 string CaseID4, string SessionDate, string SessionTime, string Textarea1)
        {
            try
            {
                // --- NAVIGATE TO PROVIDER PORTAL ---
                await Page.GotoAsync(Url);
                var providerportal = new ProviderPortalDashboard(Page);

                // Replaced SafeAssertIsTrue with Web-First Assertions
                await Expect(providerportal.LoginButton).ToBeVisibleAsync();
                await providerportal.LoginButton.ClickAsync();

                await providerportal.ProviderName.FillAsync(ProviderUsername);
                await providerportal.ProviderPassword.FillAsync(ProviderPassword);
                await providerportal.Login.ClickAsync();

                // --- SECURITY QUESTION ---
                await providerportal.RadioSelect.ClickAsync();

                // Playwright native dropdown selection by Index
                await Page.Locator("select#securityQuestion").SelectOptionAsync(new SelectOptionValue { Index = 1 });

                await providerportal.SecurityAnswer.PressSequentiallyAsync(SecurityAnswer, new LocatorPressSequentiallyOptions { Delay = 50 });
                //await providerportal.SecurityAnswer.FillAsync(SecurityAnswer);
            
                await providerportal.SecurityLogin.ClickAsync();

                await Expect(providerportal.ServiceEvent).ToBeVisibleAsync();

                // --- SEARCH CASE ---
                await providerportal.ServiceEvent.ClickAsync();
                await providerportal.Servicesearch.FillAsync(CaseID4);
                await providerportal.Searchclick.ClickAsync();

                await Expect(providerportal.OpenCase).ToBeVisibleAsync();

                // --- SWITCH TO NEW TAB ---
                // Playwright catches the new page explicitly without index guessing
                var newTabPage = await Context.RunAndWaitForPageAsync(async () =>
                {
                    await providerportal.OpenCase.ClickAsync();
                });

                // Re-initialize POM with the NEW tab's context!
                var portalNewTab = new ProviderPortalDashboard(newTabPage);

                await Expect(portalNewTab.AddDocument).ToBeVisibleAsync();
                await portalNewTab.AddDocument.ClickAsync();

                // --- UPLOAD FILE ---
                string filePath = @"C:\Users\dpatel\source\repos\QA-ProviderPortal-Automation\TestData\SampleAsset.txt";
                Assert.That(File.Exists(filePath), $"Upload file exists at path: {filePath}");

                // Playwright's native file upload method replaces SendKeys
                await portalNewTab.FiletoUpload.SetInputFilesAsync(filePath);
                await portalNewTab.SaveFile.ClickAsync();

                test.Log(Status.Pass, "AddDoc test completed successfully.");
            }
            catch (Exception ex)
            {
                test?.Log(Status.Fail, $"Exception during AddDoc test: {ex.Message}");

                // Native Playwright Screenshot without needing ITakesScreenshot casting
                var screenshotPath = $"AddDoc_Failure_{time:yy_dd_h_mm_ss}.png";
                await Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });
                test?.AddScreenCaptureFromPath(screenshotPath);

                // CRITICAL: Re-throw the exception so NUnit accurately reports the failure!
                throw;
            }
        }

        [Test, TestCaseSource(nameof(LoginJsonData1))]
        public async Task AdminNote(string ProviderUsername, string ProviderPassword, string Url, string SecurityAnswer,
                            string CaseID5, string SessionDate, string SessionTime, string Textarea1)
        {
            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                // --- NAVIGATE TO PROVIDER PORTAL ---
                await Page.GotoAsync(Url);
                var providerportal = new ProviderPortalDashboard(Page);

                // Verify login page loaded
                await Expect(providerportal.LoginButton).ToBeVisibleAsync();

                // --- LOGIN SEQUENCE ---
                await providerportal.LoginButton.ClickAsync();
                await providerportal.ProviderName.FillAsync(ProviderUsername);
                await providerportal.ProviderPassword.FillAsync(ProviderPassword);
                await providerportal.Login.ClickAsync();

                // --- SECURITY QUESTION ---
                await providerportal.RadioSelect.ClickAsync();

                // Playwright handles the select element natively by index
                await Page.Locator("select#securityQuestion").SelectOptionAsync(new SelectOptionValue { Index = 1 });
                await providerportal.SecurityAnswer.PressSequentiallyAsync(SecurityAnswer, new LocatorPressSequentiallyOptions { Delay = 50 });
                //await providerportal.SecurityAnswer.FillAsync(SecurityAnswer);
              
                await providerportal.SecurityLogin.ClickAsync();

                // Verify dashboard loaded
                await Expect(providerportal.ServiceEvent).ToBeVisibleAsync();

                // --- SEARCH FOR CASE ---
                await providerportal.ServiceEvent.ClickAsync();
                await providerportal.Servicesearch.FillAsync(CaseID5);
                await providerportal.Searchclick.ClickAsync();

                // Verify case found
                await Expect(providerportal.OpenCase).ToBeVisibleAsync();

                // --- SWITCH TO NEW CASE TAB ---
                var newTabPage = await Context.RunAndWaitForPageAsync(async () =>
                {
                    await providerportal.OpenCase.ClickAsync();
                });

                // Re-initialize the POM with the NEW tab's context
                var portalNewTab = new ProviderPortalDashboard(newTabPage);

                await Expect(portalNewTab.AddForm).ToBeVisibleAsync();
                await portalNewTab.AddForm.ClickAsync();

                // --- ADMIN NOTE CREATION ---
                await portalNewTab.AdminNote.ClickAsync();
                await portalNewTab.AddEMHC.ClickAsync();

                // --- FILL NOTE DETAILS ---
                // Notice we do NOT need to SwitchTo().ActiveElement() here. 
                // Playwright targets the elements directly regardless of focus.
                await portalNewTab.SendDate.FillAsync(SessionDate);
                await portalNewTab.SendTimeCouns.ClickAsync();
                await portalNewTab.SelectTime.ClickAsync();

                await portalNewTab.Textarea333.FillAsync(Textarea1);

                await portalNewTab.Save.ClickAsync();

                test.Log(Status.Pass, "AdminNote test completed successfully.");
            }
            catch (Exception ex)
            {
                test?.Log(Status.Fail, $"Exception during AdminNote test: {ex.Message}");

                // Capture a full page screenshot. If the new tab failed to open, fallback to main page.
                var screenshotPath = $"AdminNote_Failure_{time:yy_dd_h_mm_ss}.png";
                var pageToScreenshot = Page.Context.Pages.Count > 1 ? Page.Context.Pages[1] : Page;

                await pageToScreenshot.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });
                test?.AddScreenCaptureFromPath(screenshotPath);

                // CRITICAL: Re-throw to ensure NUnit runner registers the failure
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