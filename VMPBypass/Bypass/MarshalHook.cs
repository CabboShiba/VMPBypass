using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMPBypass.Bypass
{
    internal class MarshalHook
    {
        [HarmonyPatch(typeof(System.Runtime.InteropServices.Marshal), nameof(System.Runtime.InteropServices.Marshal.AllocHGlobal), new[] { typeof(int) })]
        class MarshalHook
        {
            [STAThread]
            static bool Prefix(int cb)
            {
                try
                {
                    cb--;
                }
                catch (Exception ex)
                {
                    Program.Log($"Error during AntiDBG Bypass:\n{ex.Message}", "ERROR", ConsoleColor.Red);
                    cb = 1;
                }
                return false;
            }
        }
    }
}
