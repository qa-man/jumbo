using JumboMobileTests.Extensions;
using JumboMobileTests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace JumboMobileTests.MobileScreens;

public class LoginScreen: BaseScreen
{
    private const string LoginScreenWelcomeLocator = "Welkom terug!";
    private const string RequiredValuesWarning = "De combinatie van e-mailadres en wachtwoord klopt helaas niet. Wachtwoord vergeten?";
    private const string RequiredPasswordWarning = "Je hebt nog geen wachtwoord ingevuld.";
    private const string RequiredUsernameWarning = "Je hebt nog geen e-mailadres ingevuld.";

    private AppiumWebElement UserNameInput => Driver.GetElement(By.XPath("(//*[@class='android.widget.EditText'])[1]"));
    private AppiumWebElement PasswordInput => Driver.GetElement(By.XPath("(//*[@class='android.widget.EditText'])[2]"));
    private AppiumWebElement InloggenButton => Driver.GetElement(By.XPath("//*[@text='Inloggen']"));

    public LoginScreen(AndroidDriver<AppiumWebElement> driver) : base(driver) { }

    public override bool Displayed()
    {
        return IsTextDisplayedOnScreen(LoginScreenWelcomeLocator) && UserNameInput.Displayed && PasswordInput.Displayed && InloggenButton.Displayed;
    }

    public MijnJumboTabScreen LogIn()
    {
        EnterUsername();
        EnterPassword();
        return InloggenButton.TapElement<MijnJumboTabScreen>(Driver);
    }

    public void EnterUsername(string username = null)
    {
        UserNameInput.EnterText(username ?? TestRunHelper.Username);
        Driver.HideKeyboard();
    }

    public void CleanUsernameField()
    {
        UserNameInput.TapElement();
        UserNameInput.Clear();
        Driver.HideKeyboard();
    }

    public void EnterPassword(string password = null)
    {
        PasswordInput.EnterText(password ?? TestRunHelper.Password);
        Driver.HideKeyboard();
    }

    public MijnJumboTabScreen TapInloggenButtonForLogin()
    {
        return InloggenButton.TapElement<MijnJumboTabScreen>(Driver);
    }

    public void TapInloggenButton()
    {
        InloggenButton.TapElement();
    }

    public bool UsernameRequiredWarningDisplayed()
    {
        return IsTextDisplayedOnScreen(RequiredUsernameWarning, timeLimitInSeconds: 2.5);
    }

    public bool PasswordRequiredWarningDisplayed()
    {
        return IsTextDisplayedOnScreen(RequiredPasswordWarning, timeLimitInSeconds: 2.5);
    }

    public bool RequiredValuesWarningDisplayed()
    {
        return IsTextDisplayedOnScreen(RequiredValuesWarning, timeLimitInSeconds: 2.5);
    }
}