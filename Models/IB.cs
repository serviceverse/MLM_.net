using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class IB
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }

        public IbStatus Status { get; set; } = IbStatus.Pending;

        public int? Level { get; set; } = 1;

        public string? Note { get; set; }

        public int? ApprovedBy { get; set; }
        [ForeignKey("ApprovedBy")]
        public virtual Users? ApprovedByAdmin { get; set; }

        public double? TotalVolume { get; set; } = 0;
        public int? ReferredCount { get; set; } = 0;

        public int? ParentUserId { get; set; }
        [ForeignKey("ParentUserId")]
        public virtual Users? ParentUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<IbCommission> IbCommissions { get; set; } = new List<IbCommission>();
        public virtual ICollection<CommissionPayout> CommissionPayouts { get; set; } = new List<CommissionPayout>();
    }
}
