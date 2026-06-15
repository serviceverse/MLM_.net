using System.ComponentModel.DataAnnotations;

namespace MLM.Models
{
    public class NavigationGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string? Icon { get; set; }

        public int Sequence { get; set; }

        public ICollection<NavigationItem> Items { get; set; } = new List<NavigationItem>();
    }
}
