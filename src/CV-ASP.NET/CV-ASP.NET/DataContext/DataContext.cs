using CV_ASP.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace CV_ASP.NET.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }

        public DbSet<Adress> Adresser { get; set; }
        public DbSet<Anvandare> Anvandare { get; set; }
        public DbSet<CV> CVs { get; set; }
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
                .HasOne(m => m.Frananvandare) // One-to-Many: Sent Messages
                .WithMany(u => u.skickatMeddelande)            // Meddelande has a Sender
                .HasForeignKey(m => m.FranAnvandareId)    // Foreign Key in Meddelande
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Meddelande>()
                .HasOne(m => m.Tillanvandare) // One-to-Many: Received Messages
                .WithMany(u => u.TagitEmotMeddelande)            // Meddelande has a Receiver
                .HasForeignKey(m => m.TillAnvandareId)    // Foreign Key in Meddelande
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CV_kompetenser>()
                .HasKey(cc => new { cc.Cvid, cc.Kid });
            modelBuilder.Entity<CV_Utbildning>()
                .HasKey(ce => new { ce.CVid, ce.Uid });
            modelBuilder.Entity<CV_Erfarenhet>()
                .HasKey(cex => new { cex.Cvid, cex.Eid });
           // modelBuilder.Entity<AnvProjekt>()
               // .HasKey(up => new { up.Anvid, up.Pid });


        }

       

    }
}
