using System.ComponentModel.DataAnnotations;

namespace MLM.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        public string? Description { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
