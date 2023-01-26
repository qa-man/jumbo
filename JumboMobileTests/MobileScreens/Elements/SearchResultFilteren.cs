using JumboMobileTests.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System.Collections.Generic;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens.Elements;

public class SearchResultFilteren : BaseElement
{
    private static By SearchResultFilterScreenLocator => By.Id("filter_options_wrapper");

    public List<CategoryElement> Results => this.GetElements<CategoryElement>();

    public SearchResultFilteren(AndroidDriver<AppiumWebElement> driver) : base(driver, driver.GetElement(SearchResultFilterScreenLocator).Id) { }
}