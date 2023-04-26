using Microsoft.Extensions.Logging;

public interface ISchedule
{
    void PrintSchedule();
    Dictionary<DayOfWeek, List<Flight>> Schedules { get; }
}

public class Schedule : ISchedule
{   
    Dictionary<DayOfWeek, List<Flight>> _schedules = new()
    {
        {
            DayOfWeek.Monday, new List<Flight>
            {
                new Flight { Id = "1", Departure = "YUL", Arrival = "YYZ" },
                new Flight { Id = "2", Departure = "YUL", Arrival = "YYC" },
                new Flight { Id = "3", Departure = "YUL", Arrival = "YVR" }
            }
        },
        {
            DayOfWeek.Tuesday, new List<Flight>
            {
                new Flight { Id = "4", Departure = "YUL", Arrival = "YYZ" },
                new Flight { Id = "5", Departure = "YUL", Arrival = "YYC" },
                new Flight { Id = "6", Departure = "YUL", Arrival = "YVR" }
            }
        }
    };

    ILogger<Schedule> _logger;
    public Schedule(ILogger<Schedule> logger)
    {
        _ = logger ?? throw new ArgumentNullException(nameof(logger));
        
        _logger = logger;
    }

    public Dictionary<DayOfWeek, List<Flight>> Schedules => _schedules;

    public void PrintSchedule()
    {
        foreach (KeyValuePair<DayOfWeek, List<Flight>> kvp in _schedules)
        {
            foreach (var flight in kvp.Value)
            {
                _logger.LogInformation($"Flight: {flight.Id}, departure: {flight.Departure}, arrival: {flight.Arrival}, day: {kvp.Key}");                
            }
        }
    }
}