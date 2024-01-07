
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.repository;

public static class DbInitializer
{
  public static void InjectDbContext(this IServiceCollection services, IConfiguration Configuration)
  {
    services.AddDbContext<ProntuDbContext>(options => options.UseNpgsql(Configuration["ConnectionStrings:UserConnection"]));
    var db = services.BuildServiceProvider().GetRequiredService<ProntuDbContext>();
    db.Database.CanConnectAsync().ContinueWith(_ => db.Database.Migrate());
  }
}