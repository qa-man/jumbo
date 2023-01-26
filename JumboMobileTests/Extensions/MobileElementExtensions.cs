using System;
using System.Collections.Generic;
using System.Linq;
using JumboMobileTests.Helpers;
using JumboMobileTests.MobileScreens.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Interactions;

namespace JumboMobileTests.Extensions
{
    public static class MobileElementExtensions
    {
        public static void TapElement(this AppiumWebElement element)
        {
            element.Click();
        }

        public static T TapElement<T>(this AppiumWebElement element, object parameter)
        {
            element.TapElement();
            return (T)Activator.CreateInstance(typeof(T), parameter);
        }

        public static AppiumWebElement GetElement(this AppiumWebElement parent, By locator, double timeLimitInSeconds = 30)
        {
            (parent.WrappedDriver as AndroidDriver<AppiumWebElement>).WaitElementToBeDisplayed(locator, timeLimitInSeconds);
            return parent.FindElement(locator);
        }

        public static List<T> GetElements<T>(this AppiumWebElement parent) where T : IQuantitative
        {
            return (parent.WrappedDriver as AndroidDriver<AppiumWebElement>)?.FindElements(T.Locator).Select(e => (T)Activator.CreateInstance(typeof(T), e, e.Id)).ToList();
        }

        public static void EnterText(this AppiumWebElement element, string text)
        {
            element.TapElement();
            if(!string.IsNullOrWhiteSpace(element.Text)) element.Clear();
            element.SendKeys(text);
        }

        public static void EnterTextUsingActions(this AppiumWebElement element, string text)
        {
            new Actions(AndroidHelper.CurrentAndroidDriver).SendKeys(element, text).Perform();
        }

    }
}