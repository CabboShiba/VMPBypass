using HarmonyLib;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
namespace VMPBypass
{
    internal class Program
    {
        private static string AssemblyLoad = "";
        static void Main(string[] args)
        {
            try
            {
                AssemblyLoad = args[0];
            }
            catch(Exception ex)
            {
                while (!File.Exists(AssemblyLoad) || new FileInfo(AssemblyLoad).Extension != ".exe")
                {
                    Console.Clear();
                    Log("Please provide a valid executable File [.EXE]: ", "CONFIG", ConsoleColor.Cyan);
                    AssemblyLoad = Console.ReadLine();
                }
                Console.Clear();
            }
            try
            {
                object[] parameters = null;
                var assembly = Assembly.LoadFile(Path.GetFullPath(AssemblyLoad));
                var paraminfo = assembly.EntryPoint.GetParameters();
                parameters = new object[paraminfo.Length];
                Harmony patch = new Harmony("VMPRotectAntiDebuggerBypass_https://github.com/CabboShiba");
                patch.PatchAll(Assembly.GetExecutingAssembly());
                assembly.EntryPoint.Invoke(null, parameters);
            }
            catch (Exception ex)
            {
                Log($"Could not load {AssemblyLoad}\n{ex}", "ERROR", ConsoleColor.Red);
            }
            
            Console.ReadLine();
        }

        private static bool WinForm = false;
        public static void Log(string Data, string Type, ConsoleColor Color)
        {
            if (WinForm == true)
            {
                MessageBox.Show(Data, Type, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Console.ForegroundColor = Color;
                Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")} - {Type}] {Data}");
                Console.ResetColor();
            }
        }
    }
}
