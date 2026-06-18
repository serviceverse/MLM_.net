using System;
using System.ComponentModel.DataAnnotations;

namespace MLM.Models
{
    public class ErrorLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Level { get; set; } = "Error"; // Info, Warning, Error

        [Required]
        [StringLength(500)]
        public string Title { get; set; } = null!;

        public string? StackTrace { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
