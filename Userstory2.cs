using System.Text.Json;

public class Userstory2
{
    const int MAX_ORDERS = 100;
    const int MAX_ORDERS_FLIGHT = 20;
    const string ORDERS_FILE = "coding-assigment-orders.json";

    public static void PrintOrders()
    {
        try
        {
            string ordersString = LoadJsonDocument();
            JsonDocument ordersJson = ParseJson(ordersString);

            for (int i = 1; i <= MAX_ORDERS; i++)
            {
                string destination = string.Empty;
                // add leading 0's to create orderNumber
                string orderNumber = string.Concat("order-", i.ToString("D3"));

                try
                {
                    destination = ordersJson.RootElement.GetProperty(orderNumber).GetProperty("destination").GetString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Order '{orderNumber}' not found in Json file. {ex.Message}");
                    continue;
                }

                ProcessOrder(orderNumber, destination);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An exception occured. {ex.Message}");
            return;
        }
    }

    private static void ProcessOrder(string orderNumber, string destination)
    {
        var schedule = Userstory1.GetSchedule();

        foreach (KeyValuePair<DayOfWeek, List<Userstory1.Flight>> kvp in schedule)
        {
            foreach (var flight in kvp.Value)
            {
                if (string.Equals(flight.Arrival, destination, StringComparison.OrdinalIgnoreCase))
                {
                    if (flight.Orders < MAX_ORDERS_FLIGHT)
                    {
                        flight.Orders++;
                        Console.WriteLine($"Order: {orderNumber}, flightNumber: {flight.Id}, departure: {flight.Departure}, arrival: {flight.Arrival}, day: {kvp.Key}");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Order: {orderNumber}, flightNumber: not scheduled");
                    }
                }
            }
        }
    }

    private static string LoadJsonDocument()
    {
        var options = new JsonDocumentOptions
        {
            AllowTrailingCommas = true,
            CommentHandling = JsonCommentHandling.Skip
        };

        string orders = string.Empty;

        try
        {
            orders = File.ReadAllText(ORDERS_FILE);
        }
        catch (Exception)
        {
            Console.WriteLine($"An error occured while loading Json document '{ORDERS_FILE}'.");
            throw;
        }

        return orders;
    }

    private static JsonDocument ParseJson(string json)
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

