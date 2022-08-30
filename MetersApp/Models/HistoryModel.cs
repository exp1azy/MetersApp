namespace MetersApp.Models
{
    public class HistoryModel
    {
        public string MeterTitle { get; set; }
        public int MeterId { get; set; }
        public DateOnly Fixed { get; set; }
        public decimal Data { get; set; }
    }
}
