using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramUpdater
{
    internal class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("A fazer scan de programas instalados...");
            var programasAtuais = ProgramScanner.ListInstalledPrograms();
            Console.WriteLine($"Foram encontrados {programasAtuais.Count} programas.\n");

            var programasSalvos = ProgramSaver.LoadFromJson("programas.json");

            var comparer = new ProgramComparer();
            var resultado = comparer.Compare(programasAtuais, programasSalvos);

            // Mostrar resultados
            comparer.PrintResults(resultado);

            // Perguntar se quer atualizar o JSON
            Console.Write("\nDeseja atualizar a lista de programas no JSON? (s/n): ");
            var inputJson = Console.ReadLine();
            if (inputJson.Trim().ToLower() == "s")
            {
                ProgramSaver.SaveToJson(programasAtuais, "programas.json");
                Console.WriteLine("Lista de programas atualizada no JSON.\n");
            }

            // Atualizações de programas alterados
            if (resultado.Alterados.Count == 0)
            {
                Console.WriteLine("Não existem programas desatualizados para atualizar.");
            }
            else
            {
                foreach (var (antigo, atual) in resultado.Alterados)
                {
                    Console.Write($"\nQueres atualizar {antigo.Name} de {antigo.Version} para {atual.Version}? (s/n): ");
                    var input = Console.ReadLine();
                    if (input.Trim().ToLower() == "s")
                    {
                        Console.WriteLine($"A atualizar {antigo.Name}...");

                        bool sucesso = await ProgramUpdateAutoStatic.InstallProgramAsync(atual.Name);

                        if (sucesso)
                            Console.WriteLine($"{atual.Name} atualizado com sucesso!");
                        else
                            Console.WriteLine($"Falha ao atualizar {atual.Name}.");
                    }
                    else
                    {
                        Console.WriteLine($"Atualização de {antigo.Name} ignorada.");
                    }
                }
            }

            Console.WriteLine("\nProcesso concluído. Pressiona qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}