using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens.Elements;

public abstract class BaseElement: AppiumWebElement
{
    protected static AndroidDriver<AppiumWebElement> Driver;

    protected BaseElement(AndroidDriver<AppiumWebElement> driver, string id) : base(driver, id)
    {
        Driver = driver;
    }
}