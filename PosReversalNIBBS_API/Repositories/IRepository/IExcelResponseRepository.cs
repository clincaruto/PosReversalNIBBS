using PosReversalNIBBS_API.Models.Domain;
using PosReversalNIBBS_API.Models.DTO;

namespace PosReversalNIBBS_API.Repositories.IRepository
{
    public interface IExcelResponseRepository
    {
        Task<IEnumerable<ExcelResponse>> GetAllAsync();
        Task<ExcelResponse> GetAsync(Guid id);
        Task<ExcelResponse> AddAsync(ExcelResponse excelResponse);
        Task<ExcelResponse> UpdateAsync(Guid id, ExcelResponse excelResponse);
        Task<ExcelResponse> DeleteAsync(Guid id);
        Task<ExcelResponse?> CheckDuplicate(AddExcelResponseVM addExcelResponseVM);
        Task<ExcelResponse> AddExcelAsync(AddExcelResponseVM excelResponse);
    }
}
