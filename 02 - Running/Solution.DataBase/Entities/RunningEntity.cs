using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Database.Entities;

[Table("Running")]
public class RunningEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(64)]
    public string PublicId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public double Distance { get; set; }

    [Required]
    public double AverageSpeed { get; set; }
    
    [Required]
    public int BurnedCalories { get; set; }

    [Required]
    public int Time { get; set; }
}
