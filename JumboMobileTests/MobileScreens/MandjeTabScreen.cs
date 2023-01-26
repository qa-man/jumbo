using System;
using JumboMobileTests.Enums;
using JumboMobileTests.Extensions;
using JumboMobileTests.MobileScreens.Elements;
using JumboMobileTests.MobileScreens.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;

namespace JumboMobileTests.MobileScreens;

public class MandjeTabScreen : BaseScreen, INavigable
{
    private const string MandjeTabName = "Mandje";
    private readonly By _checkoutButtonLocator = By.Id("fl_checkout");
    private readonly By _productTitleLocator = By.Id("tv_title");
    private readonly By _productPriceLocator = By.Id("tv_price");
    private readonly By _tipPopupLocator = By.Id("tv_description");
    
    public NavigationBar NavigationBar { get; }
    public string ProductTitle => Driver.GetElement(_productTitleLocator).Text;
    public int ProductCount => int.Parse(Driver.GetElement(By.Id("tv_value")).Text);

    public MandjeTabScreen(AndroidDriver<AppiumWebElement> driver) : base(driver)
    {
        NavigationBar = new NavigationBar(driver);
        CloseInfoPopupIfDisplayed();
    }

    public override bool Displayed()
    {
        return IsTextDisplayedOnScreen(MandjeTabName) && Driver.IsElementDisplayed(_checkoutButtonLocator);
    }

    public void CloseInfoPopupIfDisplayed()
    {
        if (!Driver.IsElementDisplayed(_tipPopupLocator, timeLimitInSeconds: 2.5)) return;
        var size = Driver.Manage().Window.Size;
        new TouchAction(Driver).Tap(size.Width*0.5, size.Height*0.5).Perform();
    }

    public void RemoveProduct()
    {
        Driver.SwipeElement(Driver.GetElement(_productPriceLocator), Direction.Left);
    }

    public bool IsProductDisplayedInBasket(string productName)
    {
        if (Driver.IsElementDisplayed(_productTitleLocator)) return ProductTitle.Equals(productName) || ProductTitle.Contains(productName!);
        return false;
    }
}