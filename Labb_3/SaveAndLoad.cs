using Labb_3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Labb_3
{
    internal class SaveAndLoad
    {

        public async Task Save(List<QuestionPack> questionPacks, string fileName)
        {
           
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RobinLabb3");

            // Om mappen inte finns, skapa den
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Skapa den fullständiga sökvägen för filen
            string filePath = Path.Combine(directoryPath, fileName);

            // Serialisera QuestionPacks till JSON
            string json = JsonSerializer.Serialize(questionPacks, new JsonSerializerOptions { WriteIndented = true });

            // Skriv JSON till den skapade filen
            await File.WriteAllTextAsync(filePath, json);

            
        }

        // Här laddar jag in listan med questionPacks.
        public async Task<List<QuestionPack>> Load(string fileName)
        {
            // Hitta rätt mapp i AppData\Local och skapa filvägen till RobinLabb3-mappen
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RobinLabb3");
            string filePath = Path.Combine(directoryPath, fileName);

            // Om filen finns, läs den och deserialisera tillbaka till ett QuestionPack-objekt
            if (File.Exists(filePath))
            {
                // Läs av JSON-filen
                string json = await File.ReadAllTextAsync(filePath);

                // Deserialisera JSON till ett QuestionPack-objekt
                return JsonSerializer.Deserialize<List<QuestionPack>>(json);
            }

            // Dialogruta? "Filen kan inte hittas."
            return new List<QuestionPack>();
        }

    }

}