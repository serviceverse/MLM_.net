using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class WalletTransaction
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }

        public int WalletId { get; set; }
        [ForeignKey("WalletId")]
        public virtual Wallet? Wallet { get; set; }

        public int? AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Mt5Account? Account { get; set; }

        [Required]
        [StringLength(100)]
        public string Method { get; set; } = null!;

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Amount { get; set; }

        public string? Note { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "COMPLETED";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Deposit> Deposits { get; set; } = new List<Deposit>();
        public virtual ICollection<Withdrawal> Withdrawals { get; set; } = new List<Withdrawal>();
    }
}
