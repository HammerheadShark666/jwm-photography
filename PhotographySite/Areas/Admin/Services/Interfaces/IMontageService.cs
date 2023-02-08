using PhotographySite.Models;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface IMontageService
{
    Task<Montage> AddImageTemplateAsync(int column, int order, int orientation);
    Task MoveImageTemplateAsync(int id, int column, int order);
    Task DeleteImageTemplateAsync(int id);
    Task<MontagesDto> GetMontageTemplatesAsync();
}
