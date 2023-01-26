using System;
using System.Collections.Generic;
using JumboMobileTests.Enums;
using JumboMobileTests.Extensions;
using JumboMobileTests.Helpers;
using JumboMobileTests.MobileScreens;
using JumboMobileTests.MobileScreens.Elements;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using TechTalk.SpecFlow;

namespace JumboMobileTests.StepDefinitions
{
    [Binding]
    public sealed class MobileStepDefinitions
    {
        private static AndroidDriver<AppiumWebElement> Driver => AndroidHelper.CurrentAndroidDriver;
        private WelcomeScreen _welcomeScreen;
        private LoginScreen _loginScreen;
        private NavigationBar _navigationBar;
        private MijnJumboTabScreen _mijnJumboTabScreen;
        private ProductenTabScreen _productenTabScreen;
        private ReceptenTabScreen _receptenTabScreen;
        private AanbiedingenTabScreen _aanbiedingenTabScreen;
        private MandjeTabScreen _mandjeTabScreen;
        private SearchResultView _searchResultView;
        private List<SearchResultItem> _searchResults;
        private SearchResultFilteren _searchResultFilteren;
        private ProductScreen _productScreen;
        private string _testableProductName;
        private int _previousBasketItemsCount;

        #region Given

        [Given(@"Jumbo mobile app opened")]
        public void GivenJumboMobileAppOpened()
        {
            _welcomeScreen = new WelcomeScreen(Driver);
            _welcomeScreen.AcceptCookiesIfRequested();
        }

        [Given(@"User logged into the app")]
        public void GivenUserLoggedIn()
        {
            if (!_welcomeScreen.Displayed()) { _mijnJumboTabScreen = new MijnJumboTabScreen(Driver); _navigationBar = _mijnJumboTabScreen.NavigationBar; return; }
            _loginScreen = _welcomeScreen.TapInloggenButton();
            _mijnJumboTabScreen = _loginScreen.LogIn();
            _navigationBar = _mijnJumboTabScreen.NavigationBar;
        }

