using System;
using System.Diagnostics;

namespace JumboMobileTests.Helpers
{
    public static class CommandHelper
    {
        public static void RunCmdCommandAsAdmin(string cmdStr)
        {
            try
            {
                var processInfo = new ProcessStartInfo("cmd.exe", "/S /C " + cmdStr)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Verb = "runas",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                var process = new Process
                {
                    StartInfo = processInfo
                };
                process.Start();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"'{cmdStr}' command execution throws exception - '{ex.Message}'");
            }
        }
    }

}
