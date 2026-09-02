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
using WarriorHealthBeta.Objects;

namespace WarriorHealthBeta.TestCases
{
    [TestFixture]
    public class WarriorHealthSmokeTests : PageTest
    {
        private ExtentReports extent;
        private ExtentTest test;
        private DateTime time = DateTime.Now;

        [OneTimeSetUp]
        public void SetUp()
        {
            extent = ExtentManager.GetReporter();
        }

        [SetUp]
        public void Initialize()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            // PageTest automatically manages browser initialization and context.
        }

        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions()
            {
                ViewportSize = ViewportSize.NoViewport
            };
        }

        public class TestCaseJsonData
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public string Url { get; set; }
            public string Invalid { get; set; }
            public string Valid { get; set; }
            public string Search1 { get; set; }
            public string Search2 { get; set; }
            public string OrgName { get; set; }
            public string RepName { get; set; }
            public string RepTitle { get; set; }
            public string RepEmail { get; set; }
            public string RepPhone { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
            public string RepLastName { get; set; }
            public string AddressLine { get; set; }
            public string PostalCode { get; set; }
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task SmokeHeader(string Login, string Password, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await ScrollAndClickAsync(home.WHlogo);
            await Page.WaitForTimeoutAsync(5000); // Retained specific hard wait from original
            await ScrollAndClickAsync(home.ChatBox);

            await ScrollAndClickAsync(home.Home);
            await ScrollAndClickAsync(home.Browse);
            await ScrollAndClickAsync(home.Assessments);
            await ScrollAndClickAsync(home.Search);
            await ScrollAndClickAsync(home.FAQ);
            await ScrollAndClickAsync(home.AboutUs);
            await ScrollAndClickAsync(home.PSO);
            await ScrollAndClickAsync(home.Toggle);
            await ScrollAndClickAsync(home.ToggleEng);
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task SmokeFooter(string Login, string Password, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await ScrollAndClickAsync(home.Footer1);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Footer2);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Footer3);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Footer4);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Footer5);
            await ScrollAndClickAsync(home.Home1);
            await ClickAndSwitchAsync(home.Footer6);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Footer7);
            await ScrollAndClickAsync(home.Home1);
            await ClickAndSwitchAsync(home.Footer9);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Footer10);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Footer11);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Footer12);
            await ScrollAndClickAsync(home.Home1);

            await TakeScreenshotAsync("C:\\TestData\\WarriorHealth\\Home_");

            string content = await Page.ContentAsync();
            Assert.That(content.Contains("error") || content.Contains("required"));
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task SmokeHome(string Login, string Password, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await ScrollAndClickAsync(home.ForIndividuals);
            await ScrollAndClickAsync(home.ForFamilies);
            await ScrollAndClickAsync(home.ForOrgs);
            await Page.GoBackAsync();

            await ScrollAndClickAsync(home.PeerSupport);
            await Page.GoBackAsync();
            await ScrollAndClickAsync(home.CrisisSupport);
            await Page.GoBackAsync();

            await ScrollAndClickAsync(home.Poweredby1);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Poweredby2);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Poweredby3);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Poweredby4);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Poweredby5);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Poweredby6);
            await ScrollAndClickAsync(home.Home1);

            await TakeScreenshotAsync("C:\\TestData\\WarriorHealth\\Home_");
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task SmokePSP(string Login, string Password, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await ScrollAndClickAsync(home.Home);
            await ScrollAndClickAsync(home.Coreservice);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.GetRecommendation);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPservice1);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPservice2);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPservice3);
            await ScrollAndClickAsync(home.Home1);

            await TakeScreenshotAsync("C:\\TestData\\WarriorHealth\\Home_");

            string content = await Page.ContentAsync();
            Assert.That(content.Contains("error") || content.Contains("required"));
        }

        [Test, TestCaseSource(nameof(LoginJsonData1))]
        public async Task SmokePSPresources(string Login, string Password, string Url, string Invalid, string Valid)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await ScrollAndClickAsync(home.Home);
            await ScrollAndClickAsync(home.PSPresource1);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPresource2);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPresource3);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPresource4);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPresource5);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPresource6);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPresource7);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPresource8);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPresource9);
            await ScrollAndClickAsync(home.Home1);

            await ScrollAndClickAsync(home.Searchitem);
            await home.Searchitem.FillAsync(Invalid);
            await home.Searchitem.PressAsync("Enter");

            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Searchitem);
            await home.Searchitem.ClearAsync();
            await home.Searchitem.FillAsync(Valid);
            await home.Searchitem.PressAsync("Enter");

            await TakeScreenshotAsync("C:\\TestData\\WarriorHealth\\Home_");
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task SmokePSPresourcesforfamilies(string Login, string Password, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await ScrollAndClickAsync(home.PSPFamily1);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPFamily3);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPFamily4);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPFamily5);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPFamily6);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPFamily7);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPFamily8);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPFamily9);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPFamily10);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.PSPFamily11);
            await ScrollAndClickAsync(home.Home1);

            await TakeScreenshotAsync("C:\\TestData\\WarriorHealth\\Home_");

            string content = await Page.ContentAsync();
            Assert.That(content.Contains("error") || content.Contains("required"));
        }

        [Test, TestCaseSource(nameof(LoginJsonData1))]
        public async Task SmokeBrowse(string Login, string Password, string Url, string Invalid, string Valid)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await ScrollAndClickAsync(home.Browse);
            await ScrollAndClickAsync(home.Browse2);
            await ScrollAndClickAsync(home.Browse);
            await ScrollAndClickAsync(home.Browse3);
            await ScrollAndClickAsync(home.Browse);
            await ScrollAndClickAsync(home.Browse4);
            await ScrollAndClickAsync(home.Browse);
            await ScrollAndClickAsync(home.Browse5);
            await ScrollAndClickAsync(home.Browse);
            await ScrollAndClickAsync(home.Browse7);
            await ScrollAndClickAsync(home.Browse);
            await ScrollAndClickAsync(home.Browse8);
            await ScrollAndClickAsync(home.Browse);
            await ClickAndSwitchAsync(home.Browse9);

            await ScrollAndClickAsync(home.Searchitem);
            await home.Searchitem.FillAsync(Invalid);
            await home.Searchitem.PressAsync("Enter");

            await ScrollAndClickAsync(home.Browse);
            await ScrollAndClickAsync(home.Searchitem);
            await home.Searchitem.ClearAsync();
            await home.Searchitem.FillAsync(Valid);
            await home.Searchitem.PressAsync("Enter");

            await TakeScreenshotAsync("C:\\TestData\\WarriorHealth\\Home_");
        }

        [Test, TestCaseSource(nameof(LoginJsonData))]
        public async Task SmokeSearch(string Login, string Password, string Url, string Invalid, string Valid,
            string Search1, string Search2)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await ScrollAndClickAsync(home.Search);
            await ScrollAndClickAsync(home.Searchitem);
            await home.Searchitem.FillAsync(Invalid);
            await home.Searchitem.PressAsync("Enter");

            await ScrollAndClickAsync(home.Searchitem);
            await home.Searchitem.ClearAsync();
            await home.Searchitem.FillAsync(Valid);
            await home.Searchitem.PressAsync("Enter");

            await ScrollAndClickAsync(home.Search3);

            await TakeScreenshotAsync("C:\\TestData\\WarriorHealth\\Home_");
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task SmokeAboutUs(string Login, string Password, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await ScrollAndClickAsync(home.AboutUs);
            await ScrollAndClickAsync(home.WHlogo);
            await ScrollAndClickAsync(home.AboutUs);
            await ScrollAndClickAsync(home.Aboutus1);
            await ScrollAndClickAsync(home.AboutUs);
            await ScrollAndClickAsync(home.Aboutus2);
            await ScrollAndClickAsync(home.AboutUs);
            await ScrollAndClickAsync(home.Aboutus3);
            await ScrollAndClickAsync(home.AboutUs);
            await ScrollAndClickAsync(home.Aboutus4);
            await ScrollAndClickAsync(home.AboutUs);
            await ScrollAndClickAsync(home.Aboutus5);
            await ScrollAndClickAsync(home.AboutUs);
            await ScrollAndClickAsync(home.Aboutus6);

            await TakeScreenshotAsync("C:\\TestData\\WarriorHealth\\Home_");

            string content = await Page.ContentAsync();
            Assert.That(content.Contains("About Us"));
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task SmokeEnroll(string Login, string Password, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await home.ForOrgs.ScrollIntoViewIfNeededAsync();
            await home.ForOrgs.ClickAsync();

            await home.Enroll1.ScrollIntoViewIfNeededAsync();
            await home.Enroll1.ClickAsync();
            await Page.GoBackAsync();

            await home.Enroll2.ScrollIntoViewIfNeededAsync();
            await ClickAndSwitchAsync(home.Enroll2);

            await home.Enroll3.ScrollIntoViewIfNeededAsync();
            await home.Enroll3.ClickAsync();
            await Page.GoBackAsync();

            await home.Enroll4.ScrollIntoViewIfNeededAsync();
            await ClickAndSwitchAsync(home.Enroll4);

            await home.Enroll5.ScrollIntoViewIfNeededAsync();
            await home.Enroll5.ClickAsync();
            await Page.GoBackAsync();

            await home.Enroll6.ScrollIntoViewIfNeededAsync();
            await ClickAndSwitchAsync(home.Enroll6);

            await ScrollAndClickAsync(home.Aboutus1);
            await Page.GoBackAsync();
            await ScrollAndClickAsync(home.Aboutus2);
            await Page.GoBackAsync();
            await ScrollAndClickAsync(home.Aboutus3);
            await Page.GoBackAsync();
            await ScrollAndClickAsync(home.Aboutus4);
            await Page.GoBackAsync();
            await ScrollAndClickAsync(home.Aboutus5);
            await Page.GoBackAsync();
            await ScrollAndClickAsync(home.Aboutus6);

            await TakeScreenshotAsync("C:\\TestData\\WarriorHealth\\Home_");
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task SmokeAssessment(string Login, string Password, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await ScrollAndClickAsync(home.Assessments);
            await home.Assessment1.ClickAsync();
            await home.NewAssessment.ClickAsync();
            await home.Assessment2.ClickAsync();
            await home.Assessment3.ClickAsync();
            await home.Assessment2.ClickAsync();
            await home.Assessment3.ClickAsync();
            await home.Assessment2.ClickAsync();
            await home.Assessment3.ClickAsync();

            // Drag a slider horizontally by 50px using bounding box
            var sliderBox = await home.Select.BoundingBoxAsync();
            if (sliderBox != null)
            {
                await Page.Mouse.MoveAsync(sliderBox.X + sliderBox.Width / 2, sliderBox.Y + sliderBox.Height / 2);
                await Page.Mouse.DownAsync();
                await Page.Mouse.MoveAsync(sliderBox.X + sliderBox.Width / 2 + 50, sliderBox.Y + sliderBox.Height / 2);
                await Page.Mouse.UpAsync();
            }

            await ScrollAndClickAsync(home.Next);
            await ScrollAndClickAsync(home.Assessment4);
            await ScrollAndClickAsync(home.Assessment5);
            await ScrollAndClickAsync(home.Assessment6);
            await ScrollAndClickAsync(home.Assessment7);
            await ScrollAndClickAsync(home.Assessment8);
            await ScrollAndClickAsync(home.Assessment10);
            await ScrollAndClickAsync(home.Assessment11);
            await ScrollAndClickAsync(home.Assessment12);
            await ScrollAndClickAsync(home.Assessment13);
            await ScrollAndClickAsync(home.Assessment14);
            await ScrollAndClickAsync(home.Assessment15);
            await ScrollAndClickAsync(home.Assessment16);
            await ScrollAndClickAsync(home.Assessment17);
            await ScrollAndClickAsync(home.Assessment18);
            await ScrollAndClickAsync(home.Assessment19);
            await ScrollAndClickAsync(home.Assessment20);
            await ScrollAndClickAsync(home.Assessment21);
            await ScrollAndClickAsync(home.Assessment22);
            await ScrollAndClickAsync(home.Assessment23);
            await ScrollAndClickAsync(home.Assessment24);
            await ScrollAndClickAsync(home.Assessment25);
            await ScrollAndClickAsync(home.Assessment26);
            await ScrollAndClickAsync(home.Assessment27);
            await ScrollAndClickAsync(home.Assessment28);
            await ScrollAndClickAsync(home.Home1);

            await TakeScreenshotAsync("C:\\TestData\\WarriorHealth\\Home_");
        }

        [Test, TestCaseSource(nameof(LoginJsonData3))]
        public async Task SmokeEnrollorg(string Login, string Password, string Url, string OrgName, string RepName,
            string RepTitle, string RepEmail, string RepPhone, string City, string Province, string RepLastName,
            string AddressLine, string PostalCode)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await ScrollAndClickAsync(home.ForOrgs);
            await ScrollAndClickAsync(home.Enroll1);

            await home.OrgEnroll1.FillAsync(OrgName);
            await home.OrgEnroll2.FillAsync(RepName);
            await home.OrgEnroll8.FillAsync(RepLastName);
            await home.OrgEnroll3.FillAsync(RepTitle);
            await home.OrgEnroll4.FillAsync(GenerateUniqueEmail());
            await home.OrgEnroll5.FillAsync(RepPhone);

            await Page.Locator("//select[@id='locale']").SelectOptionAsync(new SelectOptionValue { Label = "English" });

            await home.OrgEnroll9.FillAsync(AddressLine);
            await home.OrgEnroll6.FillAsync(City);

            await Page.Locator("//select[@id='province']").SelectOptionAsync(new SelectOptionValue { Label = Province });

            await home.OrgEnroll10.FillAsync(PostalCode);
            await ScrollAndClickAsync(home.OrgEnroll11);
            await ScrollAndClickAsync(home.OrgEnroll7);

            await TakeScreenshotAsync("C:\\TestData\\WarriorHealth\\Home_");
        }

        [Test, TestCaseSource(nameof(LoginJsonData4))]
        public async Task SmokeEmailUpdates(string Login, string Password, string Url, string OrgName, string RepName,
            string RepTitle, string RepEmail, string RepPhone)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await ScrollAndClickAsync(home.Footer7);
            await home.OrgEmailupdate.FillAsync(RepName);
            await home.OrgEnroll4.FillAsync(GenerateUniqueEmail());
            await home.OrgEnroll3.FillAsync(RepTitle);
            await home.OrgEnroll1.FillAsync(OrgName);
            await home.OrgEnroll5.FillAsync(RepPhone);
            await ScrollAndClickAsync(home.EmailSign1);

            await TakeScreenshotAsync("C:\\TestData\\WarriorHealth\\Home_");
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task SmokeFAQ(string Login, string Password, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await ScrollAndClickAsync(home.FAQ);
            await ScrollAndClickAsync(home.FAQ1);
            await ScrollAndClickAsync(home.FAQ1);

            await TakeScreenshotAsync("C:\\TestData\\WarriorHealth\\Home_");
        }

        [Test, TestCaseSource(nameof(LoginJsonData2))]
        public async Task SmokeFounder(string Login, string Password, string Url)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await NavigateWithCredentialsAsync(Login, Password);
            var home = new WarriorHealthObjects(Page);

            await ScrollAndClickAsync(home.Poweredby1);
            await ClickAndSwitchAsync(home.Founder1);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Poweredby2);
            await ClickAndSwitchAsync(home.Founder2);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Poweredby3);
            await ClickAndSwitchAsync(home.Founder3);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Poweredby4);
            await ClickAndSwitchAsync(home.Founder4);
            await ScrollAndClickAsync(home.Home1);
            await ScrollAndClickAsync(home.Poweredby5);
            await ClickAndSwitchAsync(home.Founder5);
            await ScrollAndClickAsync(home.Home1);
            await ClickAndSwitchAsync(home.Poweredby6);

            await TakeScreenshotAsync("C:\\TestData\\WarriorHealth\\Home_");
        }

        // --- Extracted Helper Methods ---

        private async Task ScrollAndClickAsync(ILocator locator)
        {
            await locator.ScrollIntoViewIfNeededAsync();
            await locator.ClickAsync();
        }

        private async Task ClickAndSwitchAsync(ILocator locator)
        {
            var popup = await Page.RunAndWaitForPopupAsync(async () =>
            {
                await ScrollAndClickAsync(locator);
            });
            await popup.WaitForLoadStateAsync(LoadState.Load);
            await popup.CloseAsync();
        }

        private async Task NavigateWithCredentialsAsync(string user, string pass)
        {
            await Page.GotoAsync($"https://{user}:{pass}@warriorhealth.ca/en");
        }

        private async Task TakeScreenshotAsync(string filePath)
        {
            string fullPath = filePath + time.ToString("yy_dd_h_mm_ss") + ".png";
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = fullPath });
        }

        private static string GenerateUniqueEmail()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return $"testuser_{timestamp}@demo.com";
        }

        // --- Data Providers ---

        public static IEnumerable<TestCaseData> LoginJsonData()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\QA-Homeweb-Automation-3.0\TestData\TestData.Json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.Login)
                                                        && !string.IsNullOrEmpty(data.Password)
                                                        && data.Url == "https://warriorhealth.ca/en");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.Login, loginData.Password, loginData.Url, loginData.Invalid,
                    loginData.Valid, loginData.Search1, loginData.Search2);
            }
        }

        public static IEnumerable<TestCaseData> LoginJsonData1()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\QA-Homeweb-Automation-3.0\TestData\TestData.Json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.Login)
                                                        && !string.IsNullOrEmpty(data.Password)
                                                        && data.Url == "https://warriorhealth.ca/en");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.Login, loginData.Password, loginData.Url, loginData.Invalid,
                    loginData.Valid);
            }
        }

        public static IEnumerable<TestCaseData> LoginJsonData2()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\QA-Homeweb-Automation-3.0\TestData\TestData.Json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.Login)
                                                        && !string.IsNullOrEmpty(data.Password)
                                                        && data.Url == "https://warriorhealth.ca/en");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.Login, loginData.Password, loginData.Url);
            }
        }

        public static IEnumerable<TestCaseData> LoginJsonData3()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\QA-Homeweb-Automation-3.0\TestData\TestData.Json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.Login)
                                                        && !string.IsNullOrEmpty(data.Password)
                                                        && data.Url == "https://warriorhealth.ca/en");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.Login, loginData.Password, loginData.Url, loginData.OrgName,
                    loginData.RepName, loginData.RepTitle, loginData.RepEmail, loginData.RepPhone, loginData.City, loginData.Province,
                    loginData.RepLastName, loginData.AddressLine, loginData.PostalCode);
            }
        }

        public static IEnumerable<TestCaseData> LoginJsonData4()
        {
            string jsonString = File.ReadAllText(@"C:\Users\dpatel\source\repos\QA-Homeweb-Automation-3.0\TestData\TestData.Json");
            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.Login)
                                                        && !string.IsNullOrEmpty(data.Password)
                                                        && data.Url == "https://warriorhealth.ca/en");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.Login, loginData.Password, loginData.Url, loginData.OrgName,
                    loginData.RepName, loginData.RepTitle, loginData.RepEmail, loginData.RepPhone);
            }
        }

        // --- Teardown ---

        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
        }

        [TearDown]
        public void Endtest()
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