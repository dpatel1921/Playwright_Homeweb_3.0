using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Playwright_Homeweb_3._0
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            //Playwright
            using var playwright = await Playwright.CreateAsync();

            //Browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            //Page
            var page = await browser.NewPageAsync();
            await page.GotoAsync(url: "https://homeweb.ca/en");
            await page.ClickAsync(selector:"text=Sign in");
            await page.FillAsync(selector: "#emailAddress", value: "demo_DPatel@demo.com");
            await page.ClickAsync(selector: "text=Next");
            await page.FillAsync(selector: "#password", value: "Password!1");
            await page.Locator("xpath=//button[@type='submit']").ClickAsync();
            await page.ClickAsync(selector: "text=Access Sentio");


        }
    }
}