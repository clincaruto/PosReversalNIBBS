using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PosReversalNIBBS_API.Data;
using PosReversalNIBBS_API.Models.Domain;
using PosReversalNIBBS_API.Repositories.IRepository;

namespace PosReversalNIBBS_API.Repositories.Repository
{
    public class UploadExcelDetailRepository : IUploadedExcelDetailsRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly PosNibbsDbContext dbContext;

        public UploadExcelDetailRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor, PosNibbsDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<UploadedExcelDetail>> GetAllUploadAsync()
        {
            return await dbContext.UploadedExcelDetails.ToListAsync();
        }

        public async Task<UploadedExcelDetail> GetByIdAsync(Guid batchId)
        {
            return await dbContext.UploadedExcelDetails.FirstAsync(x => x.BatchId == batchId);
        }
        public async Task<UploadedExcelDetail> UpdateAsync(Guid id, UploadedExcelDetail uploadedExcelDetail)
        {
            var existingUpload = await GetByIdAsync(id);
            if (existingUpload == null)
            {
                return null;
            }
            existingUpload.TotalTransaction = uploadedExcelDetail.TotalTransaction; 
            existingUpload.TotalAmount = uploadedExcelDetail.TotalAmount;

            await dbContext.SaveChangesAsync();

            return existingUpload;
        }

        public async Task<UploadedExcelDetail> Upload(UploadedExcelDetail uploadedExcelDetail)
        {
           // var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "UploadedFiles",
           //    $"{uploadedExcelDetail.FileName}{uploadedExcelDetail.FileExtension}");

           // // Upload Image to Local Path
           // using var stream = new FileStream(localFilePath, FileMode.Create);
           // await uploadedExcelDetail.File.CopyToAsync(stream);

           // // https://localhost:1234/UploadedFiles/image.jpg

           // var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/UploadedFiles/{uploadedExcelDetail.FileName}{uploadedExcelDetail.FileExtension}";
           //// var urlFilePath = $"{HttpContextAccessor.}"
           // uploadedExcelDetail.FilePath = urlFilePath;

            // Add Image to the UploadedExcelDetails table
            await dbContext.AddAsync(uploadedExcelDetail);
            await dbContext.SaveChangesAsync();

            return uploadedExcelDetail;
        }
    }
}
