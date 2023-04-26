using System.Text.Json;
using Microsoft.Extensions.Logging;

public interface IOrder
{
    void PrintOrders();
}

public class Order : IOrder
{
    const int MAX_ORDERS = 100;
    const int MAX_ORDERS_FLIGHT = 20;
    const string ORDERS_FILE = "coding-assigment-orders.json";
    ILoadOrder _loader;
    ISchedule _schedule;
    ILogger<Order> _logger;
    public Order(ILoadOrder loader, ISchedule schedule, ILogger<Order> logger)
    {
        _ = loader ?? throw new ArgumentNullException(nameof(loader));
        _ = schedule ?? throw new ArgumentNullException(nameof(schedule));
        _ = logger ?? throw new ArgumentNullException(nameof(logger));

        _loader = loader;
        _schedule = schedule;
        _logger = logger;
    }

    public void PrintOrders()
    {
        try
        {
            string ordersString = _loader.LoadJsonDocument(ORDERS_FILE);
            JsonDocument ordersJson = _loader.ParseJsonFile(ordersString);

            for (int i = 1; i <= MAX_ORDERS; i++)
            {
                string destination = string.Empty;
                // add leading 0's to create orderNumber
                string orderNumber = string.Concat("order-", i.ToString("D3"));

                try
                {
                    destination = ordersJson.RootElement.GetProperty(orderNumber).GetProperty("destination").GetString()!;
                }
                catch (Exception ex)
                {
                    // log the exception and continue with remaining orders
                    _logger.LogError($"Order '{orderNumber}' not found in Json file. {ex.Message}");

                    continue;
                }

                ProcessOrder(orderNumber, destination);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"An exception occured. {ex.Message}");
            return;
        }
    }

    private void ProcessOrder(string orderNumber, string destination)
    {
        var schedule = _schedule.Schedules;

        foreach (KeyValuePair<DayOfWeek, List<Flight>> kvp in schedule)
        {
            foreach (var flight in kvp.Value)
            {
                if (string.Equals(flight.Arrival, destination, StringComparison.OrdinalIgnoreCase))
                {
                    if (flight.Orders < MAX_ORDERS_FLIGHT)
                    {
                        flight.Orders++;
                        _logger.LogInformation($"Order: {orderNumber}, flightNumber: {flight.Id}, departure: {flight.Departure}, arrival: {flight.Arrival}, day: {kvp.Key}");

                        return;
                    }
                    else
                    {
                        _logger.LogWarning($"Order: {orderNumber}, flightNumber: not scheduled");
                    }
                }
            }
        }
    }
}

