using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLM.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }

        public int AppModuleId { get; set; }
        [ForeignKey("AppModuleId")]
        public virtual AppModule? AppModule { get; set; }

        public int AppActionId { get; set; }
        [ForeignKey("AppActionId")]
        public virtual AppAction? AppAction { get; set; }
    }
}
