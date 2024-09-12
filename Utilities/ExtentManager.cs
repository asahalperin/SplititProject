using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;


namespace SplititAutomation.Utilities
{
    public static class ExtentManager
    {
        private static ExtentReports _extent;
        private static ExtentSparkReporter _htmlReporter;

        public static ExtentReports GetInstance()
        {
            if (_extent == null)
            {
                // Determine the project root directory
                string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

                // Combine the project root with the Reports folder
                string reportDirectory = Path.Combine(projectRoot, "Reports");

                // Ensure the directory exists
                if (!Directory.Exists(reportDirectory))
                {
                    Directory.CreateDirectory(reportDirectory);
                }

                // Create the report file with a timestamp
                string reportPath = Path.Combine(reportDirectory, $"TestReport_{DateTime.Now:yyyyMMdd_HHmmss}.html");

                // Initialize the reporter and the extent instance
                _htmlReporter = new ExtentSparkReporter(reportPath);
                _extent = new ExtentReports();
                _extent.AttachReporter(_htmlReporter);
            }

            return _extent;
        }
    }
}
