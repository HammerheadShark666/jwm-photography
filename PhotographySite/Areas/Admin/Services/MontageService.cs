using AutoMapper;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Response;
using PhotographySite.Helpers;
using PhotographySite.Models;

namespace PhotographySite.Areas.Admin.Services;

public class MontageService : IMontageService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public MontageService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Montage> AddImageTemplateAsync(int column, int order, int orientation)
    {
        Montage montage = new()
        {
            Column = column,
            Order = order,
            Orientation = (Enums.PhotoOrientation)orientation
        };

        montage = await _unitOfWork.Montages.AddAsync(montage);
        await UpdateMontageColumnOrderAsync(column, order, montage);
        await _unitOfWork.Complete();

        return montage;
    }

    public async Task MoveImageTemplateAsync(int id, int column, int order)
    {
        var montageToMove = await _unitOfWork.Montages.ByIdAsync(id);

        if (montageToMove != null)
        {
            int originalColumn = montageToMove.Column;
            int originalOrder = montageToMove.Order;

            montageToMove.Order = order;
            montageToMove.Column = column;

            _unitOfWork.Montages.Update(montageToMove);
            await UpdateMontageColumnOrderAsync(originalColumn, originalOrder, montageToMove);
            await _unitOfWork.Complete();
        }
    }

    public async Task DeleteImageTemplateAsync(int id)
    {
        var montageToDelete = await _unitOfWork.Montages.ByIdAsync(id);

        if (montageToDelete != null)
        {
            _unitOfWork.Montages.Delete(montageToDelete);

            var montages = (await _unitOfWork.Montages.FindAsync(m => m.Column == montageToDelete.Column && m.Order > montageToDelete.Order)).OrderBy(m => m.Order).ToList<Montage>();
            UpdateMontagesOrder(montages, montageToDelete.Order, null);

            await _unitOfWork.Complete();
        }
    }

    public async Task<MontagesResponse> GetMontageTemplatesAsync()
    {
        var montages = await _unitOfWork.Montages.AllSortedAsync();

        return new MontagesResponse()
        {
            MontageImagesColumns = new List<List<MontageResponse>> {
                GetMontageTemplatesForColumn(montages, 1),
                GetMontageTemplatesForColumn(montages, 2),
                GetMontageTemplatesForColumn(montages, 3),
                GetMontageTemplatesForColumn(montages, 4)
            }
        };
    }

    private async Task UpdateMontageColumnOrderAsync(int originalColumn, int originalOrder, Montage montage)
    {
        if (originalColumn != montage.Column)
        {
            var originalColumnMontages = (await _unitOfWork.Montages.FindAsync(m => m.Column == originalColumn && m.Order > originalOrder)).OrderBy(m => m.Order).ToList<Montage>();
            UpdateMontagesOrder(originalColumnMontages, originalOrder, null);

            var newColumnMontages = (await _unitOfWork.Montages.FindAsync(m => m.Column == montage.Column && m.Order >= montage.Order)).OrderBy(m => m.Order).ToList<Montage>();
            UpdateMontagesOrder(newColumnMontages, montage.Order + 1, null);
        }
        else if (montage.Id == 0)
        {
            var allColumnMontages = (await _unitOfWork.Montages.FindAsync(m => m.Column == montage.Column)).OrderBy(m => m.Order).ToList<Montage>();

            UpdateMontagesOrder(allColumnMontages, 1, montage.Order);
            allColumnMontages.Insert(montage.Order - 1, montage);
        }
        else
        {
            var allColumnMontages = (await _unitOfWork.Montages.FindAsync(m => m.Column == montage.Column)).OrderBy(m => m.Order).ToList<Montage>();
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
            _unitOfWork.Montages.Update(m);

            baseOrder++;
        }
    }

    private List<MontageResponse> GetMontageTemplatesForColumn(List<Montage> montages, int column)
    {
        return MontageHelper.AddMontageTemplateImages(_mapper.Map<List<MontageResponse>>(GetColumnMontages(montages, column)));
    }

    private List<Montage> GetColumnMontages(List<Montage> montages, int column)
    {
        return montages.FindAll(i => i.Column == column).OrderBy(i => i.Order).ToList();
    }
}