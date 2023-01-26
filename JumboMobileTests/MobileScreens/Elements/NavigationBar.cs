using JumboMobileTests.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens.Elements;

public class NavigationBar: BaseElement
{
    private static By NavigationBarLocator => By.Id("bottom_navigation_container");
    private readonly By _mijnJumboTabLocator = By.Id("myJumbo");
    private readonly By _productenTabLocator = By.Id("nav_groceries");
    private readonly By _receptenTabLocator = By.Id("nav_recipes");
    private readonly By _aanbiedingenTabLocator = By.Id("promotions");
    private readonly By _mandjeLocator = By.Id("shopping_list");
    private readonly By _mandjeCountLocator = By.Id("tv_basket_count");

    public AppiumWebElement MijnJumboTabButton => Driver.GetElement(_mijnJumboTabLocator);
    public AppiumWebElement ProductenTabButton => Driver.GetElement(_productenTabLocator);
    public AppiumWebElement ReceptenTabButton => Driver.GetElement(_receptenTabLocator);
    public AppiumWebElement AanbiedingenTabButton => Driver.GetElement(_aanbiedingenTabLocator);
    public AppiumWebElement MandjeButton => Driver.GetElement(_mandjeLocator);
    public int MandjeCount => int.Parse(Driver.GetElement(_mandjeCountLocator).Text);

    public NavigationBar(AndroidDriver<AppiumWebElement> driver) : base(driver, driver.GetElement(NavigationBarLocator).Id) { }
}