using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Helper
{
    public class WindowSoftHelper
    {
        /// <summary>
        /// 用于判断windows电脑中有无安装指定程序，名字是*.exe
        /// </summary>
        /// <param name="softName"></param>
        /// <returns></returns>
        [SupportedOSPlatform("windows")]
        public static bool IsTheSoft(string softName)
        {
            if (SoftIsInLocalMachine(softName))
            {
                return true;
            }
            else if (SoftIsInUsers(softName))
            {
                return true;
            }
            return false;
        }

        [SupportedOSPlatform("windows")]
        public static bool SoftIsInLocalMachine(string softName)
        {
            RegistryKey regKey = Registry.LocalMachine;
            RegistryKey regSubKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\", false)!;
            if (regSubKey != null)
                foreach (string keyName in regSubKey.GetSubKeyNames())
                {
                    if (keyName.Contains(softName))
                    {
                        return true;
                    }
                }

            return false;
        }
        [SupportedOSPlatform("windows")]
        public static bool SoftIsInUsers(string softName)
        {
            RegistryKey regKey = Registry.Users;
            foreach (string keyName in regKey.GetSubKeyNames())
            {
                if (keyName.ToLower().Contains("classes"))
                {
                    string subKeyName = keyName.Substring(0, keyName.Length - 8);
                    RegistryKey regSubKey = regKey.OpenSubKey(subKeyName + @"\Software\Microsoft\Windows\CurrentVersion\App Paths\", false)!;
                    if (regSubKey != null)
                    {
                        foreach (string subName in regSubKey.GetSubKeyNames())
                        {
                            if (subName.Contains(softName))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
