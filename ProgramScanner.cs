using System.Collections.Generic;
using Microsoft.Win32;

namespace ProgramUpdater
{
    public class ProgramScanner
    {
        public static List<ProgramInfo> ListInstalledPrograms()
        {
            var programas = new List<ProgramInfo>();
            programas.AddRange(ReadRegistry(Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"));
            programas.AddRange(ReadRegistry(Registry.CurrentUser, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"));
            return programas;
        }

        private static List<ProgramInfo> ReadRegistry(RegistryKey root, string subKey)
        {
            var lista = new List<ProgramInfo>();
            using (RegistryKey key = root.OpenSubKey(subKey))
            {
                if (key == null) return lista;
                foreach (var subkeyName in key.GetSubKeyNames())
                {
                    using (var subkey = key.OpenSubKey(subkeyName))
                    {
                        string displayName = subkey.GetValue("DisplayName") as string;
                        string displayVersion = subkey.GetValue("DisplayVersion") as string;
                        if (!string.IsNullOrEmpty(displayName))
                            lista.Add(new ProgramInfo { Name = displayName, Version = displayVersion ?? "Desconhecida" });
                    }
                }
            }
            return lista;
        }
    }
}
