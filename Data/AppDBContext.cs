using Microsoft.EntityFrameworkCore;

namespace MLM.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext>option):DbContext(option)
    {

        public DbSet<Models.Users> Users { get; set; } = null!;
        public DbSet<Models.Role> Roles { get; set; } = null!;
        public DbSet<Models.AppModule> AppModules { get; set; } = null!;
        public DbSet<Models.AppAction> AppActions { get; set; } = null!;
        public DbSet<Models.Permission> Permissions { get; set; } = null!;
    }
}
