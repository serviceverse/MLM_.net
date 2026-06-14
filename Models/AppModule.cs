using System.ComponentModel.DataAnnotations;

namespace MLM.Models
{
    public class AppModule
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Module Name is required")]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
