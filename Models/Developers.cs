using System.ComponentModel.DataAnnotations;

namespace projectbridgemvc.Models
{
    public class Developers
    {
        [Key]
        public int DevelopersId { get; set; }
        public string? DeveloperName { get; set; }

        public DateTime FoundationDate { get; set; }

        public int DeveloperValue { get; set; }
    }
}
