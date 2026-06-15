using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class CommissionPayout
    {
        [Key]
        public int Id { get; set; }

        public int TradeId { get; set; }
        [ForeignKey("TradeId")]
        public virtual TradeHistory? Trade { get; set; }

        public int IbId { get; set; }
        [ForeignKey("IbId")]
        public virtual IB? IB { get; set; }

        public int Level { get; set; }

        [Column(TypeName = "decimal(10, 4)")]
        public decimal RateUsed { get; set; }

        [Column(TypeName = "decimal(12, 4)")]
        public decimal Lots { get; set; }

        [Column(TypeName = "decimal(14, 2)")]
        public decimal Amount { get; set; }

        public string? GroupId { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
