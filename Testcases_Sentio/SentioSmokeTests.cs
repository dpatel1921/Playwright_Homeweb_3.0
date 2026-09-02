using AventStack.ExtentReports;
using Homeweb_3._0_Tests.Objects;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Homeweb_Tests.TestCases
{
    [TestFixture]
    public class SentioSmokeTests : PageTest
    {
        private ExtentReports extent;
        private ExtentTest test;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Initialize ExtentReports instance
            extent = ExtentManager.GetReporter();
        }

        [SetUp]
        public void Initialize()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            Page.SetDefaultTimeout(100000);
        }

        public class TestCaseJsonData
        {
            public string SentioEmail { get; set; } = string.Empty;
            public string SentioPassword { get; set; } = string.Empty;
            public string SentioUrl { get; set; } = string.Empty;
        }

        public static IEnumerable<TestCaseData> LoginJsonData()
        {
            string path = @"C:\Users\dpatel\source\repos\QA-Homeweb-Automation-3.0\TestData\HomewebLoginData.json";
            string jsonString = File.ReadAllText(path);

            var dataToLoad = JsonSerializer.Deserialize<List<TestCaseJsonData>>(jsonString);
            var filteredData = dataToLoad.Where(data => !string.IsNullOrEmpty(data.SentioEmail)
                                                        && !string.IsNullOrEmpty(data.SentioPassword)
                                                        && data.SentioUrl == "https://beta.sentioapp.com/en");
            foreach (var loginData in filteredData)
            {
                yield return new TestCaseData(loginData.SentioEmail, loginData.SentioPassword, loginData.SentioUrl);
            }
        }

        [Test, TestCaseSource(nameof(LoginJsonData)), Order(1)]
        public async Task SentioLandingPage(string SentioEmail, string SentioPassword, string SentioUrl)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            await Page.GotoAsync(SentioUrl);
            var homePage = new HomewebLoginObjects(Page);

            await homePage.Login.ClickAsync();
            await Page.GoBackAsync();

            await homePage.SentioLearnmore.ClickAsync();
            await homePage.SentioHome.ClickAsync();

            await homePage.SentioGetStarted1.ClickAsync();
            await Page.GoBackAsync();

            await homePage.SentioCreateAccount.ClickAsync();
            await homePage.SentioHome.ClickAsync();

            await homePage.SentioHowTo.ClickAsync();
            await homePage.SentioHome.ClickAsync();

            await homePage.SentioContactHH.ClickAsync();
            await Page.GoBackAsync();

            await homePage.SentioAppstore1.ClickAsync();
            await Page.GoBackAsync();
            await homePage.SentioHome.ClickAsync();
            await homePage.SentioAppstore2.ClickAsync();
            await Page.GoBackAsync();
            await homePage.SentioHome.ClickAsync();

            await homePage.SentioAppstore3.ClickAsync();
            await homePage.SentioHome.ClickAsync();

            await homePage.SentioAppstore4.ClickAsync();
            await homePage.SentioHome.ClickAsync();
        }
        [Test, TestCaseSource(nameof(LoginJsonData)), Order(2)]
        public async Task SentioFooter(string SentioEmail, string SentioPassword, string SentioUrl)
        {
            await Page.SetViewportSizeAsync(1920, 1080);
            // Navigate to the provided URL
            await Page.GotoAsync(SentioUrl);

            // Initialize POM with the Playwright Page context
            var homePage = new HomewebLoginObjects(Page);


            // --- FOOTER LINK VALIDATION ---
            // The explicit ScrollToElement and Thread.Sleep calls are completely removed.
            // Playwright will automatically scroll to the footer for each click.
            await ClickAndNavigateBackAsync(homePage.SentioWelcome);
            await ClickAndNavigateBackAsync(homePage.Aboutsentio);
            await ClickAndNavigateBackAsync(homePage.SentioFAQ);
            await ClickAndNavigateBackAsync(homePage.SentioTerms);
            await ClickAndNavigateBackAsync(homePage.SentioPrivacy);
            await ClickAndNavigateBackAsync(homePage.SentioAccessibility);
        }

        [Test, TestCaseSource(nameof(LoginJsonData)), Order(4)]
        public async Task SentioAnxietyCourse(string SentioEmail, string SentioPassword, string SentioUrl)
        {
            try
            {


                await Page.SetViewportSizeAsync(1920, 1080);

                await Page.SetViewportSizeAsync(1920, 1080);
                // Navigate to the provided URL
                await Page.GotoAsync(SentioUrl);
                var homePage = new HomewebLoginObjects(Page);

                // Login Sequence
                await homePage.Login.ClickAsync();
                await homePage.UserName.FillAsync(SentioEmail);
                await Page.Keyboard.PressAsync("Enter");
                //await homePage.Next.ClickAsync();
                await homePage.Password.FillAsync(SentioPassword);
                await Page.Keyboard.PressAsync("Enter");
                // await homePage.Submit.ClickAsync(); // Or .PressAsync("Enter")

                // Begin Course
                await homePage.SentioAnxiety1.ClickAsync();
                await homePage.SentioAnxietyBegin.ClickAsync();
                await homePage.SentioAnxietyQuestion1.ClickAsync();
                await homePage.SentioAnxietyQuestion2.ClickAsync();

                // Handling the Dropdown (SelectElement equivalent)
                // Playwright selects options natively without needing a separate Select class
                await Page.Locator("select#jurisdictionSelect").SelectOptionAsync(new SelectOptionValue { Label = "Ontario" });

                await homePage.SentioAnxietystartprogram.ClickAsync();

                // Series of course clicks
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                // Mood Tracker & Journal
                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksMood1.ClickAsync();
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                // The manual scrollBy script is removed; Playwright handles it
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksMood1.ClickAsync();
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                // ... continued mood tracker interactions
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksMood1.ClickAsync();
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodcomplete.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                // Course interactions
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker2.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                // Cognitive Tasks
                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksCog1.ClickAsync();
                await homePage.SentioTasksCog2.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                // Repeat Cognitive Tasks
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksCog1.ClickAsync();
                await homePage.SentioTasksCog2.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksCog1.ClickAsync();
                await homePage.SentioTasksCog2.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodcomplete.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                // Final stretch of the course
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                await homePage.SentioAnxietycourseelectives.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                // Final Journal Entries
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");

                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");

                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietyquestion7.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                // Input tasks
                await homePage.SentioAnxietyinput1.ClickAsync();
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioAnxietyinput2.ClickAsync();
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioAnxietyinput3.ClickAsync();
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioAnxietyinput4.ClickAsync();
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioAnxietyinput5.ClickAsync();
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyinput6.ClickAsync();
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietyinput7.ClickAsync();
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioAnxietyinput2.ClickAsync();
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioAnxietyinput4.ClickAsync();
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                // Final Completion Block
                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioAnxietyselectentry.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioAnxietyinput2.ClickAsync();
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietyquestion7.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext7.ClickAsync();
                await homePage.SentioAnxietyquestion8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");

                await homePage.SentioAnxietycoursesubmit.ClickAsync();
                await homePage.SentioAnxietymoodcomplete.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();
                await homePage.SentioAnxietycompleteprogram.ClickAsync();
            }
            catch (Exception ex)
            {
            }
            
            
           
           
        }

        [Test, TestCaseSource(nameof(LoginJsonData)), Order(5)]
        public async Task SentioDepressionCourse(string SentioEmail, string SentioPassword, string SentioUrl)
        {
            try
            {
                await Page.SetViewportSizeAsync(1920, 1080);
                // Navigate and Initialize POM
                await Page.GotoAsync(SentioUrl);
                var homePage = new HomewebLoginObjects(Page);

                // Login Sequence
                await homePage.Login.ClickAsync();
                await homePage.UserName.FillAsync(SentioEmail);
                await Page.Keyboard.PressAsync("Enter");
                // await homePage.Next.ClickAsync();
                await homePage.Password.FillAsync(SentioPassword);
                await Page.Keyboard.PressAsync("Enter");
                // await homePage.Submit.ClickAsync(); // Or .PressAsync("Enter")

                // Begin Depression Course
                await homePage.SentioDepression.ClickAsync();
                await homePage.SentioDeperessionBegin.ClickAsync();
                await homePage.SentioAnxietyQuestion1.ClickAsync();
                await homePage.SentioAnxietyQuestion2.ClickAsync();

                // Handle Dropdown natively
                await Page.Locator("select#jurisdictionSelect").SelectOptionAsync(new SelectOptionValue { Label = "Ontario" });

                await homePage.SentioAnxietystartprogram.ClickAsync();

                // Course Navigation
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                // Mood Tracker & Journal
                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksMood1.ClickAsync();
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksMood1.ClickAsync();
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksMood1.ClickAsync();
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodcomplete.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");

                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");

                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietyquestion7.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksCog1.ClickAsync();
                await homePage.SentioAnxietyselectentry.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksCog1.ClickAsync();
                await homePage.SentioAnxietyselectentry.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodcomplete.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");

                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");

                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyinput6.ClickAsync();
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietyquestion7.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext7.ClickAsync();
                await homePage.SentioAnxietyinput8.ClickAsync();
                await homePage.SentioAnxietynext8.ClickAsync();
                await homePage.SentioAnxietyquestion9.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksCog1.ClickAsync();
                await homePage.SentioAnxietyselectentry.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioAnxietyinput2.ClickAsync();
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");

                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");

                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");

                // Note: I preserved your logic here, but in your original code you clicked 'next5' twice 
                // instead of 'next6' and 'next7'. Keep an eye on this if it fails in execution!
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion7.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext7.ClickAsync();
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodcomplete.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();
                await homePage.SentioAnxietycompleteprogram.ClickAsync();
            }
            catch (Exception ex)
            {
            }
           
        }

        [Test, TestCaseSource(nameof(LoginJsonData)), Order(6)]
        public async Task SentioMentalHealthandWellnessCourse(string SentioEmail, string SentioPassword, string SentioUrl)
        {

            try
            {
                await Page.SetViewportSizeAsync(1920, 1080);
                // Navigate and Initialize POM
                await Page.GotoAsync(SentioUrl);
                var homePage = new HomewebLoginObjects(Page);

                // Login Sequence
                await homePage.Login.ClickAsync();
                await homePage.UserName.FillAsync(SentioEmail);
                await Page.Keyboard.PressAsync("Enter");
                await homePage.Password.FillAsync(SentioPassword);
                await Page.Keyboard.PressAsync("Enter"); // Or .PressAsync("Enter")

                // Begin Mental Health and Wellness Course
                await homePage.SentioMentalHealth.ClickAsync();
                await homePage.SentioMentalHealthbegin.ClickAsync();

                // Initial Questionnaire
                await homePage.SentioAnxietyQuestion1.ClickAsync();
                await homePage.SentioAnxietyQuestion2.ClickAsync();
                await homePage.SentioAnxietyQuestion1.ClickAsync();
                await homePage.SentioAnxietyQuestion2.ClickAsync();

                // Handle Dropdown natively
                await Page.Locator("select#jurisdictionSelect").SelectOptionAsync(new SelectOptionValue { Label = "Ontario" });

                await homePage.SentioAnxietystartprogram.ClickAsync();

                // Course Navigation Block 1
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                // Mood Tracker & Journal - Entry 1
                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioAnxietyinput5.ClickAsync();
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                // Mood Tracker & Journal - Entry 2
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioAnxietyinput5.ClickAsync();
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                // Mood Tracker & Journal - Entry 3
                // Note: Removed ExecuteScript("window.scrollBy(0, 400);") - Playwright auto-scrolls
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioAnxietyinput5.ClickAsync();
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioAnxietyinput5.ClickAsync();
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioAnxietyinput5.ClickAsync();
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioAnxietyinput5.ClickAsync();
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioAnxietyinput5.ClickAsync();
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();



                // Course Navigation Block 2
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietyquestion7.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext7.ClickAsync();
                await homePage.SentioAnxietyquestion8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext8.ClickAsync();
                await homePage.SentioAnxietyquestion9.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();
                await homePage.SentioAnxietycourseelectives.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyinput6.ClickAsync();
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietyquestion7.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext7.ClickAsync();
                await homePage.SentioAnxietyinput8.ClickAsync();
                await homePage.SentioAnxietynext8.ClickAsync();
                await homePage.SentioAnxietyquestion9.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksCog1.ClickAsync();
                await homePage.SentioAnxietyselectentry.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioAnxietyinput2.ClickAsync();
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietyquestion7.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext7.ClickAsync();
                await homePage.SentioAnxietyquestion8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodcomplete.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                await homePage.SentioAnxietyinput1.ClickAsync();
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioAnxietyinput2.ClickAsync();
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioAnxietyinput3.ClickAsync();
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioAnxietyinput4.ClickAsync();
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioAnxietyinput5.ClickAsync();
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyinput6.ClickAsync();
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietyinput7.ClickAsync();
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");

                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioAnxietyinput2.ClickAsync();
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioAnxietyinput4.ClickAsync();
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();
                await homePage.SentioAnxietycompleteprogram.ClickAsync();
            }
            catch (Exception ex)
            {
            }
          
        }

        [Test, TestCaseSource(nameof(LoginJsonData)), Order(7)]
        public async Task SentioCoExistingAnxietyandDepressionCourse(string SentioEmail, string SentioPassword, string SentioUrl)
        {

            try
            {
                await Page.SetViewportSizeAsync(1920, 1080);
                // Navigate to the provided URL and Initialize POM
                await Page.GotoAsync(SentioUrl);
                var homePage = new HomewebLoginObjects(Page);

                // Login Sequence
                await homePage.Login.ClickAsync();
                await homePage.UserName.FillAsync(SentioEmail);
                await Page.Keyboard.PressAsync("Enter");
                await homePage.Password.FillAsync(SentioPassword);
                await Page.Keyboard.PressAsync("Enter");

                // Begin Co-Existing Course
                await homePage.SentioCoexist.ClickAsync();
                await homePage.SentioAnxietyBegin.ClickAsync();

                // Initial Questionnaire
                await homePage.SentioAnxietyQuestion1.ClickAsync();
                await homePage.SentioAnxietyQuestion2.ClickAsync();
                await homePage.SentioAnxietyQuestion1.ClickAsync();
                await homePage.SentioAnxietyQuestion2.ClickAsync();

                // Handle Dropdown
                await Page.Locator("select#jurisdictionSelect").SelectOptionAsync(new SelectOptionValue { Label = "Ontario" });

                await homePage.SentioAnxietystartprogram.ClickAsync();

                // Course Block 1
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                // Mood Tracker & Journal - Section 1
                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksMood1.ClickAsync();
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksMood1.ClickAsync();
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksMood1.ClickAsync();
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodcomplete.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                // Course Block 2
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                // Journal Entry Sequence 1
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");

                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietyquestion7.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext7.ClickAsync();
                await homePage.SentioTasksjournal9.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                // Course Block 3
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();



                // Mood Tracker & Journal - Section 2 (Cognitive)
                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksCog1.ClickAsync();
                await homePage.SentioAnxietyselectentry.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");

                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");

                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietyquestion7.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioAnxietynext7.ClickAsync();
                await homePage.SentioAnxietyquestion8.FillAsync("Test");
                await Page.Keyboard.PressAsync("Enter");
                await homePage.SentioTasksjournal9.ClickAsync();



                // Course Block 4
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                // --- MOOD TRACKER SECTION 1 ---
                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksCog1.ClickAsync();
                await homePage.SentioAnxietyselectentry.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await homePage.SentioTasksjournal9.ClickAsync();

                // --- MOOD TRACKER REPEAT 1 ---
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksCog1.ClickAsync();
                await homePage.SentioAnxietyselectentry.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await homePage.SentioTasksjournal9.ClickAsync();

                // --- MOOD TRACKER REPEAT 2 ---
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksCog1.ClickAsync();
                await homePage.SentioAnxietyselectentry.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await homePage.SentioTasksjournal9.ClickAsync();
                await homePage.SentioAnxietymoodcomplete.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                // --- COURSE PROGRESSION 1 ---
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                // --- MOOD TRACKER SECTION 2 ---
                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietymoodtracker1.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await homePage.SentioTasksjournal9.ClickAsync();
                await homePage.SentioAnxietymoodcomplete.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                // --- COURSE PROGRESSION 2 ---
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();
                await homePage.SentioAnxietycourseelectives.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                // --- ANXIETY INPUT & JOURNAL COMBINATION 1 ---
                await homePage.SentioAnxietyinput1.ClickAsync();
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioAnxietyinput2.ClickAsync();
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioAnxietyinput3.ClickAsync();
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioAnxietyinput4.ClickAsync();
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioAnxietyinput5.ClickAsync();
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyinput6.ClickAsync();
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietyinput7.ClickAsync();
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                // --- ANXIETY INPUT & JOURNAL COMBINATION 2 ---
                await homePage.SentioTasksjournal2.FillAsync("Test");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioAnxietyinput2.ClickAsync();
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioAnxietyinput4.ClickAsync();
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                // --- COURSE PROGRESSION 3 ---
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                // --- MOOD TRACKER SECTION 3 ---
                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioTasksjournal4.FillAsync("Test");
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");

                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyinput6.ClickAsync();
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietyquestion7.FillAsync("Test");
                await homePage.SentioAnxietynext7.ClickAsync();
                await homePage.SentioAnxietyinput8.ClickAsync();
                await homePage.SentioAnxietynext8.ClickAsync();
                await homePage.SentioAnxietyquestion9.FillAsync("Test");
                await homePage.SentioTasksjournal9.ClickAsync();

                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();
                await homePage.SentioAnxietycourse1.ClickAsync();

                // --- FINAL PROGRAM COMPLETION ---
                await homePage.SentioAnxietymoodtracker.ClickAsync();
                await homePage.SentioAnxietymoodtracker1.ClickAsync();
                await homePage.SentioTasksCog1.ClickAsync();
                await homePage.SentioAnxietyselectentry.ClickAsync();

                await homePage.SentioTasksjournal2.FillAsync("Test");
                await homePage.SentioTasksjournal3.ClickAsync();
                await homePage.SentioAnxietyinput2.ClickAsync();
                await homePage.SentioTasksjournal5.ClickAsync();
                await homePage.SentioTasksjournal6.FillAsync("Test");
                await homePage.SentioTasksjournal7.ClickAsync();
                await homePage.SentioTasksjournal8.FillAsync("Test");
                await homePage.SentioTasksCog3.ClickAsync();
                await homePage.SentioTasksCog4.FillAsync("Test");
                await homePage.SentioAnxietynext5.ClickAsync();
                await homePage.SentioAnxietyquestion6.FillAsync("Test");
                await homePage.SentioAnxietynext6.ClickAsync();
                await homePage.SentioAnxietyquestion7.FillAsync("Test");
                await homePage.SentioAnxietynext7.ClickAsync();
                await homePage.SentioAnxietyquestion8.FillAsync("Test");

                await homePage.SentioAnxietycoursesubmit.ClickAsync();
                await homePage.SentioAnxietymoodcomplete.ClickAsync();
                await homePage.SentioAnxietycoursesubmit.ClickAsync();
                await homePage.SentioAnxietycompleteprogram.ClickAsync();
            }
            catch (Exception ex)
            {
            }
            
          
        }

        private async Task ClickAndNavigateBackAsync(ILocator locator)
        {
            await locator.ClickAsync();
            await Page.GoBackAsync();
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
                test.Log(AventStack.ExtentReports.Status.Fail, $"Test failed with logtrace: {stackTrace}");
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                test.Pass("Test Passed");
            }
        }
    }
}