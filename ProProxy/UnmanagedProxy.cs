using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;

namespace ProProxy
{
    internal class UnmanagedProxy
    {
        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        private const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        private const int INTERNET_OPTION_REFRESH = 37;
        private static bool _settingsReturn;
        private static bool _refreshReturn;
        private static void SetSettingsReturn(bool value) => _settingsReturn = value;


        private static void SetRefreshReturn(bool value) => _refreshReturn = value;

        public static IProxy ChangeProxy(IProxy proxy)
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            registry.SetValue("ProxyEnable", 1);
            registry.SetValue("ProxyServer", proxy.ProxyHost);
            RefreshOS();
            return proxy;
        }

        public static void DisableProxy()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            registry.SetValue("ProxyEnable", 0);

            RefreshOS();
        }

        private static void RefreshOS()
        {
            SetSettingsReturn(InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0));
            SetRefreshReturn(InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0));
        }
    }
}