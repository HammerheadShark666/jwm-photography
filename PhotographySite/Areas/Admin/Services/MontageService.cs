using AutoMapper;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Response;
using PhotographySite.Helpers;
using PhotographySite.Models;

namespace PhotographySite.Areas.Admin.Services;

public class MontageService(IUnitOfWork unitOfWork, IMapper mapper) : IMontageService
{
    public async Task<Montage> AddImageTemplateAsync(int column, int order, int orientation)
    {
        Montage montage = new()
        {
            Column = column,
            Order = order,
            Orientation = (Enums.PhotoOrientation)orientation
        };

        montage = await unitOfWork.Montages.AddAsync(montage);
        await UpdateMontageColumnOrderAsync(column, order, montage);
        await unitOfWork.Complete();

        return montage;
    }

    public async Task MoveImageTemplateAsync(int id, int column, int order)
    {
        var montageToMove = await unitOfWork.Montages.ByIdAsync(id);

        if (montageToMove != null)
        {
            int originalColumn = montageToMove.Column;
            int originalOrder = montageToMove.Order;

            montageToMove.Order = order;
            montageToMove.Column = column;

            unitOfWork.Montages.Update(montageToMove);
            await UpdateMontageColumnOrderAsync(originalColumn, originalOrder, montageToMove);
            await unitOfWork.Complete();
        }
    }

    public async Task DeleteImageTemplateAsync(int id)
    {
        var montageToDelete = await unitOfWork.Montages.ByIdAsync(id);

        if (montageToDelete != null)
        {
            unitOfWork.Montages.Delete(montageToDelete);

            var montages = (await unitOfWork.Montages.FindAsync(m => m.Column == montageToDelete.Column && m.Order > montageToDelete.Order)).OrderBy(m => m.Order).ToList<Montage>();
            UpdateMontagesOrder(montages, montageToDelete.Order, null);

            await unitOfWork.Complete();
        }
    }

    public async Task<MontagesResponse> GetMontageTemplatesAsync()
    {
        var montages = await unitOfWork.Montages.AllSortedAsync();

        return new MontagesResponse()
        {
            MontageImagesColumns = [
                GetMontageTemplatesForColumn(montages, 1),
                GetMontageTemplatesForColumn(montages, 2),
                GetMontageTemplatesForColumn(montages, 3),
                GetMontageTemplatesForColumn(montages, 4)
            ]
        };
    }

    private async Task UpdateMontageColumnOrderAsync(int originalColumn, int originalOrder, Montage montage)
    {
        if (originalColumn != montage.Column)
        {
            var originalColumnMontages = (await unitOfWork.Montages.FindAsync(m => m.Column == originalColumn && m.Order > originalOrder)).OrderBy(m => m.Order).ToList<Montage>();
            UpdateMontagesOrder(originalColumnMontages, originalOrder, null);

            var newColumnMontages = (await unitOfWork.Montages.FindAsync(m => m.Column == montage.Column && m.Order >= montage.Order)).OrderBy(m => m.Order).ToList<Montage>();
            UpdateMontagesOrder(newColumnMontages, montage.Order + 1, null);
        }
        else if (montage.Id == 0)
        {
            var allColumnMontages = (await unitOfWork.Montages.FindAsync(m => m.Column == montage.Column)).OrderBy(m => m.Order).ToList<Montage>();

            UpdateMontagesOrder(allColumnMontages, 1, montage.Order);
            allColumnMontages.Insert(montage.Order - 1, montage);
        }
        else
        {
            var allColumnMontages = (await unitOfWork.Montages.FindAsync(m => m.Column == montage.Column)).OrderBy(m => m.Order).ToList<Montage>();
            allColumnMontages.RemoveAt(allColumnMontages.FindIndex(m => m.Id == montage.Id));
            allColumnMontages.Insert(montage.Order - 1, montage);
            UpdateMontagesOrder(allColumnMontages, 1, null);
        }
    }

    private void UpdateMontagesOrder(List<Montage> montages, int baseOrder, int? ignoreOrder)
    {
        foreach (Montage m in montages)
        {
            if ((ignoreOrder.HasValue && ignoreOrder == m.Order))
                baseOrder++;

            m.Order = baseOrder;
            unitOfWork.Montages.Update(m);

            baseOrder++;
        }
    }

    private List<MontageResponse> GetMontageTemplatesForColumn(List<Montage> montages, int column)
    {
        return MontageHelper.AddMontageTemplateImages(mapper.Map<List<MontageResponse>>(GetColumnMontages(montages, column)));
    }

    private static List<Montage> GetColumnMontages(List<Montage> montages, int column)
    {
        return [.. montages.FindAll(i => i.Column == column).OrderBy(i => i.Order)];
    }
}