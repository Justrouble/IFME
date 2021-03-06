﻿using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Principal;

namespace ifme
{
    internal class Elevated
    {
        private static bool IsElevated { get; set; }

        internal static bool IsAdmin
        {
            get
            {
                using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
                {
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    IsElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
                }

                return IsElevated;
            }
        }

        internal static void RunAsAdmin()
        {
            if (IsAdmin == false)
            {
                var startInfo = new ProcessStartInfo(Get.AppPath)
                {
                    Verb = "runas"
                };

                Process.Start(startInfo);

                Application.Exit();

                return;
            }
        }

        internal static void RunAsAdmin(string projectFile)
        {
            if (IsAdmin == false)
            {
                var startInfo = new ProcessStartInfo(Get.AppPath)
                {
                    Verb = "runas",
                    Arguments = $"-s -i \"{projectFile}\""
                };

                Process.Start(startInfo);

                Application.Exit();

                return;
            }
        }
    }
}
