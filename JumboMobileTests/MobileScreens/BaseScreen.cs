using JumboMobileTests.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens;

public abstract class BaseScreen
{
    protected AndroidDriver<AppiumWebElement> Driver;

    protected BaseScreen(AndroidDriver<AppiumWebElement> driver)
    {
        Driver = driver;
    }
    
    public abstract bool Displayed();

    public bool IsTextDisplayedOnScreen(string text, double timeLimitInSeconds = 1)
    {
        return Driver.IsElementDisplayed(By.XPath($"//*[@text='{text}']"), timeLimitInSeconds);
    }
}