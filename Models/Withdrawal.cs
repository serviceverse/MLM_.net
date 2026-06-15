using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class Withdrawal
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }

        public int? Mt5AccountId { get; set; }
        [ForeignKey("Mt5AccountId")]
        public virtual Mt5Account? Account { get; set; }

        public string? Network { get; set; }
        
        [Required]
        public string WithdrawTo { get; set; } = null!; // USDT Address or method

        public WithdrawalMode? Mode { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Fee { get; set; } = 2.0m;

        public string? Note { get; set; }
        public string? Comment { get; set; }
        public string? TransactionId { get; set; }
        
        public bool Verified { get; set; } = false;

        public WithdrawalStatus Status { get; set; } = WithdrawalStatus.Pending;

        public int? ApprovedBy { get; set; }

        [ForeignKey("ApprovedBy")]
        public virtual Users? ApprovedUser { get; set; }

        public int? WalletTransactionId { get; set; }
        [ForeignKey("WalletTransactionId")]
        public virtual WalletTransaction? WalletTransaction { get; set; }

        public DepositType Type { get; set; } = DepositType.Wallet;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
