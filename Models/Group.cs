using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MLM.Models
{
    public class Group
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Code { get; set; } = null!;

        [Required]
        public string ManagerGroup { get; set; } = null!; // mt5 managerGroup name

        [Required]
        public string Leverage { get; set; } = null!;

        public string? Commission { get; set; }

        [Required]
        public string MinDeposit { get; set; } = null!;

        public string? Description { get; set; }
        public string? Range { get; set; }
        public string? SpreadRange { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<IbCommission> IbCommissions { get; set; } = new List<IbCommission>();
        public virtual ICollection<TradeHistory> TradeHistories { get; set; } = new List<TradeHistory>();
        public virtual ICollection<CommissionPayout> CommissionPayouts { get; set; } = new List<CommissionPayout>();
        public virtual ICollection<Mt5Account> Accounts { get; set; } = new List<Mt5Account>();
    }
}
