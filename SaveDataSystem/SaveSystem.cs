using System;
using System.IO;
using System.Text.Json;

namespace NEITGameEngine.SaveDataSystem
{
    
    public class SaveSystem
    {
        private string _saveFilePath;

        public SaveSystem(string fileName)
        {
            // Determine the file path based on the operating system
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _saveFilePath = Path.Combine(folderPath, fileName);
        }

        public void SaveGame(SaveData data)
        {
            try
            {
                // Serialize the SaveData object to JSON
                //string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
                //{
                //    WriteIndented = true // Optional for human-readable formatting
                //});

                // Register the custom converter
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                options.Converters.Add(new Vector2Converter());

                // Serialize the SaveData object to JSON
                string json = JsonSerializer.Serialize(data, options);

                // Write JSON to the file
                File.WriteAllText(_saveFilePath, json);

                Console.WriteLine("Game saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
            }
        }

        public SaveData LoadGame()
        {
            try
            {
                if (File.Exists(_saveFilePath))
                {
                    // Read JSON from the file
                    string json = File.ReadAllText(_saveFilePath);

                    // Deserialize the JSON back into a SaveData object
                    SaveData data = JsonSerializer.Deserialize<SaveData>(json);

                    Console.WriteLine("Game loaded successfully!");
                    return data;
                }
                else
                {
                    Console.WriteLine("Save file not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading game: {ex.Message}");
                return null;
            }
        }

        public void DeleteSave()
        {
            try
            {
                if (File.Exists(_saveFilePath))
                {
                    File.Delete(_saveFilePath);
                    Console.WriteLine("Save file deleted.");
                }
                else
                {
                    Console.WriteLine("No save file to delete.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting save file: {ex.Message}");
            }
        }
    }
}
