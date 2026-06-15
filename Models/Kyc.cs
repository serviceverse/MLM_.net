using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class Kyc
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }

        public string? PoaUrl { get; set; }
        public string? PoiUrl { get; set; }

        public IbStatus PoaStatus { get; set; } = IbStatus.Pending; // Using IbStatus enum (Pending, Approved, Rejected)
        public IbStatus PoiStatus { get; set; } = IbStatus.Pending;

        public string? Reason { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
