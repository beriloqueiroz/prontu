namespace infrastructure.repository;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ProntuDbContextFactory : IDesignTimeDbContextFactory<ProntuDbContext>
{
  public ProntuDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<ProntuDbContext>();
    optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionStrings:UserConnection"));
    return new ProntuDbContext(optionsBuilder.Options);
  }
}