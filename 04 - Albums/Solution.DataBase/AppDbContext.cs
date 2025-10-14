using Solution.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Solution.Database;

public class AppDbContext : DbContext
{
    public DbSet<ArtistEntity> Artists { get; set; }
    public DbSet<AlbumEntity> Albums { get; set; }
    public DbSet<SongEntity> Songs { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public AppDbContext() { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ArtistEntity>().HasMany(x => x.Songs).WithOne(x => x.Artist);
        builder.Entity<ArtistEntity>().HasMany(x => x.Albums).WithOne(x => x.Artist);
        builder.Entity<AlbumEntity>().HasOne(x => x.Artist).WithMany(x => x.Albums);
        builder.Entity<AlbumEntity>().HasMany(x => x.Songs).WithOne(x => x.Album);
        builder.Entity<SongEntity>().HasOne(x => x.Artist).WithMany(x => x.Songs);
        builder.Entity<SongEntity>().HasOne(x => x.Album).WithMany(x => x.Songs);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=MotorcycleDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
        }
    }
}