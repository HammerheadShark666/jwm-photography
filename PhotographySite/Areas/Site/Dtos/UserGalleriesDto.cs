using PhotographySite.Areas.Admin.Dtos;
using PhotographySite.Helpers; 

namespace PhotographySite.Models.Dto;

public class UserGalleriesDto
{
    public LookupsDto LookupsDto { get; set; }

    public List<UserGalleryDto> UserGalleryListDto { get; set; }

    public PhotosPageDto PhotoListDto { get; set; }

    public UserGalleryDto SelectedUserGallery { get; set; }

    public List<PhotoDto> SelectUserGalleryPhotos { get; set; }

	public string AzureStoragePhotosContainerUrl { get { return EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl(); } }
}
