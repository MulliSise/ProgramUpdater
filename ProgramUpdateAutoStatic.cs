using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProgramUpdater
{
    public static class ProgramUpdateAutoStatic
    {
        public static async Task<bool> InstallProgramAsync(string programName)
        {
            try
            {
                using var process = new Process();
                process.StartInfo.FileName = "winget";
                process.StartInfo.Arguments = $"upgrade \"{programName}\" --accept-source-agreements --accept-package-agreements";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (!string.IsNullOrWhiteSpace(error) && error.Contains("Nenhum pacote instalado"))
                {
                    Console.WriteLine($"Falha ao atualizar {programName}: pacote não encontrado.");
                    return false;
                }

                if (process.ExitCode == 0)
                {
                    Console.WriteLine($"Programa {programName} atualizado com sucesso.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Falha ao atualizar {programName}: código de saída {process.ExitCode}");
                    if (!string.IsNullOrWhiteSpace(error))
                        Console.WriteLine("Erro: " + error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Falha ao atualizar {programName}: {ex.Message}");
                return false;
            }
        }
    }
}