        [Given(@"""Mijn Jumbo"" screen displayed")]
        public void GivenMijnJumboTabDisplayed()
        {
            if(!_mijnJumboTabScreen.Displayed()) _mijnJumboTabScreen = _navigationBar.MijnJumboTabButton.TapElement<MijnJumboTabScreen>(Driver);
        }

        [Given(@"User is NOT logged into the app")]
        public void GivenUserIsNotLoggedIntoTheApp()
        {
            if (_welcomeScreen.Displayed()) return;
            _navigationBar = new NavigationBar(Driver);
            _welcomeScreen = _navigationBar.MijnJumboTabButton.TapElement<MijnJumboTabScreen>(Driver).SignOut();
        }

        [Given(@"List of products displayed")]
        public void GivenListOfProductsDisplayed()
        {
            GivenUserLoggedIn();
            WhenUserPerformsSearchByQueryUsingSearchField();
        }

        [Given(@"The product details screen displayed")]
        public void GivenTheProductDetailsScreenDisplayed()
        {
            GivenListOfProductsDisplayed();
            WhenUserTapsOnProductItemToGetMoreDetails();
        }

        #endregion

        #region When

        [When(@"User taps on ""Inloggen"" button on 'Welcome' screen")]
        public void WhenUserTapsOnButtonOnScreen()
        {
            _loginScreen = _welcomeScreen.TapInloggenButton();
        }

        [When(@"User taps ""Inloggen"" button on 'Login' screen")]
        public void WhenUserTapsButtonOnScreen()
        {
            _loginScreen.TapInloggenButton();
        }

        [When(@"User taps ""Inloggen"" button on 'Login' screen for login")]
        public void WhenUserTapsButtonOnScreenForLogin()
        {
            _mijnJumboTabScreen = _loginScreen.TapInloggenButtonForLogin();
        }

        [When(@"User enters valid username into username text input")]
        public void WhenUserEntersValidUsernameIntoUsernameTextInput()
        {
            _loginScreen.EnterUsername();
        }

        [When(@"User removes username from username text input")]
        public void WhenUserRemovesUsernameFromUsernameTextInput()
        {
            _loginScreen.CleanUsernameField();
        }

        [When(@"User enters valid password into password text input")]
        public void WhenUserEntersValidPasswordIntoPasswordTextInput()
        {
            _loginScreen.EnterPassword();
        }

        [When(@"User taps ""Producten"" icon on navigation bar")]
        public void WhenUserTapsIconOnProductenNavigationBar()
        {
            _productenTabScreen = _mijnJumboTabScreen.NavigationBar.ProductenTabButton.TapElement<ProductenTabScreen>(Driver);
        }

        [When(@"User taps ""Recepten"" icon on navigation bar")]
        public void WhenUserTapsIconOnReceptenNavigationBar()
        {
            _receptenTabScreen = _productenTabScreen.NavigationBar.ReceptenTabButton.TapElement<ReceptenTabScreen>(Driver);
        }

        [When(@"User taps ""Aanbiedingen"" icon on navigation bar")]
        public void WhenUserTapsIconOnAanbiedingenNavigationBar()
        {
            _aanbiedingenTabScreen = _receptenTabScreen.NavigationBar.AanbiedingenTabButton.TapElement<AanbiedingenTabScreen>(Driver);
        }

        [When(@"User taps ""Mandje"" icon on navigation bar")]
        public void WhenUserTapsIconOnNavigationBar()
        {
            _mandjeTabScreen = _aanbiedingenTabScreen.NavigationBar.MandjeButton.TapElement<MandjeTabScreen>(Driver);
        }

        [When(@"User performs search by ([^""]*) using search field")]
        public void WhenUserPerformsSearchByQueryUsingSearchField(string query = "unicorn")
        {
            _mijnJumboTabScreen = _navigationBar.MijnJumboTabButton.TapElement<MijnJumboTabScreen>(Driver);
            _searchResultView = _mijnJumboTabScreen.SearchByQuery(query);
        }

        [When(@"User taps ""Filteren"" button on search result screen")]
        public void WhenUserTapsButtonOnSearchResultScreen()
        {
            _searchResultFilteren = _searchResultView.FilteringButton.TapElement<SearchResultFilteren>(Driver);
        }

        [When(@"User taps on random product item to get more details")]
        public void WhenUserTapsOnProductItemToGetMoreDetails()
        {
            _searchResults = _searchResultView.Results;
            var randomElement = _searchResults[new Random().Next(_searchResults.Count - 1)];
            var expectedTitle = randomElement.Title;
            
            _productScreen = randomElement.TapElement<ProductScreen>(Driver);
            _testableProductName = _productScreen.ProductName;
            
            Assert.AreEqual(expectedTitle, _testableProductName, "Incorrect product (title) displayed after tap on the product item displayed in the list");
        }

        [When(@"User add the product to a basket")]
        public void WhenUserAddTheProductToABasket()
        {
            _testableProductName = _productScreen.ProductName;
            _productScreen.AddToBasket();
        }

        [When(@"User navigates to the basket")]
        public void WhenUserNavigatesToTheBasket()
        {
            _mandjeTabScreen = _navigationBar.MandjeButton.TapElement<MandjeTabScreen>(Driver);
        }

        [When(@"User removes the product from the basket")]
        public void WhenUserRemovesTheProductFromTheBasket()
        {
            _mandjeTabScreen.RemoveProduct();
        }

        #endregion

        #region Then

        [Then(@"User redirected to Login screen")]
        public void ThenUserRedirectedToLoginScreen()
        {
            Assert.IsTrue(_loginScreen.Displayed(), "Login Screen does NOT displayed");
        }

        [Then(@"Warning about required fields displayed")]
        public void ThenWarningAboutRequiredFieldsDisplayed()
        {
            Assert.IsTrue(_loginScreen.RequiredValuesWarningDisplayed(), "Warning about required values does NOT displayed");
        }

        [Then(@"Warning about required username displayed")]
        public void ThenWarningAboutRequiredUsernameDisplayed()
        {
            Assert.IsTrue(_loginScreen.UsernameRequiredWarningDisplayed(), "Warning about required username value does NOT displayed");
        }

        [Then(@"Warning about required password displayed")]
        public void ThenWarningAboutRequiredPasswordDisplayed()
        {
            Assert.IsTrue(_loginScreen.PasswordRequiredWarningDisplayed(), "Warning about required password value does NOT displayed");
        }

        [Then(@"Warning about required username does NOT displayed")]
        public void ThenWarningAboutRequiredUsernameDoesNotDisplayed()
        {
            Assert.IsFalse(_loginScreen.UsernameRequiredWarningDisplayed(), "Warning about required username value still displayed");
        }

        [Then(@"Warning about required password does NOT displayed")]
        public void ThenWarningAboutRequiredPasswordDoesNotDisplayed()
        {
            Assert.IsFalse(_loginScreen.PasswordRequiredWarningDisplayed(), "Warning about required password value still displayed");
        }

        [Then(@"User logged into the app")]
        public void ThenUserLoggedIntoTheApp()
        {
            Assert.IsTrue(_mijnJumboTabScreen.Displayed());
        }

        [Then(@"User greeted by their first name ""([^""]*)"" on the home page")]
        public void ThenUserGreetedByTheirFirstNameOnTheHomePage(string greeting)
        {
            Assert.IsTrue(_mijnJumboTabScreen.IsTextDisplayedOnScreen(greeting));
        }

        [Then(@"""([^""]*)"" screen displayed")]
        public void ThenScreenDisplayed(Tab tab)
        {
            switch (tab)
            {
                case Tab.MijnJumbo:
                    Assert.IsTrue(_mijnJumboTabScreen.Displayed());
                    break;
                case Tab.Producten:
                    Assert.IsTrue(_productenTabScreen.Displayed());
                    break;
                case Tab.Recepten:
                    Assert.IsTrue(_receptenTabScreen.Displayed());
                    break;
                case Tab.Aanbiedingen:
                    Assert.IsTrue(_aanbiedingenTabScreen.Displayed());
                    break;
                case Tab.Mandje:
                    Assert.IsTrue(_mandjeTabScreen.Displayed());
                    break;
            }
        }

        [Then(@"""Mijn Jumbo"", ""Producten"", ""Recepten"", ""Aanbiedingen"" and ""Mandje"" tabs displayed on navigation bar")]
        public void ThenTabsDisplayedOnNavigationBar()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(_mijnJumboTabScreen.NavigationBar.MijnJumboTabButton.Displayed, "Mijn Jumbo tab does NOT displayed");
                Assert.IsTrue(_mijnJumboTabScreen.NavigationBar.ProductenTabButton.Displayed, "Producten tab does NOT displayed");
                Assert.IsTrue(_mijnJumboTabScreen.NavigationBar.ReceptenTabButton.Displayed, "Recepten tab does NOT displayed");
                Assert.IsTrue(_mijnJumboTabScreen.NavigationBar.AanbiedingenTabButton.Displayed, "Aanbiedingen tab does NOT displayed");
                Assert.IsTrue(_mijnJumboTabScreen.NavigationBar.MandjeButton.Displayed, "Mandje tab does NOT displayed");
            });
        }

        [Then(@"Search results displayed on screen")]
        public void ThenSearchResultsDisplayedOnScreen()
        {
            CollectionAssert.IsNotEmpty(_searchResults = _mijnJumboTabScreen.GetSearchResultItems());
        }

        [Then(@"All search result items has ([^""]*) in their names")]
        public void ThenAllSearchResultItemsHasUnicornInTheirNames(string query)
        {
            Assert.IsTrue(_searchResults.TrueForAll(result => result.Title.Contains(query)));
        }

        [Then(@"Price is displayed for all search result items")]
        public void ThenPriceIsDisplayedForAllSearchResultItems()
        {
            Assert.IsTrue(_searchResults.TrueForAll(result => (!string.IsNullOrWhiteSpace(result.Price) && result.Price != "0" && result.Price != "0 00")));
        }

        [Then(@"All categories with item count >0 are shown as options to refine the search results")]
        public void ThenNon_EmptyProductCategoriesDisplayedForSearchResults()
        {
            Assert.IsTrue(_searchResultFilteren.Results.TrueForAll(category => category.Number > 0), "There is category with incorrect number value for search results");
        }

        [Then(@"The product details screen displayed")]
        public void ThenTheProductDetailsScreenDisplayed()
        {
            Assert.IsTrue(_productScreen.Displayed(), "Product details screen does NOT displayed");
        }

        [Then(@"The price of the product is shown")]
        public void ThenThePriceOfTheProductIsShown()
        {
            Assert.IsTrue(_productScreen.ProductPrice != decimal.Zero && decimal.IsPositive(_productScreen.ProductPrice), "Product price displayed incorrect");
        }

        [Then(@"Add To Basket option is available")]
        public void ThenAddToBasketOptionIsAvailable()
        {
            Assert.IsTrue(_productScreen.AddToBasketButton.Displayed, "Add To Basket button does NOT displayed");
        }

        [Then(@"Back button is visible")]
        public void ThenBackButtonIsVisible()
        {
            Assert.IsTrue(_productScreen.BackButton.Displayed, "Back button unavailable");
        }

        [Then(@"Item count on basket icon is increased")]
        public void ThenItemCountOnBasketIconIsIncreased()
        {
            Assert.IsTrue(_navigationBar.MandjeCount > _previousBasketItemsCount, "Basket products count has NOT been increased");
            _previousBasketItemsCount = _navigationBar.MandjeCount;
        }

        [Then(@"Product is shown in the basket with the correct quantity")]
        public void ThenProductIsShownInTheBasketWithTheCorrectQuantity()
        {
            Assert.AreEqual(_navigationBar.MandjeCount, _mandjeTabScreen.ProductCount, "Product has incorrect quantity in the basket");
        }

        [Then(@"The product removed from the basket")]
        public void ThenTheProductRemovedFromTheBasket()
        {
            Assert.IsFalse(_mandjeTabScreen.IsProductDisplayedInBasket(_testableProductName), "The product still displayed in the basket after removing");
        }

        #endregion

    }
}