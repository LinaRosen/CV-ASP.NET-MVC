using CV_ASP.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace CV_ASP.NET.DataContext
{
    public class TestDataContext : DbContext
    {
        public TestDataContext(DbContextOptions<TestDataContext> options): base(options) { }

        public DbSet<Adress> Adresser { get; set; }
        public DbSet<Anvandare> Anvandare { get; set; }
        public DbSet<CV> CV { get; set; }
        public DbSet<CV_Erfarenhet> CV_Erfarenhet { get; set; }
        public DbSet<CV_kompetenser> CV_Kompetenser { get; set; }
        public DbSet<CV_Utbildning> CV_Utbildning { get; set; }
        public DbSet<Erfarenhet> Erfarenhet { get; set; }
        public DbSet<Kompetenser> Kompetenser { get; set; }
        public DbSet<Meddelande> Meddelande { get; set; }
        public DbSet<Utbildning> Utbildning { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meddelande>()
                .HasOne(m => m.Frananvandare)
                .WithMany(u => u.skickatMeddelande)
                .HasForeignKey(m => m.FranAnvandareId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Meddelande>()
                .HasOne(m => m.Tillanvandare)
                .WithMany(u => u.TagitEmotMeddelande)
                .HasForeignKey(m => m.TillAnvandareId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CV_kompetenser>()
                .HasKey(cc => new { cc.Cvid, cc.Kid });
            modelBuilder.Entity<CV_Utbildning>()
                .HasKey(ce => new { ce.CVid, ce.Uid });
            modelBuilder.Entity<CV_Erfarenhet>()
                .HasKey(cex => new { cex.Cvid, cex.Eid });
            modelBuilder.Entity<AnvProjekt>()
                .HasKey(up => new { up.Anvid, up.Pid });

        }

       

    }
}
