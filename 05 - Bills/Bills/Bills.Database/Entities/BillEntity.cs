namespace Bills.Database.Entities;

[Table("Bill")]
public class BillEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int Number { get; set; }

    [Required]
    public DateTime Date { get; set; }

    //Items
    public virtual ICollection<ItemEntity> Items { get; set; }

}
