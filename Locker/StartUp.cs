using System;
using System.Reflection;

namespace Locker
{
    public class StartUp
    {
        public static void SetStartup(bool enabled)
        {
            Microsoft.Win32.RegistryKey rkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey
                        ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (enabled)
                rkey.SetValue("CaptIt", "\"" + Assembly.GetEntryAssembly().Location + "\"");
            else
                rkey.DeleteValue("CaptIt", false);

            rkey.Close();
        }
    }
}
