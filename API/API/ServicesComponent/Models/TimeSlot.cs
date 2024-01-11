using System.Text.Json.Serialization;

namespace API.ServicesComponent.Models;

public class TimeSlot
{
    // public TimeOnly StartTime { get; set; }
    // public TimeOnly EndTime { get; set; }
    
    [JsonIgnore]
    public TimeOnly StartTime { get; set; }

    public string StartTimeSerialized
    {
        get => StartTime.ToString();
        set => StartTime = TimeOnly.Parse(value);
    }
    
    [JsonIgnore]
    public TimeOnly EndTime { get; set; }

    public string EndTimeSerialized
    {
        get => EndTime.ToString();
        set => EndTime = TimeOnly.Parse(value);
    }
}