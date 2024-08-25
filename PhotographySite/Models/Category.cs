using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographySite.Models;

[Table("PHOTO_Category")]
public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(75)")]
    [Required]
    public string Name { get; set; }
}