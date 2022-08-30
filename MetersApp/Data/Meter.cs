using System.ComponentModel.DataAnnotations;

namespace MetersApp.Data
{
    public class Meter
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal MaxValue { get; set; }
        public int DecimalPlaces { get; set; }
        public int UnitId { get; set; }
        public int ResourceId { get; set; }

        virtual public Resourse Resource { get; set; }
    }
}