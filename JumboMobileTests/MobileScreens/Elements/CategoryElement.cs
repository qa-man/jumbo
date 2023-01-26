using System.Linq;
using System.Text.RegularExpressions;
using JumboMobileTests.MobileScreens.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens.Elements;

public class CategoryElement : BaseElement, IQuantitative
{
    public static By Locator => By.Id("filter_checkbox");
    public int Number => int.Parse(Regex.Matches(Text, @"\((\d+)\)").Select(m => m.Groups[1].Value).Single());

    public CategoryElement(AppiumWebElement parent, string id) : base(parent.WrappedDriver as AndroidDriver<AppiumWebElement>, id) { }
}