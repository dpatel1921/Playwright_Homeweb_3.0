/*using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

public class ExtentManager
{

    private static ExtentReports extent;
    private static ExtentHtmlReporter htmlReporter;

    public static ExtentReports GetReporter()
    {
        if (extent == null)
        {

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string reportPath = $"C:\\TestData\\ExtentReport_{timestamp}.html";
            // string reportDirectory = $"C:\\TestData\\TestRun_{timestamp}\\";
            var htmlReporter = new ExtentV3HtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local host");
            extent.AddSystemInfo("Environment", "Production");
            extent.AddSystemInfo("Username", "dpatel");
        }
        return extent;
    }
}*/
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;



    public class ExtentManager
    {
        private static ExtentReports extent;

        public static ExtentReports GetReporter()
        {
            if (extent == null)
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string reportPath = $"C:\\TestData\\ExtentReport_{timestamp}.html";

                // Using the modern Spark Reporter (Standard for ExtentReports v5+)
                var sparkReporter = new ExtentSparkReporter(reportPath);

                // Optional: Customize the report's look and feel
                sparkReporter.Config.DocumentTitle = "Homeweb Automation Status";
                sparkReporter.Config.ReportName = "Playwright NUnit Test Results";
                sparkReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;

                extent = new ExtentReports();
                extent.AttachReporter(sparkReporter);

                extent.AddSystemInfo("Host Name", "Local host");
                extent.AddSystemInfo("Environment", "Production");
                extent.AddSystemInfo("Username", "dpatel");
                extent.AddSystemInfo("Framework", "Playwright C#"); // Added context
            }
            return extent;
        }
    }

