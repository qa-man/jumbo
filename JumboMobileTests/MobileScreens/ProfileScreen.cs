using JumboMobileTests.Extensions;
using JumboMobileTests.MobileScreens.Elements;
using JumboMobileTests.MobileScreens.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens;

public class ProfileScreen : BaseScreen, INavigable
{
    private const string ProfileScreenTitle = "Mijn profiel";
    private AppiumWebElement UitloggenButton => Driver.GetElement(By.XPath("//*[@class='android.widget.Button']"));

    public ProfileScreen(AndroidDriver<AppiumWebElement> driver) : base(driver)
    {
        NavigationBar = new NavigationBar(driver);
    }

    public NavigationBar NavigationBar { get; }

    public WelcomeScreen SignOut()
    {
        Driver.ScrollToElementWithText("Uitloggen");
        UitloggenButton.TapElement();
        return UitloggenButton.TapElement<WelcomeScreen>(Driver);
    }

    public override bool Displayed()
    {
        return IsTextDisplayedOnScreen(ProfileScreenTitle);
    }
}