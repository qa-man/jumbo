using System.Globalization;
using JumboMobileTests.Extensions;
using JumboMobileTests.MobileScreens.Elements;
using JumboMobileTests.MobileScreens.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens;

public class ProductScreen : BaseScreen, INavigable
{
    private const string ProductTabName = "Product";
    private By ProductTitleLocator => By.Id("product_title");
    private readonly By _productPrice = By.Id("product_price");
    private readonly By _addToBasket = By.Id("iv_plus");
    
    public AppiumWebElement BackButton => Driver.GetElement(By.XPath("//*[@class='android.widget.ImageButton']"));
    public AppiumWebElement AddToBasketButton => Driver.GetElement(_addToBasket);
    public string ProductName => Driver.GetElement(ProductTitleLocator).Text;
    public decimal ProductPrice => decimal.Parse(string.Join('.', Driver.GetElement(_productPrice).Text.Split()), CultureInfo.InvariantCulture);

    public NavigationBar NavigationBar { get; }

    public ProductScreen(AndroidDriver<AppiumWebElement> driver) : base(driver)
    {
        NavigationBar = new NavigationBar(driver);
        Driver.WaitElementToBeDisplayed(ProductTitleLocator);
    }

    public override bool Displayed()
    {
        return IsTextDisplayedOnScreen(ProductTabName) && Driver.IsElementDisplayed(ProductTitleLocator);
    }

    public void AddToBasket()
    {
        AddToBasketButton.TapElement();
    }
}