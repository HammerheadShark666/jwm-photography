using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static PhotographySite.Helpers.Enums;

namespace PhotographySite.Models;

public class Montage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "tinyint")]
    [Required]
    public int Column { get; set; }

    [Column(TypeName = "tinyint")]
    [Required]
    public int Order { get; set; }

    [Column(TypeName = "tinyint")]
    [Required]
    public PhotoOrientation Orientation { get; set; }
}