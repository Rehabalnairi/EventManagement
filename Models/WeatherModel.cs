namespace EventManagement.Models
{
    public class WeatherResponse
    {
        public Hourly Hourly { get; set; } = null!;
    }

    public class Hourly
    {
        public List<double> Temperature_2m { get; set; } = new List<double>();
    }
}

