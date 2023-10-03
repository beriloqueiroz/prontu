
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.repository;

public static class DbInitializer
{
  public static void InjectDbContext(this IServiceCollection services)
  {
    services.AddDbContext<ProntuDbContext>();
    var db = services.BuildServiceProvider().GetRequiredService<ProntuDbContext>();
    db.Database.CanConnectAsync().ContinueWith(_ => db.Database.Migrate());
  }
}