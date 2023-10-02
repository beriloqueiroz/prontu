namespace infrastructure.repository;

using Microsoft.EntityFrameworkCore;

public class ProntuDbContext : DbContext
{
  public ProntuDbContext(DbContextOptions<ProntuDbContext> options) : base(options)
  {

  }
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