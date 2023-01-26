using JumboMobileTests.MobileScreens.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens.Elements;

public class SearchResultItem: BaseElement, IQuantitative
{
    public static By Locator => By.XPath("//*/androidx.recyclerview.widget.RecyclerView/android.widget.LinearLayout");
    public string Price => FindElement(By.Id("tv_price")).Text;
    public string Title => FindElement(By.Id("tv_title")).Text;

    public SearchResultItem(AppiumWebElement parent, string id) : base(parent.WrappedDriver as AndroidDriver<AppiumWebElement>, id) { }
}