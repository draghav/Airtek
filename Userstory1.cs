public class Userstory1
{
    // In-memory data structure
    public static Dictionary<DayOfWeek, List<Flight>> schedule = new()
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

    public static void PrintSchedule()
    {  
        foreach (KeyValuePair<DayOfWeek, List<Flight>> kvp in schedule)
        {
            foreach (var flight in kvp.Value)
            {
                Console.WriteLine($"Flight: {flight.Id}, departure: {flight.Departure}, arrival: {flight.Arrival}, day: {kvp.Key}");                
            }
        }
    }

    public static Dictionary<DayOfWeek, List<Flight>> GetSchedule() => schedule;

    public class Flight
    {
        public string? Id { get; set; }
        public string? Departure { get; set; }
        public string? Arrival { get; set; }
        public int Orders { get; set; }
    }
}
