using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class Mt5Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }

        [Required]
        [StringLength(100)]
        public string Mt5Id { get; set; } = null!;

        [StringLength(100)]
        public string? DisplayId { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountType { get; set; } = null!;

        [StringLength(50)]
        public string? GroupId { get; set; }

        [StringLength(50)]
        public string? Leverage { get; set; }

        [Required]
        [StringLength(255)]
        public string MainPassword { get; set; } = null!;

        [StringLength(255)]
        public string? InvestorPassword { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Balance { get; set; } = 0;

        [StringLength(100)]
        public string Server { get; set; } = "CRIB";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
