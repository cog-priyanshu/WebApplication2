using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Log
    {
        public int Id { get; set; }
        [Required]
        public string email { get; set; }
        public string password { get; set; }
        [Required]
        public int age { get; set; }

        public string? gender { get; set; }

    }
}
