using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhotographySite.Models;

public class Gallery
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column(TypeName = "int")]
	public long Id { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    [Required]
    public string Name { get; set; }

	public ICollection<GalleryPhoto> Photos { get; set; }

	[Column(TypeName = "nvarchar(1000)")]
	public string Description { get; set; } 
}