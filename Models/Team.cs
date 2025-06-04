using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Team : BaseMod
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Country { get; set; }
        [Required]
        public int? Phone { get; set; }
    }
}
