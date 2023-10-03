namespace infrastructure.repository;

using Microsoft.EntityFrameworkCore;

public class ProntuDbContext : DbContext
{
  public static string UrlConnection = "Host=localhost;Database=prontu_db;Username=teste;Password=teste";
  public ProntuDbContext(DbContextOptions<ProntuDbContext> options) : base(options)
  {
  }
  protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseNpgsql(UrlConnection);
  public DbSet<Professional>? Professionals { get; set; }
  public DbSet<Patient>? Patients { get; set; }
  public DbSet<ProfessionalPatient>? ProfessionalsPatients { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder
        .Entity<ProfessionalPatient>()
        .HasOne(p => p.Professional)
        .WithMany(pp => pp.ProfessionalPatients)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder
        .Entity<ProfessionalPatient>()
        .HasOne(p => p.Patient)
        .WithMany(pp => pp.ProfessionalPatients)
        .OnDelete(DeleteBehavior.Cascade);
  }
}