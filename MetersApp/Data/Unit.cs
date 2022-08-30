using System.ComponentModel.DataAnnotations;

namespace MetersApp.Data
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}