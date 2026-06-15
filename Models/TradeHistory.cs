using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class TradeHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ExternalDealId { get; set; } = null!;

        public string? OrderId { get; set; }
        public string? PositionId { get; set; }

        [Required]
        public string AccountLogin { get; set; } = null!;

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }

        public string Symbol { get; set; } = null!;
        public string? GroupId { get; set; }

        [Column(TypeName = "decimal(12, 4)")]
        public decimal Lots { get; set; }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal? PriceOpen { get; set; }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal? PriceClose { get; set; }

        [Column(TypeName = "decimal(14, 2)")]
        public decimal? Profit { get; set; }

        [Column(TypeName = "decimal(14, 2)")]
        public decimal? CommissionFee { get; set; }

        [Column(TypeName = "decimal(14, 2)")]
        public decimal? Swap { get; set; }

        public DateTime ClosedAt { get; set; }
        public string? Entry { get; set; }
        public string? Comment { get; set; }
        public int? Magic { get; set; }
        public int? DurationSeconds { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
