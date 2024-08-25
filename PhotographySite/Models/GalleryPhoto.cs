using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographySite.Models;

[Table("PHOTO_GalleryPhoto")]
public class GalleryPhoto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(TypeName = "int")]
    public int Id { get; set; }

    [Column(TypeName = "int")]
    public long GalleryId { get; set; }

    [Column(TypeName = "bigint")]
    public long PhotoId { get; set; }

    [Column(TypeName = "tinyint")]
    public int Order { get; set; }

    public Photo Photo { get; set; }
}