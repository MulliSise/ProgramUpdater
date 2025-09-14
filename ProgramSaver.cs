using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ProgramUpdater
{
    public class ProgramSaver
    {
        public static void SaveToJson(List<ProgramInfo> programas, string fileName)
        {
            string json = JsonSerializer.Serialize(programas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, json);
        }

        public static List<ProgramInfo> LoadFromJson(string fileName)
        {
            if (!File.Exists(fileName)) return new List<ProgramInfo>();
            string json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<ProgramInfo>>(json) ?? new List<ProgramInfo>();
        }
    }
}
