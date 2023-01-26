using JumboMobileTests.Extensions;
using JumboMobileTests.MobileScreens.Elements;
using JumboMobileTests.MobileScreens.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens;

public class ReceptenTabScreen : BaseScreen, INavigable
{
    private const string ReceptenTabName = "Recepten";
    private readonly By _receptenContentLocator = By.Id("cookbook_tab");
    private readonly By _infoPopupLocator = By.Id("bt_positive");

    public NavigationBar NavigationBar { get; }

    public ReceptenTabScreen(AndroidDriver<AppiumWebElement> driver) : base(driver)
    {
        NavigationBar = new NavigationBar(driver);
        CloseInfoPopupIfDisplayed();
    }

    public override bool Displayed()
    {
        return IsTextDisplayedOnScreen(ReceptenTabName) && Driver.IsElementDisplayed(_receptenContentLocator);
    }

    public void CloseInfoPopupIfDisplayed()
    {
        if (Driver.IsElementDisplayed(_infoPopupLocator)) Driver.GetElement(_infoPopupLocator).TapElement();
    }
}