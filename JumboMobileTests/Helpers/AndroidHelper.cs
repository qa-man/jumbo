using System;
using System.Collections.Generic;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;

namespace JumboMobileTests.Helpers;

public static class AndroidHelper
{
    public static AndroidDriver<AppiumWebElement> CurrentAndroidDriver { get { if (_currentAndroidDriver is null) InitializeAndroidDriver(); return _currentAndroidDriver; } }
    private static AndroidDriver<AppiumWebElement> _currentAndroidDriver;

    public static AndroidDriver<AppiumWebElement> InitializeAndroidDriver()
    {
        try
        {
            AppiumOptions options = new();
            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, ConfigHelper.AndroidPackage);
            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, ConfigHelper.AndroidActivity);
            options.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UIAutomator2");
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            options.AddAdditionalCapability("ensureWebviewsHavePages", true);
            options.AddAdditionalCapability("autoGrantPermissions", true);

            var serverOptions = new OptionCollector().AddArguments(new KeyValuePair<string, string>("--relaxed-security", string.Empty));
            var appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().WithArguments(serverOptions).Build();
            appiumLocalService.Start();
            return _currentAndroidDriver = new AndroidDriver<AppiumWebElement>(appiumLocalService, options, TimeSpan.FromMinutes(5));
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error '{exception.Message}' during AndroidDriver process initialization");
            throw;
        }
    }

    public static void DisposeAndroidDriver()
    {
        _currentAndroidDriver.CloseApp();
        _currentAndroidDriver.Dispose();
        _currentAndroidDriver = null;
    }

    public static void RestartAdbServer()
    {
        CommandHelper.RunCmdCommandAsAdmin($"{ConfigHelper.AdbPath} kill-server");
        CommandHelper.RunCmdCommandAsAdmin($"{ConfigHelper.AdbPath} start-server");
        CommandHelper.RunCmdCommandAsAdmin($"{ConfigHelper.AdbPath} devices");
    }
}