using System;
using System.Collections.Generic;

namespace ProgramUpdater
{
    public class ProgramUpdateOnline
    {
        public class OnlineProgramInfo
        {
            public string Name { get; set; }
            public string LatestVersion { get; set; }
            public string DownloadUrl { get; set; }
        }

        private readonly List<OnlineProgramInfo> onlinePrograms;

        public ProgramUpdateOnline(List<OnlineProgramInfo> onlinePrograms)
        {
            this.onlinePrograms = onlinePrograms ?? new List<OnlineProgramInfo>();
        }

        public List<(ProgramInfo Local, OnlineProgramInfo Online)> GetOutdatedPrograms(List<ProgramInfo> installed)
        {
            var outdated = new List<(ProgramInfo, OnlineProgramInfo)>();
            foreach (var prog in installed)
            {
                var onlineProg = onlinePrograms.Find(p => p.Name.Equals(prog.Name, StringComparison.OrdinalIgnoreCase));
                if (onlineProg != null && !string.Equals(prog.Version, onlineProg.LatestVersion))
                    outdated.Add((prog, onlineProg));
            }
            return outdated;
        }

        public void PrintOutdatedPrograms(List<(ProgramInfo Local, OnlineProgramInfo Online)> outdated)
        {
            if (outdated.Count == 0)
            {
                Console.WriteLine("\nTodos os programas estão atualizados.");
                return;
            }

            Console.WriteLine("\n=== Programas Desatualizados ===");
            foreach (var (local, online) in outdated)
            {
                Console.WriteLine($"* {local.Name}: {local.Version} -> {online.LatestVersion}" +
                                  (string.IsNullOrEmpty(online.DownloadUrl) ? "" : $" (Update: {online.DownloadUrl})"));
            }
        }
    }
}
