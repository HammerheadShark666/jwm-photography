using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PhotographySite.Helpers.Enums;

namespace PhotographySite.Models;

public class Photo
{
    private PhotoOrientation orientation;              

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    [Required]
    public string FileName { get; set; }

    [Column(TypeName = "nvarchar(150)")]        
    public string Title { get; set; }         

    [Column(TypeName = "nvarchar(100)")]
    public string Camera { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    public string Lens { get; set; }

    [Column(TypeName = "nvarchar(25)")]
    public string ExposureTime { get; set; }

    [Column(TypeName = "nvarchar(25)")]
    public string AperturValue { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string ExposureProgram { get; set; }

    [Column(TypeName = "int")]
    public int Iso { get; set; }

    [Column(TypeName = "datetime2(7)")]
    public DateTime? DateTaken { get; set; }

    [Column(TypeName = "nvarchar(10)")]
    public string FocalLength { get; set; }

    [Column(TypeName = "tinyint")]
    public int Orientation { get; set; }

    [Column(TypeName = "int")]
    public int Height { get; set; }

    [Column(TypeName = "int")]
    public int Width { get; set; }
          
    [Column(TypeName = "bit")]
    public Boolean UseInMontage { get; set; }

    public Country Country { get; set; }
 
    public Category Category { get; set; }  

    public Palette Palette { get; set; } 

    public List<Favourite> Favourites { get; set; }
}
