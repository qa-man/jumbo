using System.Collections.Generic;
using JumboMobileTests.Extensions;
using JumboMobileTests.MobileScreens.Elements;
using JumboMobileTests.MobileScreens.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens;

public class MijnJumboTabScreen : BaseScreen, INavigable
{
    private readonly By _userProfileLocator = By.Id("iv_user_profile");
    private readonly By _closeBestelwekkerLocator = By.Id("iv_close");
    private AppiumWebElement SearchInput => Driver.GetElement(By.Id("bt_search"));
    private AppiumWebElement SearchInputField => Driver.GetElement(By.Id("search_plate"));
    private AppiumWebElement UserProfileButton => Driver.GetElement(_userProfileLocator);
    private SearchResultView _searchResultView;

    public NavigationBar NavigationBar { get; }

    public MijnJumboTabScreen(AndroidDriver<AppiumWebElement> driver) : base(driver)
    {
        CloseBestelwekkerPopupIfDisplayed();
        NavigationBar = new NavigationBar(Driver);
    }

    public override bool Displayed()
    {
        return Driver.IsElementDisplayed(_userProfileLocator, timeLimitInSeconds: 2.5);
    }

    public WelcomeScreen SignOut()
    {
        return UserProfileButton.TapElement<ProfileScreen>(Driver).SignOut();
    }

    public void CloseBestelwekkerPopupIfDisplayed()
    {
        if(Driver.IsElementDisplayed(_closeBestelwekkerLocator)) Driver.GetElement(_closeBestelwekkerLocator).TapElement();
    }

    public SearchResultView SearchByQuery(string query)
    {
        SearchInput.TapElement();
        SearchInputField.EnterTextUsingActions(query);
        Driver.PressKeyCode(AndroidKeyCode.Enter);
        return _searchResultView = new SearchResultView(Driver);
    }

    public List<SearchResultItem> GetSearchResultItems()
    {
        return _searchResultView.Results;
    }
}