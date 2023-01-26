using System;
using JumboMobileTests.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Support.UI;

namespace JumboMobileTests.Extensions
{
    public static class MobileDriverExtensions
    {
        public static AppiumWebElement GetElement(this AndroidDriver<AppiumWebElement> driver, By locator, double timeLimitInSeconds = 30)
        {
            return WaitElementToBeDisplayed(driver, locator, timeLimitInSeconds).FindElement(locator);
        }

        public static AndroidDriver<AppiumWebElement> WaitElementToBeDisplayed(this AndroidDriver<AppiumWebElement> driver, By locator, double timeLimitInSeconds = 30)
        {
            if (IsElementDisplayed(driver, locator, timeLimitInSeconds)) return driver;
            throw new NotFoundException($"Element with locator '{locator}' has NOT been found.");
        }

        public static bool IsElementDisplayed(this AndroidDriver<AppiumWebElement> driver, By locator, double timeLimitInSeconds = 1)
        {
            try
            {
                var result = IsElementExist(driver, locator, timeLimitInSeconds) && WaitElementCondition(driver, SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator), timeLimitInSeconds);
                return result;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public static void ScrollToElementWithText(this AndroidDriver<AppiumWebElement> driver, string text)
        {
            var element = $"new UiScrollable(new UiSelector().scrollable(true)).setMaxSearchSwipes(8).scrollIntoView(new UiSelector().text(\"{text}\"))";
            driver.FindElementByAndroidUIAutomator(element);
        }
        
        public static void SwipeElement(this AndroidDriver<AppiumWebElement> driver, AppiumWebElement element, Direction direction, long duration = 250)
        {
            var screenSize = driver.Manage().Window.Size;
            var elementLocation = element.Location;

            int startX;
            int endX;
            int startY;

            switch (direction)
            {
                case Direction.Left:
                    startY = elementLocation.Y;
                    startX = (int)(screenSize.Width * 0.95);
                    endX = (int)(screenSize.Width * 0.05);
                    new TouchAction(driver)
                        .Press(startX, startY)
                        .Wait(duration)
                        .MoveTo(endX, startY)
                        .Release()
                        .Perform();
                    break;
            }
        }

        #region Private Methods

        private static bool IsElementExist(this AndroidDriver<AppiumWebElement> driver, By locator, double timeLimitInSeconds = 1)
        {
            try
            {
                WaitElementCondition(driver, SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator), timeLimitInSeconds);
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            return true;
        }

        private static bool WaitElementCondition(AndroidDriver<AppiumWebElement> driver, Func<IWebDriver, IWebElement> condition, double timeLimitInSeconds = 1)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeLimitInSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(100)
            };
            wait.IgnoreExceptionTypes(typeof(WebDriverException));
            return wait.Until(condition) is not null;
        }

        #endregion
    }
}