using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Management;

namespace Test
{
    class Test
    {
        static void Main(string[] args)
        {
            string displayName;
            RegistryKey key;

            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string +
                  subkey.GetValue("InstallLocation") as string;
                Console.WriteLine(displayName);
            }
            Console.WriteLine( "ok");
            Console.ReadLine();
        }
    }
}
