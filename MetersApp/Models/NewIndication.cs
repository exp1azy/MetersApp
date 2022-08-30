using MetersApp.Data;
using System.ComponentModel.DataAnnotations;

namespace MetersApp.Models
{
    public class NewIndication : IValidatableObject
    {
        public int MeterId { get; set; }
        public decimal Indication { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db = validationContext.GetService<DataContext>();
            List<ValidationResult> errors = new List<ValidationResult>();

            var lastIndication = db.Indication.Where(i => i.MeterId == MeterId).OrderByDescending(i => i.Fixed).First().Data;

            if (Indication < lastIndication)
            {
                errors.Add(new ValidationResult("Reading less than the last", new List<string>() { "Indication" }));
            }

            return errors;
        }
    }
}
