namespace MetersApp.ViewModels
{
    public class MeterPanelsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Value { get; set; }
        public decimal Delta { get; set; }
        public DateOnly Date { get; set; }
    }
}
