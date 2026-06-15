using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class NavigationMenuPermission
    {
        [Key]
        public int Id { get; set; }

        public int MenuId { get; set; }

        [ForeignKey("MenuId")]
        public virtual NavigationItem? Menu { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }
    }
}
