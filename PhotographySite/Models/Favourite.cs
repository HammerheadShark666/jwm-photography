using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhotographySite.Models;

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