using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class NavigationAction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Key { get; set; } = null!;

        public int ItemId { get; set; }

        [ForeignKey("ItemId")]
        public virtual NavigationItem? Item { get; set; }

        public ICollection<NavigationActionPermission> Permissions { get; set; } = new List<NavigationActionPermission>();
    }
}
