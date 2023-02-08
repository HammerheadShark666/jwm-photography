using PhotographySite.Models;

namespace PhotographySite.Areas.Admin.Models;

public class PhotoDto
{
    public long Id { get; set; } 

    public string FileName { get; set; }
     
    public string Title { get; set; }
     
    public string Camera { get; set; }
     
    public string Lens { get; set; } 

    public string ExposureTime { get; set; }
     
    public string AperturValue { get; set; }
     
    public string ExposureProgram { get; set; }
     
    public int Iso { get; set; }
     
    public DateTime? DateTaken { get; set; }
     
    public string FocalLength { get; set; }
     
    public int Orientation { get; set; }
     
    public int Height { get; set; }
     
    public int Width { get; set; }
     
    public Boolean UseInMontage { get; set; }
     
    public Country Country { get; set; }

    public Category Category { get; set; }

    public Palette Palette { get; set; }
}
