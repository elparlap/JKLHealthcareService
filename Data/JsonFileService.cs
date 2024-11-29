using System.IO;
using Newtonsoft.Json;

public class JsonFileService<T>
{
    private readonly string _filePath;

    public JsonFileService(string fileName)
    {
        _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", fileName);
    }

    public List<T> Read()
    {
        if (!File.Exists(_filePath))
            return new List<T>(); // Return an empty list instead of null

        var jsonData = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>(); // Ensure no null is returned
    }

    public void Write(List<T> data)
    {
        var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(_filePath, jsonData);
    }
}
