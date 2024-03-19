using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhotographySite.Models;

[Table("PHOTO_Orientation")]
public class Orientation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(20)")]
    [Required]
    public string Name { get; set; }
}