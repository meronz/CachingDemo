namespace CachingDemo.Storage;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(_ => _.Id);
        });
    }

    public void ApplyDatabaseMigrations()
    {
        Database.Migrate();
    }
}