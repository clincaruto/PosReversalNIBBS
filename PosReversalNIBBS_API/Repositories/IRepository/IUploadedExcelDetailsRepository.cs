using PosReversalNIBBS_API.Models.Domain;

namespace PosReversalNIBBS_API.Repositories.IRepository
{
    public interface IUploadedExcelDetailsRepository
    {
        Task<UploadedExcelDetail> Upload(UploadedExcelDetail uploadedExcelDetail);
        Task<UploadedExcelDetail> UpdateAsync(Guid id, UploadedExcelDetail uploadedExcelDetail);
        Task<UploadedExcelDetail> GetByIdAsync(Guid batchId);
        Task<IEnumerable<UploadedExcelDetail>> GetAllUploadAsync();
        Task<ExcelResponse> GetByIdBatchExcelResponse (Guid batchId);

        Task<IEnumerable<ExcelResponse>> GetAllUploadbyBatch(Guid id);
    }
}
