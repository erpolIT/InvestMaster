using backend.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApiDbContext>();
        context.Database.Migrate();
    }
}