using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class ReferralCommission
    {
        [Key]
        public int Id { get; set; }

        public int ReferralId { get; set; }
        [ForeignKey("ReferralId")]
        public virtual Referral? Referral { get; set; }

        public int EarnerId { get; set; }
        [ForeignKey("EarnerId")]
        public virtual Users? Earner { get; set; }

        public int GeneratorId { get; set; }
        [ForeignKey("GeneratorId")]
        public virtual Users? Generator { get; set; }

        public int? TradeId { get; set; }
        public string? TransactionId { get; set; }

        [Required]
        public string CommissionType { get; set; } = null!; // TRADE, DEPOSIT, WITHDRAWAL, SIGNUP_BONUS

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Percentage { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? BaseAmount { get; set; }

        public string Status { get; set; } = "PENDING"; // PENDING, PAID, CANCELLED

        public int Level { get; set; } = 1;

        public DateTime? PaidAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
