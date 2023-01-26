using OpenQA.Selenium;

namespace JumboMobileTests.MobileScreens.Interfaces
{
    public interface IQuantitative : IWebElement
    {
        public static abstract By Locator { get; }
    }
}
