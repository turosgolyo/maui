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
        builder.Entity<ArtistEntity>().HasMany(x => x.Songs).WithOne(x => x.Artist).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<ArtistEntity>().HasMany(x => x.Albums).WithOne(x => x.Artist).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<AlbumEntity>().HasOne(x => x.Artist).WithMany(x => x.Albums).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<AlbumEntity>().HasMany(x => x.Songs).WithOne(x => x.Album).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<SongEntity>().HasOne(x => x.Artist).WithMany(x => x.Songs).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<SongEntity>().HasOne(x => x.Album).WithMany(x => x.Songs).OnDelete(DeleteBehavior.NoAction);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}