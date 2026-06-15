using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }

        [Required]
        public string Category { get; set; } = null!;

        [Required]
        public string Title { get; set; } = null!;

        public string Priority { get; set; } = "MEDIUM"; // LOW | MEDIUM | HIGH

        [Required]
        public string Description { get; set; } = null!;

        public string Status { get; set; } = "OPEN"; // OPEN | CLOSED

        public string? DocUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
