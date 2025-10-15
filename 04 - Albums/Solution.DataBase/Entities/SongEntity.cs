namespace Solution.Database.Entities;

[Table("Song")]
[Index(nameof(Name), IsUnique = true)]
public class SongEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(64)]
    [Required]
    public string Name { get; set; }

    [Required]
    public string Duration { get; set; }

    //Album
    [ForeignKey("Album")]
    public int AlbumId { get; set; }
    public virtual AlbumEntity Album { get; set; }

    //Artist
    [ForeignKey("Artist")]
    public int ArtistId { get; set; }
    public virtual ArtistEntity Artist { get; set; }
}
