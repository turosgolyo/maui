namespace Solution.Database.Entities;

[Table("Artist")]
[Index(nameof(Name), IsUnique = true)]
public class ArtistEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(64)]
    [Required]
    public string Name { get; set; }

    //Albums
    public virtual ICollection<AlbumEntity> Albums { get; set; }

    //Songs
    public virtual ICollection<SongEntity> Songs { get; set; }
}
