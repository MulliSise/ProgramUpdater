using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProgramUpdater
{
    public class ProgramUpdateAuto
    {
        // Obtém a lista de versões online para programas instalados
        public async Task<List<ProgramUpdateOnline.OnlineProgramInfo>> GetOnlineProgramsAsync(List<ProgramInfo> installed)
        {
            var onlinePrograms = new List<ProgramUpdateOnline.OnlineProgramInfo>();

            foreach (var prog in installed)
            {
                string latestVersion = await GetLatestVersionWingetAsync(prog.Name);

                // Se não conseguiu obter via Winget, placeholder futuro para web scraping
                if (string.IsNullOrEmpty(latestVersion))
                {
                    latestVersion = await GetLatestVersionWebAsync(prog.Name);
                }

                if (!string.IsNullOrEmpty(latestVersion))
                {
                    onlinePrograms.Add(new ProgramUpdateOnline.OnlineProgramInfo
                    {
                        Name = prog.Name,
                        LatestVersion = latestVersion,
                        DownloadUrl = "" // opcional
                    });
                }
            }

            return onlinePrograms;
        }

        // Pega a versão mais recente usando winget
        private async Task<string> GetLatestVersionWingetAsync(string programName)
        {
            try
            {
                using Process process = new Process();
                process.StartInfo.FileName = "winget";
                process.StartInfo.Arguments = $"show \"{programName}\" --accept-source-agreements";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                string output = await process.StandardOutput.ReadToEndAsync();
                await process.WaitForExitAsync();

                var match = Regex.Match(output, @"Version:\s*(\S+)");
                if (match.Success)
                    return match.Groups[1].Value.Trim();
            }
            catch { }

            return null;
        }

        // Placeholder futuro: busca versão online via web scraping
        private async Task<string> GetLatestVersionWebAsync(string programName)
        {
            await Task.Delay(10); // placeholder
            return null;
        }
    }
}