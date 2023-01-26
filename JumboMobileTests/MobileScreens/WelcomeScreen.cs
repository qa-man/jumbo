using JumboMobileTests.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens;

public class WelcomeScreen : BaseScreen
{
    private By WelcomeScreenContentLocator => MobileBy.AccessibilityId("Jumbo Logo");
    private By AcceptCookiesButtonLocator => By.Id("bt_accept_all");
    private AppiumWebElement LoginButton => Driver.GetElement(By.XPath("//*[@text='Inloggen']"));

    public WelcomeScreen(AndroidDriver<AppiumWebElement> driver) : base(driver) { }

    public void AcceptCookiesIfRequested()
    {
        if (Driver.IsElementDisplayed(AcceptCookiesButtonLocator)) Driver.GetElement(AcceptCookiesButtonLocator).TapElement();
    }

    public LoginScreen TapInloggenButton()
    {
        return LoginButton.TapElement<LoginScreen>(Driver);
    }

    public override bool Displayed()
    {
        return Driver.IsElementDisplayed(WelcomeScreenContentLocator);
    }
}