using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class Referral
    {
        [Key]
        public int Id { get; set; }

        public int ReferrerId { get; set; }
        [ForeignKey("ReferrerId")]
        public virtual Users? Referrer { get; set; }

        public int ReferredId { get; set; }
        [ForeignKey("ReferredId")]
        public virtual Users? Referred { get; set; }

        public int Level { get; set; } = 1;

        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<ReferralCommission> Commissions { get; set; } = new List<ReferralCommission>();
    }
}
