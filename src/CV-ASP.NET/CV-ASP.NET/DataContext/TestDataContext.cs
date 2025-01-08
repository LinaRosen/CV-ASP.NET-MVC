using CV_ASP.NET.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CV_ASP.NET.DataContext
{
    public class TestDataContext : IdentityDbContext<Anvandare>
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
        public DbSet<Projekt> Projekt { get; set; }
        public DbSet<AnvProjekt> AnvProjekt { get; set; } 

        public DbSet<Erfarenhet> erfarenhets { get; set; }  
        public DbSet<Utbildning> utbildnings { get; set; }
        public DbSet<Kompetenser> kompetensers { get; set; }





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


            // CV_kompetenser konfiguration
            modelBuilder.Entity<CV_kompetenser>()
            .HasKey(ck => new { ck.Cvid, ck.Kid });

            modelBuilder.Entity<CV_kompetenser>()
                .HasOne(ck => ck.CV)
                .WithMany(cv => cv.CvKompetenser)
                .HasForeignKey(ck => ck.Cvid);

            modelBuilder.Entity<CV_kompetenser>()
                .HasOne(ck => ck.Kompetenser)
                .WithMany(k => k.CV_kompetenser)
                .HasForeignKey(ck => ck.Kid);

            // CV_Utbildning konfiguration
            modelBuilder.Entity<CV_Utbildning>()
                .HasKey(ce => new { ce.CVid, ce.Uid });

            modelBuilder.Entity<CV_Utbildning>()
                .HasOne(ce => ce.cv)
                .WithMany(cv => cv.CvUtbildning)
                .HasForeignKey(ce => ce.CVid);

            modelBuilder.Entity<CV_Utbildning>()
                .HasOne(ce => ce.utbildning)
                .WithMany(u => u.cv_Utbildning)
                .HasForeignKey(ce => ce.Uid);

            // CV_Erfarenhet konfiguration
            modelBuilder.Entity<CV_Erfarenhet>()
                .HasKey(cex => new { cex.Cvid, cex.Eid });

            modelBuilder.Entity<CV_Erfarenhet>()
                .HasOne(cex => cex.cv)
                .WithMany(cv => cv.CvErfarenhet)
                .HasForeignKey(cex => cex.Cvid);

            modelBuilder.Entity<CV_Erfarenhet>()
                .HasOne(cex => cex.erfarenhet)
                .WithMany(e => e.cv_Erfarenhet)
                .HasForeignKey(cex => cex.Eid);

            modelBuilder.Entity<AnvProjekt>()
                .HasKey(up => new { up.Anvid, up.Pid });

            //// Relation mellan Anvandare och Adress
            //modelBuilder.Entity<Adress>()
            //    .HasOne(a => a.anvandare)           // En adress tillhör en användare
            //    .WithOne(u => u.Adress)             // En användare har en adress
            //    .HasForeignKey<Adress>(a => a.Anvid); // Anvid används som foreign key

        }

    }
}
