using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace JumboMobileTests.Helpers
{
    public static class ConfigHelper
    {
        private static IConfigurationRoot Config => new ConfigurationBuilder().SetBasePath(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.FullName!).AddJsonFile("jumbo.json").Build();

        public static string AndroidPackage => Config["AndroidPackage"];
        public static string AndroidActivity => Config["AndroidActivity"];
        public static string AdbPath => @$"{Environment.GetEnvironmentVariable("ANDROID_HOME")}\platform-tools\adb.exe";
    }
}