namespace PhotographySite.Areas.Admin.Models;

public class GalleriesDto
{
    public LookupsDto LookupsDto { get; set; }

    public List<GalleryDto> GalleryListDto { get; set; }

    public PhotosPageDto PhotoListDto { get; set; }

    public GalleryDto SelectedGallery { get; set; }

    public List<PhotoDto> SelectGalleryPhotos { get; set; }

	public string AzureStoragePhotosContainerUrl { get; set; }
}
