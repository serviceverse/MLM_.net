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

        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
