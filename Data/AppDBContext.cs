using Microsoft.EntityFrameworkCore;
using MLM.Models;

namespace MLM.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext> option) : DbContext(option)
    {
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Organization> Organizations { get; set; } = null!;
        public DbSet<NavigationGroup> NavigationGroups { get; set; } = null!;
        public DbSet<NavigationItem> NavigationItems { get; set; } = null!;
        public DbSet<NavigationAction> NavigationActions { get; set; } = null!;
        public DbSet<NavigationMenuPermission> NavigationMenuPermissions { get; set; } = null!;
        public DbSet<NavigationActionPermission> NavigationActionPermissions { get; set; } = null!;
        public DbSet<Wallet> Wallets { get; set; } = null!;
        public DbSet<Mt5Account> Mt5Accounts { get; set; } = null!;
        public DbSet<WalletTransaction> WalletTransactions { get; set; } = null!;
        public DbSet<Deposit> Deposits { get; set; } = null!;
        public DbSet<Withdrawal> Withdrawals { get; set; } = null!;
        public DbSet<IB> IBs { get; set; } = null!;
        public DbSet<TradeHistory> TradeHistories { get; set; } = null!;
        public DbSet<IbCommission> IbCommissions { get; set; } = null!;
        public DbSet<CommissionPayout> CommissionPayouts { get; set; } = null!;
        public DbSet<Referral> Referrals { get; set; } = null!;
        public DbSet<ReferralCommission> ReferralCommissions { get; set; } = null!;
        public DbSet<Kyc> Kycs { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<BankDetail> BankDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Globally disable cascade delete to prevent multiple cascade paths in SQL Server
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
