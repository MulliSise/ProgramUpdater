using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgramUpdater
{
    public class ProgramComparer
    {
        public class ComparisonResult
        {
            public List<ProgramInfo> Novos = new List<ProgramInfo>();
            public List<ProgramInfo> Removidos = new List<ProgramInfo>();
            public List<(ProgramInfo Antigo, ProgramInfo Atual)> Alterados = new List<(ProgramInfo, ProgramInfo)>();
        }

        public ComparisonResult Compare(List<ProgramInfo> atual, List<ProgramInfo> salvo)
        {
            var resultado = new ComparisonResult();

            resultado.Novos = atual.Where(a => !salvo.Any(s => s.Name == a.Name)).ToList();
            resultado.Removidos = salvo.Where(s => !atual.Any(a => a.Name == s.Name)).ToList();

            foreach (var progAtual in atual)
            {
                var progSalvo = salvo.FirstOrDefault(s => s.Name == progAtual.Name);
                if (progSalvo != null && progSalvo.Version != progAtual.Version)
                    resultado.Alterados.Add((progSalvo, progAtual));
            }

            return resultado;
        }

        public void PrintResults(ComparisonResult resultado)
        {
            Console.WriteLine("\n=== Programas Novos ===");
            if (resultado.Novos.Count == 0)
                Console.WriteLine("Nenhum novo programa encontrado.");
            else
                resultado.Novos.ForEach(p => Console.WriteLine($"+ {p.Name} ({p.Version})"));

            Console.WriteLine("\n=== Programas Removidos ===");
            if (resultado.Removidos.Count == 0)
                Console.WriteLine("Nenhum programa removido.");
            else
                resultado.Removidos.ForEach(p => Console.WriteLine($"- {p.Name} ({p.Version})"));

            Console.WriteLine("\n=== Programas Atualizados ===");
            if (resultado.Alterados.Count == 0)
                Console.WriteLine("Nenhum programa atualizado.");
            else
                resultado.Alterados.ForEach(t => Console.WriteLine($"* {t.Antigo.Name}: {t.Antigo.Version} -> {t.Atual.Version}"));
        }
    }
}
