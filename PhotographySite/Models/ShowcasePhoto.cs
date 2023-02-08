using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographySite.Models;

public class ShowcasePhoto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "int")]
    public long ShowcaseId { get; set; }

    [Column(TypeName = "int")]
    public long PhotoId { get; set; }

    [Column(TypeName = "tinyint")]
    public int Order { get; set; }
}
