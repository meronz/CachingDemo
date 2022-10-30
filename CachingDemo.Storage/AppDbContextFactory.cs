using Microsoft.EntityFrameworkCore.Design;

namespace CachingDemo.Storage;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                               "Host=localhost:5432;Username=root;Password=root;Database=test_db";
        var builder = new DbContextOptionsBuilder();
        builder.LogTo(Console.WriteLine);
        builder.UseNpgsql(connectionString);

        return new(builder.Options);
    }
}
