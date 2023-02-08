using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhotographySite.Models;

public class Showcase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    [Required]
    public string Name { get; set; }

    [Column(TypeName = "datetime2(7)")]
    [Required]
    public DateTime DateFrom { get; set; }
}
