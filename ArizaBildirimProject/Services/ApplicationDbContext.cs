using ArizaBildirimProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ArizaBildirimProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aktivite> Aktiviteler { get; set; }
        public DbSet<ArizaKisaTanim> ArizaKisaTanimlar { get; set; }
        public DbSet<ArizaTur> ArizaTurler { get; set; }
        public DbSet<Bolum> Bolum { get; set; }
        public DbSet<Cihaz> Cihaz { get; set; }
        public DbSet<Departman> Departman { get; set; }
        public DbSet<Rapor> Rapor { get; set; }
        public DbSet<Role> Roller { get; set; }
        public DbSet<Statu> Statuler { get; set; }
        public DbSet<Teshis> Teshisler { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary key configurations
            modelBuilder.Entity<Aktivite>().HasKey(a => a.Id);
            modelBuilder.Entity<ArizaKisaTanim>().HasKey(a => a.Id);
            modelBuilder.Entity<ArizaTur>().HasKey(a => a.Id);
            modelBuilder.Entity<Bolum>().HasKey(b => b.Id);
            modelBuilder.Entity<Cihaz>().HasKey(c => c.Id);
            modelBuilder.Entity<Departman>().HasKey(d => d.Id);
            modelBuilder.Entity<Rapor>().HasKey(r => r.Id);
            modelBuilder.Entity<Role>().HasKey(r => r.Id);
            modelBuilder.Entity<Statu>().HasKey(s => s.Id);
            modelBuilder.Entity<Teshis>().HasKey(t => t.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            // Relationships
            modelBuilder.Entity<Bolum>()
                .HasOne(b => b.Departman)
                .WithMany(d => d.Bolumler)
                .HasForeignKey(b => b.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cihaz>()
                .HasOne(c => c.Bolum)
                .WithMany(b => b.Cihazs)
                .HasForeignKey(c => c.BolumId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rapor>()
                .HasOne(r => r.Departman)
                .WithMany(d => d.Raporlar)
                .HasForeignKey(r => r.DepartmanId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rapor>()
                .HasOne(r => r.Bolum)
                .WithMany(b => b.Raporlar)
                .HasForeignKey(r => r.BolumId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rapor>()
                .HasOne(r => r.Cihaz)
                .WithMany(c => c.Raporlar)
                .HasForeignKey(r => r.CihazId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rapor>()
                .HasOne(r => r.ArizaKisaTanim)
                .WithMany(akt => akt.Raporlar)
                .HasForeignKey(r => r.ArizaKisaTanimId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rapor>()
                .HasOne(r => r.ArizaTur)
                .WithMany(at => at.Raporlar)
                .HasForeignKey(r => r.ArizaTurId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Departman)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmanId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<ArizaKisaTanim>()
                .HasOne(akt => akt.ArizaTur)
                .WithMany(at => at.ArizaKisaTanimlar)
                .HasForeignKey(akt => akt.ArizaTurId);


        }
    }
}
