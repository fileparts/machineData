using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Win32;

namespace socket_client
{
    class software
    {
        //getmachinename
        public static string getMachineName()
        {
            return System.Environment.MachineName;
        }

        //getoperatingsystemversion
        public static string getOperatingSystem()
        {
            var os = System.Environment.OSVersion;
            string currentOS = null;

            switch (os.Platform)
            {
                case PlatformID.Win32S:
                    currentOS = "Windows 3.1";
                    break;
                case PlatformID.Win32Windows:
                    switch (os.Version.Minor)
                    {
                        case 0:
                            currentOS = "Windows 95";
                            break;
                        case 10:
                            currentOS = "Windows 98";
                            break;
                        default:
                            currentOS = "Unknown";
                            break;
                    }
                    break;
                case PlatformID.Win32NT:
                    switch (os.Version.Major)
                    {
                        case 3:
                            currentOS = "Windows NT 3.51";
                            break;
                        case 4:
                            currentOS = "Windows NT 4.0";
                            break;
                        case 5:
                            switch (os.Version.Minor)
                            {
                                case 0:
                                    currentOS = "Windows 2000";
                                    break;
                                case 1:
                                    currentOS = "Windows XP";
                                    break;
                                case 2:
                                    currentOS = "Windows 2003";
                                    break;
                                default:
                                    currentOS = "Unknown";
                                    break;
                            }
                            break;
                        case 6:
                            switch (os.Version.Minor)
                            {
                                case 0:
                                    currentOS = "Windows Vista";
                                    break;
                                case 1:
                                    currentOS = "Windows 7";
                                    break;
                                case 2:
                                    currentOS = "Windows 8";
                                    break;
                                case 3:
                                    currentOS = "Windows 8.1";
                                    break;
                                default:
                                    currentOS = "Unknown";
                                    break;
                            }
                            break;
                        case 10:
                            currentOS = "Windows 10";
                            break;
                        default:
                            currentOS = "Unknown";
                            break;
                    }
                    break;
                case PlatformID.WinCE:
                    currentOS = "Windows CE";
                    break;
                case PlatformID.Unix:
                    currentOS = "Unix";
                    break;
                default:
                    currentOS = "Unknown";
                    break;
            };
            return currentOS;
        }

        //getoperatingsystembit
        public static string getOperatingSystemBit()
        {
            string check = System.Environment.Is64BitOperatingSystem.ToString();
            string bit = string.Empty;

            if (check == "True")
            {
                bit = "64-bit";
            }
            else
            {
                bit = "32-bit";
            }
            return bit;
        }

        //getmacaddress
        public static string getMACAddress()
        {
            string host = Dns.GetHostName();
            string ip = Dns.GetHostEntry(host).AddressList[0].ToString();

            return ip;
        }

        //getipaddress
        public static string getIPAddress()
        {
            string host = Dns.GetHostName();
            int length = Dns.GetHostEntry(host).AddressList.Length;
            string ip = string.Empty;

            if (length == 2)
                ip = Dns.GetHostEntry(host).AddressList[1].ToString();
            else
                ip = Dns.GetHostEntry(host).AddressList[2].ToString();

            return ip;
        }

        //getcurrentuser
        public static string getCurrentUser()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            return userName;
        }

        //check if application is installed
        public static bool check(string app)
        {
            string displayName;
            RegistryKey key;

            //check current user
            key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            if (key != null)
            {
                foreach (string keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    if (displayName != null && displayName.ToUpper().Contains(app.ToUpper()))
                        return true;
                }
                key.Close();
            }

            //check windows 32
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            if (key != null)
            {
                foreach (string keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    if (displayName != null && displayName.ToUpper().Contains(app.ToUpper()))
                        return true;
                }
                key.Close();
            }

            //check windows 64
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            if (key != null)
            {
                foreach (string keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    if (displayName != null && displayName.ToUpper().Contains(app.ToUpper()))
                        return true;
                }
                key.Close();
            }
            return false;
        }

        //convert bytes
        public static string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;

            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
                dblSByte = bytes / 1024.0;

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }
    }
}
