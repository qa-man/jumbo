using JumboMobileTests.Extensions;
using JumboMobileTests.MobileScreens.Elements;
using JumboMobileTests.MobileScreens.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens;

public class AanbiedingenTabScreen : BaseScreen, INavigable
{
    private const string AanbiedingenTabName = "Aanbiedingen";
    private By AanbiedingenContentLocator => By.Id("rv_promotions_overview");

    public NavigationBar NavigationBar { get; }

    public AanbiedingenTabScreen(AndroidDriver<AppiumWebElement> driver) : base(driver)
    {
        NavigationBar = new NavigationBar(driver);
    }

    public override bool Displayed()
    {
        return IsTextDisplayedOnScreen(AanbiedingenTabName) && Driver.IsElementDisplayed(AanbiedingenContentLocator);
    }
}