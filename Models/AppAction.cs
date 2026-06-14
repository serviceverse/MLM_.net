using System.ComponentModel.DataAnnotations;

namespace MLM.Models
{
    public class AppAction
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Action Name is required")]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
