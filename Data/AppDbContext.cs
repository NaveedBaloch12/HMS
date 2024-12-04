using HMS.Entites;
using Microsoft.EntityFrameworkCore;

namespace HMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Examination>()
                .HasOne(e => e.Patient)
                .WithMany()
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // PharmacyInventory Configuration
            modelBuilder.Entity<PharmacyInventory>()
                .HasMany(pi => pi.DispensedMedicines)
                .WithOne(dm => dm.PharmacyInventory)
                .HasForeignKey(dm => dm.MedicineId);

            // DispensedMedicine Configuration
            modelBuilder.Entity<DispensedMedicine>()
                .HasOne(dm => dm.Patient)
                .WithMany(p => p.DispensedMedicines)
                .HasForeignKey(dm => dm.PatientId);

            modelBuilder.Entity<DispensedMedicine>()
                .HasOne(dm => dm.Examination)
                .WithMany(e => e.DispensedMedicines)
                .HasForeignKey(dm => dm.ExaminationId);
        }



        // Define Tables Here
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<PharmacyInventory> PharmacyInventories { get; set; }
        public DbSet<DispensedMedicine> DispensedMedicines { get; set; }
        public DbSet<LabResult> LabResults { get; set; }
        public DbSet<SuggestedTest> SuggestedTests { get; set; }
    }
}
