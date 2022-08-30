using System.ComponentModel.DataAnnotations;

namespace MetersApp.Data
{
    public class Resourse
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}