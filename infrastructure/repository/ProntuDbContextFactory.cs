namespace infrastructure.repository;

using Microsoft.EntityFrameworkCore.Design;

public class ProntuDbContextFactory : IDesignTimeDbContextFactory<ProntuDbContext>
{
  public ProntuDbContext CreateDbContext(string[] args)
  {
    return new ProntuDbContext(new());
  }
}