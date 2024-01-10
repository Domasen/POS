namespace API.ServicesComponent.Models;

public class DateModel
{
    public string DayName { get; set; }
    public List<DateItemModel> ActualDates { get; set; }
   
}

public partial class DateItemModel
{
    public DateTime ActualDate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}