using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class Deposit
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }

        public int? Mt5AccountId { get; set; }
        [ForeignKey("Mt5AccountId")]
        public virtual Mt5Account? Account { get; set; }

        public DepositMode? Mode { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Amount { get; set; }

        public string? Note { get; set; }
        public string? TransactionId { get; set; }
        public string? Network { get; set; }
        public string? ProofUrl { get; set; }
        public string? Comment { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "PENDING"; // PENDING, APPROVED, REJECTED

        public int? ApprovedBy { get; set; } // Admin user ID

        public int? WalletTransactionId { get; set; }
        [ForeignKey("WalletTransactionId")]
        public virtual WalletTransaction? WalletTransaction { get; set; }

        public DepositType Type { get; set; } = DepositType.Wallet;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
