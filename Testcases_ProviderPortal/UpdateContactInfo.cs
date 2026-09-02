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
    public class UpdateContactInfo : PageTest
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
            public string CaseID4 { get; set; } = string.Empty;
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
                    loginData.SecurityAnswer, loginData.CaseID4, loginData.SessionDate,
                    loginData.SessionTime, loginData.Textarea1);
            }
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task ContactInfo(string ProviderUsername, string ProviderPassword, string Url, string SecurityAnswer,
                                      string CaseID4, string SessionDate, string SessionTime, string Textarea1)
        {
            try
            {
                // --- NAVIGATE TO PORTAL ---
                await Page.GotoAsync(Url);
                var providerportal = new ProviderPortalDashboard(Page);

                // Verify Login page is visible
                await Expect(providerportal.LoginButton).ToBeVisibleAsync();

                // --- LOGIN SEQUENCE ---
                await providerportal.LoginButton.ClickAsync();
                await providerportal.ProviderName.FillAsync(ProviderUsername);

                // Simulate typing to trigger frontend UI validation
                await providerportal.ProviderPassword.PressSequentiallyAsync(ProviderPassword, new LocatorPressSequentiallyOptions { Delay = 50 });
                await providerportal.Login.ClickAsync();

                // --- SECURITY QUESTION ---
                await providerportal.RadioSelect.ClickAsync();
                await Page.Locator("select#securityQuestion").SelectOptionAsync(new SelectOptionValue { Index = 1 });

                // Simulate typing to trigger frontend UI validation
                await providerportal.SecurityAnswer.PressSequentiallyAsync(SecurityAnswer, new LocatorPressSequentiallyOptions { Delay = 50 });
                await providerportal.SecurityLogin.ClickAsync();

                // Verify dashboard appears
                await Expect(providerportal.ServiceEvent).ToBeVisibleAsync();

                // --- CONTACT INFO SECTION ---
                await Expect(providerportal.UpdateContactInfo).ToBeVisibleAsync();
                await providerportal.UpdateContactInfo.ClickAsync();

                await Expect(providerportal.Addnewcontact).ToBeVisibleAsync();
                await providerportal.Addnewcontact.ClickAsync();

                // --- ADD NEW CONTACT ---
                await providerportal.Addnewcontactvalue.FillAsync("4165254525");

                // Verify the phone number was entered successfully
                var enteredPhone = await providerportal.Addnewcontactvalue.InputValueAsync();
                Assert.That(enteredPhone, Is.EqualTo("4165254525"), "Phone number entered successfully.");

                await providerportal.Addnewcontactcheckbox.ClickAsync();

                // --- EDIT CONTACT ---
                await providerportal.Addnewcontactedit.ClickAsync();

                // Note: Playwright's FillAsync automatically clears the field before typing, 
                // so we don't need a separate Clear() command like we did in Selenium!
                await providerportal.Addnewcontactvalue.FillAsync("4165254528");

                await providerportal.Addnewcontactsave.ClickAsync();

                // Let the DOM settle after save before checking the value
                await providerportal.Addnewcontactvalue.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
                var editedPhone = await providerportal.Addnewcontactvalue.InputValueAsync();
                Assert.That(editedPhone, Is.EqualTo("4165254528"), "Contact info edited and saved successfully.");

                // --- DELETE CONTACT ---
                await providerportal.Addnewcontactdelete.ClickAsync();

                test.Log(Status.Pass, "Contact Info test completed successfully.");
            }
            catch (Exception ex)
            {
                test?.Log(Status.Fail, $"Exception during Contact Info test: {ex.Message}");

                // Capture screenshot on failure
                var screenshotPath = $"ContactInfo_Failure_{time:yy_dd_h_mm_ss}.png";
                await Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });
                test?.AddScreenCaptureFromPath(screenshotPath);

                // Re-throw exception so NUnit accurately records the failure
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