using System.ComponentModel.DataAnnotations;

namespace MLM.Models
{
    public class Organization
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = null!;

        [StringLength(100)]
        public string Email { get; set; } = null!;
    }
}
