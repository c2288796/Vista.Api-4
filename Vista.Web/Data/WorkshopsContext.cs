using Microsoft.EntityFrameworkCore;

namespace Vista.Web.Data
{
    public class WorkshopsContext : DbContext
    {
        public string DbPath { get; set; }
        public WorkshopsContext()
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "workshops.db");
        }

        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<WorkshopStaff> WorkshopStaff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkshopStaff>()
                .HasKey(ws => new { ws.WorkshopId, ws.StaffId });

            // Define many to many
            modelBuilder.Entity<WorkshopStaff>()
                .HasOne(ws => ws.Staff)
                .WithMany(ws => ws.Workshops)
                .HasForeignKey(ws => ws.StaffId);

            modelBuilder.Entity<WorkshopStaff>()
                .HasOne(sp => sp.Workshop)
                .WithMany(sp => sp.Staff)
                .HasForeignKey(sp => sp.WorkshopId);

        }
    }
}