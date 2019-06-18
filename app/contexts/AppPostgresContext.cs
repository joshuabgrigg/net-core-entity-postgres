using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class AppPostgresContext : DbContext
{
    public DbSet<Message> Messages { get; set; }

    public AppPostgresContext(DbContextOptions<AppPostgresContext> options)
    : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
        {
            entityType.Relational().TableName = entityType.DisplayName();
        }
    }
}