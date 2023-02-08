using AutoMapper;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Helpers;
using PhotographySite.Models;
using PhotographySite.Models.Dto;

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
        Montage montage = new ()
        {
            Column = column,
            Order = order,
            Orientation = (Enums.PhotoOrientation)orientation
        };

        montage = await _unitOfWork.Montages.AddAsync(montage);        
        await UpdateMontageColumnOrderAsync(column, order, montage);
        _unitOfWork.Complete();

        return montage;
    }

    public async Task MoveImageTemplateAsync(int id, int column, int order)
    {
        Montage montageToMove = await _unitOfWork.Montages.ByIdAsync(id);

        if (montageToMove != null)
        {
            int originalColumn = montageToMove.Column;
            int originalOrder = montageToMove.Order;

            montageToMove.Order = order;
            montageToMove.Column = column;

            _unitOfWork.Montages.Update(montageToMove);
            await UpdateMontageColumnOrderAsync(originalColumn, originalOrder, montageToMove);
            _unitOfWork.Complete();
        }
    }

    public async Task DeleteImageTemplateAsync(int id)
    {
        Montage montageToDelete = await _unitOfWork.Montages.ByIdAsync(id);

        if (montageToDelete != null)
        {
            _unitOfWork.Montages.Remove(montageToDelete);

            List<Montage> montages = (await _unitOfWork.Montages.FindAsync(m => m.Column == montageToDelete.Column && m.Order > montageToDelete.Order)).OrderBy(m => m.Order).ToList<Montage>();
            UpdateMontagesOrder(montages, montageToDelete.Order, null);

            _unitOfWork.Complete();
        }
    }

    public async Task<MontagesDto> GetMontageTemplatesAsync()
    {
        List<Montage> montages = await _unitOfWork.Montages.AllSortedAsync();

        List<List<MontageDto>> montageImagesColumns = new List<List<MontageDto>>();
        montageImagesColumns.Add(GetMontageTemplatesForColumn(montages, 1));
        montageImagesColumns.Add(GetMontageTemplatesForColumn(montages, 2));
        montageImagesColumns.Add(GetMontageTemplatesForColumn(montages, 3));
        montageImagesColumns.Add(GetMontageTemplatesForColumn(montages, 4));

        return new MontagesDto()
        {
            MontageImagesColumns = montageImagesColumns 
        };
    }

    private async Task UpdateMontageColumnOrderAsync(int originalColumn, int originalOrder, Montage montage)
    {
        if (originalColumn != montage.Column)
        {
            List<Montage> originalColumnMontages = (await _unitOfWork.Montages.FindAsync(m => m.Column == originalColumn && m.Order > originalOrder)).OrderBy(m => m.Order).ToList<Montage>();
            UpdateMontagesOrder(originalColumnMontages, originalOrder, null);

            List<Montage> newColumnMontages = (await _unitOfWork.Montages.FindAsync(m => m.Column == montage.Column && m.Order >= montage.Order)).OrderBy(m => m.Order).ToList<Montage>();
            UpdateMontagesOrder(newColumnMontages, montage.Order + 1, null);
        } 
        else if(montage.Id == 0)
        {
            List<Montage> allColumnMontages = (await _unitOfWork.Montages.FindAsync(m => m.Column == montage.Column)).OrderBy(m => m.Order).ToList<Montage>();
            
            UpdateMontagesOrder(allColumnMontages, 1, montage.Order);
            allColumnMontages.Insert(montage.Order - 1, montage);
        }
        else
        {
            List<Montage> allColumnMontages = (await _unitOfWork.Montages.FindAsync(m => m.Column == montage.Column)).OrderBy(m => m.Order).ToList<Montage>();
            allColumnMontages.RemoveAt(allColumnMontages.FindIndex(m => m.Id == montage.Id));
            allColumnMontages.Insert(montage.Order - 1, montage);
            UpdateMontagesOrder(allColumnMontages, 1, null);
        } 
    }

    private void UpdateMontagesOrder(List<Montage> montages, int baseOrder, int? ignoreOrder)
    {
        foreach (Montage m in montages)
        {
            if (!ignoreOrder.HasValue || ignoreOrder != m.Order) {  
                m.Order = baseOrder;
                _unitOfWork.Montages.Update(m);                
            } 
            else
            {
                baseOrder++;
                m.Order = baseOrder;
                _unitOfWork.Montages.Update(m);
            }

            baseOrder++;
        }
    }

    private List<MontageDto> GetMontageTemplatesForColumn(List<Montage> montages, int column)
    {
        return MontageHelper.AddMontageTemplateImages(_mapper.Map<List<MontageDto>>(GetColumnMontages(montages, column)));
    }

    private List<Montage> GetColumnMontages(List<Montage> montages, int column)
    {
        return montages.FindAll(i => i.Column == column).OrderBy(i => i.Order).ToList();
    }
}
