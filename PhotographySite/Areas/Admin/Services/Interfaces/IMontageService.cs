using PhotographySite.Dto.Response;
using PhotographySite.Models;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface IMontageService
{
    Task<Montage> AddImageTemplateAsync(int column, int order, int orientation);
    Task MoveImageTemplateAsync(int id, int column, int order);
    Task DeleteImageTemplateAsync(int id);
    Task<MontagesResponse> GetMontageTemplatesAsync();
}