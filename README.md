*Solution developed in Visual Studio 2022 v17.4.3 on Windows 10 (10.0.19045 Build 19045)

## Please prepare your environment before tests run: ##

0. Visual Studio or Visual Studio Build Tools **Download/Install:** https://visualstudio.microsoft.com/downloads/ 
1. Java SDK **Download/Install** https://www.oracle.com/java/technologies/downloads/
2. .NET 7 **Download/Install:** https://dotnet.microsoft.com/en-us/download/dotnet/7.0
3. NuGet.exe **Download/Install:** https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
4. Appium **Download/Install:** https://appium.io/docs/en/about-appium/getting-started/?lang=en#installing-appium
5. NodeJs **Download/Install:** https://nodejs.org/en/
6. Open PowerShell under Administrator and install npm using command '**npm install -g npm**'
7. Android Platform-Tools **Download:**  https://developer.android.com/studio/releases/platform-tools
8. Copy Android Platform-Tools to default folder (usually it's "C:\Users\{username}\AppData\Local\Android\Sdk") and add/check Environment Variables '**ANDROID_HOME**' (as mentioned folder "C:\Users\{username}\AppData\Local\Android\Sdk")
9. Add/check Environment Variable '**adb**' (usually it's "C:\Users\{username}\AppData\Local\Android\Sdk\platform-tools\adb.exe")
10. Add/check Environment Variable '**Path**' ("C:\Users\{username}\AppData\Local\Android\Sdk\platform-tools", "C:\Program Files\nodejs\", "C:\Users\{username}\AppData\Roaming\npm" )
11. Connect Android device with switched ON and authorized 'USB debugging' in 'Developer options' device settings. Install Jumbo app on the Android Device.

[Tests run through IDE] Please use **'jumbo.runsettings'** for run tests through IDE (e.g. in 'Visual Studio': menu "Test" -> "Configure Run Settings" -> "Select Solution Wide runsettings File" and select "jumbo.runsettings" which is located in project folder.
Then please run tests through 'Test Explorer' ('Visual Studio': menu "Test" -> "Test Explorer")

[Tests run through console] Please specify **'jumbo.runsettings'** for run tests using console (restore nuget packages and build solution before it):
In console: navigate to folder with artifacts after build (e.g. ...\JumboMobileTests\bin\Debug\net7.0>) then use command for run: "vstest.console.exe JumboMobileTests.dll /Settings:"jumbo.runsettings"

Test Automation Solution Description:
Framework based on/use: .NET 7 (C#), Specflow (for BDD/Gherkin usage), Appium (for Android device/app interaction), NUnit (for tests)
It has several layers: Features/tests, Business objects (App Screens, business logic aka step definitions), Utilities/Helpers.
Test run use jumbo.runsettings (sensitive parameters values which should be written during run through CI pipeline, it leaved by default for local/manual run)
jumbo.config has some kind of constant app values - android app package id and app activity id
Each test scenario tried to be independent (does not depend on run order - in scope of several/all or as a single)
It'd be great to have access to API for some pre/post conditions - in that case tests runs would be much safier/clear - in case of something would went wrong with data during previous runs
Scenarious detailed described in Gherkin language and imitates each user action/behavior with the app
During TAF development I tried to follow principles: SOLID, KISS, YAGNI, DRY, AAA
Several design pattern used: Page Object, Singleton, Factory Method, Strategy, Facade, Wrapper, Builder...
In case of any questions please feel free to email me (andrei.shendrykau@gmail.com)
