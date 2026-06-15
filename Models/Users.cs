using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100)]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [StringLength(255)]
        public string? Hash { get; set; }

        [StringLength(255)]
        public string? Salt { get; set; }

        [StringLength(20)]
        public string? ContactNo { get; set; }

        [NotMapped]
        public string? Password { get; set; }

        [StringLength(50)]
        public string? ReferralCode { get; set; }

        public int? RoleId { get; set; }
        
        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }

        public int? OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        public virtual Organization? Organization { get; set; }

        public virtual Wallet? Wallet { get; set; }

        public virtual ICollection<Mt5Account> Mt5Accounts { get; set; } = new List<Mt5Account>();

        public virtual ICollection<WalletTransaction> WalletTransactions { get; set; } = new List<WalletTransaction>();
        public virtual ICollection<Deposit> Deposits { get; set; } = new List<Deposit>();
        public virtual ICollection<Withdrawal> Withdrawals { get; set; } = new List<Withdrawal>();
        
        [InverseProperty("ApprovedUser")]
        public virtual ICollection<Withdrawal> ApprovedWithdrawals { get; set; } = new List<Withdrawal>();

        // Phase 3 Properties
        public virtual IB? IBRecord { get; set; }
        
        [InverseProperty("ApprovedByAdmin")]
        public virtual ICollection<IB> ApprovedIBs { get; set; } = new List<IB>();
        
        [InverseProperty("ParentUser")]
        public virtual ICollection<IB> ParentUserIBs { get; set; } = new List<IB>();

        public virtual ICollection<TradeHistory> TradeHistories { get; set; } = new List<TradeHistory>();
        public virtual ICollection<IbCommission> IbCommissions { get; set; } = new List<IbCommission>();
        public virtual ICollection<CommissionPayout> CommissionPayouts { get; set; } = new List<CommissionPayout>();
        
        [InverseProperty("Referrer")]
        public virtual ICollection<Referral> ReferralsMade { get; set; } = new List<Referral>();
        
        [InverseProperty("Referred")]
        public virtual ICollection<Referral> ReferralsReceived { get; set; } = new List<Referral>();

        [InverseProperty("Earner")]
        public virtual ICollection<ReferralCommission> EarnedCommissions { get; set; } = new List<ReferralCommission>();

        [InverseProperty("Generator")]
        public virtual ICollection<ReferralCommission> GeneratedCommissions { get; set; } = new List<ReferralCommission>();

        // Phase 4 Properties
        public virtual Kyc? KycRecord { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        // Phase 5 Properties
        public virtual ICollection<BankDetail> BankDetails { get; set; } = new List<BankDetail>();
    }
}
