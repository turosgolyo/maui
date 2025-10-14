namespace Solution.Database.Entities;

[Table("Album")]
public class AlbumEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(128)]
    public string? ImageId { get; set; }

    [StringLength(512)]
    public string? WebContentLink { get; set; }

    [Required]
    public DateTime ReleaseDate { get; set; }
    
    [StringLength(128)]
    [Required]
    public string Genre { get; set; }



    //Artist
    [ForeignKey("Artist")]
    public int ArtistId { get; set; }

    public virtual ArtistEntity Artist { get; set; }

    //Songs
    public virtual ICollection<SongEntity> Songs { get; set; }
}
