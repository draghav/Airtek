using System.Text.Json;

public interface ILoadOrder
{
    string LoadJsonDocument(string path);
    JsonDocument ParseJsonFile(string json);
}

public class LoadOrder : ILoadOrder
{
    public string LoadJsonDocument(string path)
    {
        var options = new JsonDocumentOptions
        {
            AllowTrailingCommas = true,
            CommentHandling = JsonCommentHandling.Skip
        };

        string orders = string.Empty;

        try
        {
            orders = File.ReadAllText(path);
        }
        catch (Exception)
        {
            Console.WriteLine($"An error occured while loading Json document '{path}'.");
            throw;
        }

        return orders;
    }

    public JsonDocument ParseJsonFile(string json)
    {
        JsonDocument ordersJson;

        try
        {
            ordersJson = JsonDocument.Parse(json);
        }
        catch (JsonException)
        {
            Console.WriteLine($"An error occured while parsing Json document.");
            throw;
        }

        return ordersJson;
    }
}