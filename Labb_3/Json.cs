using Labb_3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labb_3
{
    internal class Json
    {
        //public async Task Save(QuestionPack questionPack, string fileName)
        //{
        //    // Hitta rätt mapp i AppData\Local och skapa mappen RobinLabb3 om den inte finns
        //    string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RobinLabb3");

        //    // Om mappen inte finns, skapa den
        //    if (!Directory.Exists(directoryPath))
        //    {
        //        Directory.CreateDirectory(directoryPath);
        //    }

        //    // Skapa den fullständiga sökvägen för filen
        //    string filePath = Path.Combine(directoryPath, fileName);

        //    // Serialisera QuestionPack-objektet till en JSON-sträng
        //    string json = JsonSerializer.Serialize(questionPack, new JsonSerializerOptions { WriteIndented = true });

        //    // Skriv JSON-strängen till filen
        //    await File.WriteAllTextAsync(filePath, json);

        //    // Här kan en dialogruta visas som säger tex "Frågepaket sparat till fil."
        //}

        //// Asynkron metod för att läsa in ett QuestionPack från en JSON-fil
        //public async Task<QuestionPack> Load(string fileName)
        //{
        //    // Hitta rätt mapp i AppData\Local och skapa filvägen till RobinLabb3-mappen
        //    string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RobinLabb3");
        //    string filePath = Path.Combine(directoryPath, fileName);

        //    // Om filen finns, läs den och deserialisera tillbaka till ett QuestionPack-objekt
        //    if (File.Exists(filePath))
        //    {
        //        // Läs JSON-filen
        //        string json = await File.ReadAllTextAsync(filePath);

        //        // Deserialisera JSON till ett QuestionPack-objekt
        //        return JsonSerializer.Deserialize<QuestionPack>(json);
        //    }

        //    // Här kan en dialogruta visas som säger tex "Filen kan inte hittas."
        //    return null;
        //}
    }


}

