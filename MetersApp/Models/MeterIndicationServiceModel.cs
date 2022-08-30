namespace MetersApp.Models
{
    public class MeterIndicationServiceModel
    {
        public int ResourceId { get; set; }
        public string ResourceTitle { get; set; }
        public decimal? Value { get; set; }
        public decimal Delta { get; set; }
        public DateOnly Date { get; set; }
    }
}