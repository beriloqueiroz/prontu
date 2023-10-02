using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.repository;

public static class DbInitializer
{
  public static void InjectDbContext(this IServiceCollection services)
  {
    services.AddDbContext<ProntuDbContext>(
    options => options.UseNpgsql("Host=localhost;Database=prontu_db;Username=postgres;Password=teste@123"));
  }

}