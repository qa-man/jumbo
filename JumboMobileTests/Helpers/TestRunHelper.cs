using NUnit.Framework;

namespace JumboMobileTests.Helpers
{
    public static class TestRunHelper
    {
        public static string Username => $"{TestContext.Parameters["username"]}";
        public static string Password => $"{TestContext.Parameters["password"]}";
    }
}