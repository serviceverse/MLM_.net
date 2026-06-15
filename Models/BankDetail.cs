using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class BankDetail
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }

        [Required]
        public string AccountName { get; set; } = null!;

        [Required]
        public string AccountNo { get; set; } = null!;

        [Required]
        public string IfscCode { get; set; } = null!;

        public string? Iban { get; set; }

        [Required]
        public string BankName { get; set; } = null!;

        public string? BankAddress { get; set; }

        [Required]
        public string Country { get; set; } = null!;

        [Required]
        public string BookBankUrl { get; set; } = null!;

        public string Status { get; set; } = "PENDING";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
