using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MacroPad.Plugins.Nodes.VoiceMeeter
{
    internal static class Static
    {
        public static string Version = "0.0.0.0";
        public static VoiceMeeterType Type = 0;
        public static string[] StripsName = new string[0];
        public static string[] BusesName = new string[0];

        public static int PhysicalStripCount => Type switch
        {
            VoiceMeeterType.VoiceMeeter => 2,
            VoiceMeeterType.VoiceMeeterBanana => 3,
            VoiceMeeterType.VoiceMeeterPotato => 5,
            _ => 0,
        };
        public static int VirtualStripCount => Type switch
        {
            VoiceMeeterType.VoiceMeeter => 1,
            VoiceMeeterType.VoiceMeeterBanana => 2,
            VoiceMeeterType.VoiceMeeterPotato => 3,
            _ => 0,
        };
        public static int PhysicalBusCount => Type switch
        {
            VoiceMeeterType.VoiceMeeter => 1,
            VoiceMeeterType.VoiceMeeterBanana => 3,
            VoiceMeeterType.VoiceMeeterPotato => 5,
            _ => 0,
        };
        public static int VirtualBusCount => Type switch
        {
            VoiceMeeterType.VoiceMeeter => 1,
            VoiceMeeterType.VoiceMeeterBanana => 2,
            VoiceMeeterType.VoiceMeeterPotato => 3,
            _ => 0,
        };
        public static int StripCount => PhysicalStripCount + VirtualStripCount;
        public static int BusCount => PhysicalBusCount + VirtualBusCount;


        private static DateTime _lastUpdate = DateTime.Now.AddSeconds(-10);
        private static Timer? _timer;

        public static void Init()
        {
            if (TryGetVoiceMeeterDllPath(out string path)) NativeLibrary.Load(path);
            else return;

            var vmLogin = VoiceMeeterRemote.Login();
            if (vmLogin is not VoiceMeeterLoginResponse.OK and not VoiceMeeterLoginResponse.AlreadyLoggedIn) return;

            VoiceMeeterRemote.GetVoicemeeterVersion(out var version);

            var v1 = (version & 0xFF000000) >> 24;
            var v2 = (version & 0x00FF0000) >> 16;
            var v3 = (version & 0x0000FF00) >> 8;
            var v4 = version & 0x000000FF;

            Version = $"{v1}.{v2}.{v3}.{v4}";

            VoiceMeeterRemote.GetVoicemeeterType(out Type);
            
            StripsName = new string[StripCount];
            BusesName = new string[BusCount];

            Update();

            _timer = new Timer(new TimerCallback((object? state) =>
            {
                VoiceMeeterRemote.IsParametersDirty();
            }),null, 100,100);
        }
        public static void Update()
        {
            if (DateTime.Now.Subtract(_lastUpdate).TotalSeconds > 5)
            {
                _lastUpdate = DateTime.Now;

                UpdateBuses();
                UpdateStrips();
            }
        }
        public static void UpdateStrips()
        {
            int count = StripCount;
            for (int i = 0; i < count; i++)
            {
                UpdateStrip(i);
            }
        }
        public static void UpdateBuses()
        {
            int count = BusCount;
            for (int i = 0; i < count; i++)
            {
                UpdateBus(i);
            }
        }
        public static void UpdateStrip(int index)
        {
            string name = VoiceMeeterRemote.GetString($"Strip({index}).Label");
            StripsName[index] = string.IsNullOrWhiteSpace(name) ? $"Strip {index+1}" : name;

        }
        public static void UpdateBus(int index)
        {
            string name = VoiceMeeterRemote.GetString($"Bus({index}).Label");
            BusesName[index] = string.IsNullOrWhiteSpace(name) ? $"Bus {index + 1}" : name;
        }


        private static bool TryGetVoiceMeeterDllPath(out string path)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Find current version from the registry
                const string INSTALLED_PROGRAMS = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\VB:Voicemeeter {17359A74-1236-5467}";
                const string INSTALLED_PROGRAMS32 = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\VB:Voicemeeter {17359A74-1236-5467}";
                const string UNINSTALL_KEY = "UninstallString";

                path = string.Empty;

                using var voiceMeeterSubKey = Registry.LocalMachine.OpenSubKey(INSTALLED_PROGRAMS) ?? Registry.LocalMachine.OpenSubKey(INSTALLED_PROGRAMS32);
                if (voiceMeeterSubKey == null)
                    return false;

                var voiceMeeterPath = voiceMeeterSubKey.GetValue(UNINSTALL_KEY)?.ToString();
                if (string.IsNullOrWhiteSpace(voiceMeeterPath))
                    return false;

                path = Path.Combine(Path.GetDirectoryName(voiceMeeterPath)!, "VoicemeeterRemote64.dll");
                return File.Exists(path);
            }
            path = string.Empty;
            return false;
        }
    }
}
