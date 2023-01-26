using System;
using System.IO;
using JumboMobileTests.Helpers;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using TechTalk.SpecFlow;
using TestContext = NUnit.Framework.TestContext;

namespace JumboMobileTests
{
    [Binding]
    public class Hooks
    {
        private static AndroidDriver<AppiumWebElement> _driver;
        private static string TimeStamp => $"{DateTime.Now:dd MMMM yyyy ± HH·mm}";
        private static string _reportPath;
        private static string _uniqueName;
        private static string _androidScreenRecording;

        [BeforeTestRun]
        public static void Initializer()
        {
            SetupReporting();
            AndroidHelper.RestartAdbServer();
            _driver = AndroidHelper.InitializeAndroidDriver();
        }

        [BeforeScenario]
        public static void TestInitialize(ScenarioContext scenarioContext)
        {
            _uniqueName = Path.Combine(_reportPath!, $"{scenarioContext.ScenarioInfo.Title} [{TimeStamp}]");
            _androidScreenRecording = $"{_uniqueName}.mp4";
            _driver!.StartRecordingScreen();
        }

        [BeforeScenario("Login")]
        public static void BeforeLoginScenario()
        {
            _driver.ResetApp();
        }

        [AfterScenario]
        public static void TestCleanup()
        {
            var result = TestContext.CurrentContext.Result;
            switch (result.Outcome.Status)
            {
                case TestStatus.Failed:
                case TestStatus.Inconclusive:
                case TestStatus.Warning:
                    var screenshot = $"{_uniqueName}.png";
                    _driver!.GetScreenshot().SaveAsFile(screenshot, ScreenshotImageFormat.Png);
                    TestContext.AddTestAttachment(screenshot);
                    break;
            }

            File.WriteAllBytes(_androidScreenRecording!, Convert.FromBase64String(_driver!.StopRecordingScreen()));
            TestContext.AddTestAttachment(_androidScreenRecording!);
        }

        [AfterTestRun]
        public static void Finalizer()
        {
            AndroidHelper.DisposeAndroidDriver();
        }

        #region Private Methods

        private static void SetupReporting()
        {
            _reportPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Results", $"{TimeStamp}");
            Directory.CreateDirectory(_reportPath);
        }

        #endregion

    }
}