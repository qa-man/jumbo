using System.Collections.Generic;
using JumboMobileTests.Extensions;
using JumboMobileTests.MobileScreens.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens.Elements;

public class SearchResultView : BaseElement, INavigable
{
    private static By SearchResultViewLocator => By.Id("nav_host_fragment");
    private readonly By _resultsCountLocator = By.Id("tv_results");
    private readonly By _filteringLocator = By.Id("bt_filters");

    public NavigationBar NavigationBar { get; }
    public AppiumWebElement FilteringButton => this.GetElement(_filteringLocator);
    public List<SearchResultItem> Results => this.GetElements<SearchResultItem>();

    public SearchResultView(AndroidDriver<AppiumWebElement> driver) : base(driver, driver.GetElement(SearchResultViewLocator).Id)
    {
        NavigationBar = new NavigationBar(driver);
        Driver.WaitElementToBeDisplayed(_resultsCountLocator);
    }
}