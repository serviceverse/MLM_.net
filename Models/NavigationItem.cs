using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class NavigationItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Key { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Route { get; set; } = null!;

        public int Sequence { get; set; }

        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual NavigationGroup? Group { get; set; }

        public ICollection<NavigationAction> Actions { get; set; } = new List<NavigationAction>();
        public ICollection<NavigationMenuPermission> Permissions { get; set; } = new List<NavigationMenuPermission>();
    }
}
