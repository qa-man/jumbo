using JumboMobileTests.Extensions;
using JumboMobileTests.MobileScreens.Elements;
using JumboMobileTests.MobileScreens.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens;

public class ProductenTabScreen : BaseScreen, INavigable
{
    private const string ProductenTabName = "Producten";
    private By ProductenContentLocator => By.Id("rv_category");
    
    public NavigationBar NavigationBar { get; }

    public ProductenTabScreen(AndroidDriver<AppiumWebElement> driver) : base(driver)
    {
        NavigationBar = new NavigationBar(driver);
    }

    public override bool Displayed()
    {
        return IsTextDisplayedOnScreen(ProductenTabName) && Driver.IsElementDisplayed(ProductenContentLocator);
    }
}