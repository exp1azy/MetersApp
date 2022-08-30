using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetersApp.Data
{
    public class Indication
    {
        [Key]
        public int Id { get; set; }
        public int MeterId { get; set; }
        public DateTime Fixed { get; set; }
        public decimal Data { get; set; }
    }
}
