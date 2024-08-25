using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographySite.Models;

[Table("PHOTO_Favourite")]

public class Favourite
{
    [Required]
    [Column(TypeName = "UNIQUEIDENTIFIER")]
    public Guid UserId { get; set; }

    [Required]
    [Column(TypeName = "bigint")]
    public long PhotoId { get; set; }

    public Photo Photo { get; set; }
}