namespace Authentification.Domain.Database;

public sealed class AuthentificationDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
{
    public override DbSet<UserEntity> Users { get; set; }

    public AuthentificationDbContext(DbContextOptions<AuthentificationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureUser();
    }

}
