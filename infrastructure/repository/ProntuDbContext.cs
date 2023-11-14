namespace infrastructure.repository;

using domain;
using Microsoft.EntityFrameworkCore;

public class ProntuDbContext : DbContext
{
  public static readonly string UrlConnection = "Host=localhost;Database=prontu_db;Username=teste;Password=teste";
  public ProntuDbContext(DbContextOptions<ProntuDbContext> options) : base(options)
  {
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);//para solucionar datetime error Pgsql
  }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(UrlConnection);
  public DbSet<Professional>? Professionals { get; set; }
  public DbSet<Patient>? Patients { get; set; }
  public DbSet<ProfessionalPatient>? ProfessionalsPatients { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Professional>()
        .HasMany(pr => pr.Patients)
        .WithMany(pa => pa.Professionals)
        .UsingEntity<ProfessionalPatient>();

    modelBuilder.Entity<Patient>()
      .HasOne(p => p.PersonalForm)
      .WithOne(pf => pf.Patient)
      .HasForeignKey<PersonalForm>()
      .IsRequired(false);

    modelBuilder
      .Entity<ProfessionalPatient>()
      .Property(e => e.SessionType)
      .HasConversion(
          v => v.ToString(),
          v => (SessionType)Enum.Parse(typeof(SessionType), v));

    modelBuilder
      .Entity<ProfessionalPatient>()
      .Property(e => e.PaymentType)
      .HasConversion(
          v => v.ToString(),
          v => (PaymentType)Enum.Parse(typeof(PaymentType), v));
  }
}