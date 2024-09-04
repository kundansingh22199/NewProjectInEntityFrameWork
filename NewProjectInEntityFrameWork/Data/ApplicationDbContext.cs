using Microsoft.EntityFrameworkCore;
using NewProjectInEntityFrameWork.Models;

namespace NewProjectInEntityFrameWork.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ModDashboard> Dashboard { get; set; }
        public DbSet<Tbl_AppMst> appmst { get; set; }
        public DbSet<ModRegistrationDetails> user { get; set; }
        public DbSet<ModTeamDetails> myteam { get; set; }
        public DbSet<ModDirectTeamDetails> direct { get; set; }
        public DbSet<modFundrequestRpt> fund { get; set; }
        public DbSet<modUsdtRequest> fundusdt { get; set; }
        public DbSet<modFundReceive> fundreceive { get; set; }
        public DbSet<topupbal> topupbal { get; set; }
        public DbSet<ModPassword> pass { get; set; }
        public DbSet<topupreport> topup { get; set; }
        public DbSet<modKYC> kyc { get; set; }
        public DbSet<Withdrawreport> withdraw { get; set; }
        public DbSet<ModDirectIncome> directincome { get; set; }
        public DbSet<ModLevelIncome> levelincome { get; set; }
        public DbSet<ModRoiIncome> roiincome { get; set; }
        public DbSet<modCompose> mail { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tbl_AppMst>()
                .HasNoKey()
                .ToTable("Tbl_Appmst"); 
            modelBuilder.Entity<ModDashboard>()
                .HasNoKey()
                .ToView(null);
            modelBuilder.Entity<ModRegistrationDetails>()
               .HasNoKey()
               .ToView(null);
            modelBuilder.Entity<ModTeamDetails>()
               .HasNoKey()
               .ToView(null);
            modelBuilder.Entity<ModDirectTeamDetails>()
               .HasNoKey()
               .ToView(null);
            modelBuilder.Entity<modFundrequestRpt>()
               .HasNoKey()
               .ToView(null);
            modelBuilder.Entity<modUsdtRequest>()
               .HasNoKey()
               .ToView(null);
            modelBuilder.Entity<modFundReceive>()
               .HasNoKey()
               .ToView(null);
            modelBuilder.Entity<topupreport>()
               .HasNoKey()
               .ToView(null);
            modelBuilder.Entity<topupbal>()
               .HasNoKey()
               .ToView(null);
            modelBuilder.Entity<ModPassword>()
               .HasNoKey()
               .ToView(null);
            modelBuilder.Entity<modKYC>()
               .HasNoKey()
               .ToView(null);
            modelBuilder.Entity<Withdrawreport>()
               .HasNoKey()
               .ToView(null);
            modelBuilder.Entity<ModDirectIncome>()
               .HasNoKey()
               .ToView(null);
            modelBuilder.Entity<ModLevelIncome>()
               .HasNoKey()
               .ToView(null);
            modelBuilder.Entity<ModRoiIncome>()
               .HasNoKey()
               .ToView(null);
            modelBuilder.Entity<modCompose>()
               .HasNoKey()
               .ToView(null);
        }
    }
}
