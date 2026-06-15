using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class NavigationActionPermission
    {
        [Key]
        public int Id { get; set; }

        public int ActionId { get; set; }

        [ForeignKey("ActionId")]
        public virtual NavigationAction? Action { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }
    }
}
