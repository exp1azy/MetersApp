namespace MetersApp.Models
{
    public class IndicationMeterServiceModel
    {
        public int MeterId { get; set; }
        public string MeterTitle { get; set; }
        public decimal? Value { get; set; }
        public decimal Delta { get; set; }
        public DateOnly Date { get; set; }
    }
}
