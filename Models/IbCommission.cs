using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class IbCommission
    {
        [Key]
        public int Id { get; set; }

        public int IbId { get; set; }
        [ForeignKey("IbId")]
        public virtual IB? IB { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }

        public string GroupId { get; set; } = null!;

        [Column(TypeName = "decimal(10, 4)")]
        public decimal Value { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
