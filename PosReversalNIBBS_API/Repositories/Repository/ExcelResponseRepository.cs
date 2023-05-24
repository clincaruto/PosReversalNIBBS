using Microsoft.EntityFrameworkCore;
using PosReversalNIBBS_API.Data;
using PosReversalNIBBS_API.Models.Domain;
using PosReversalNIBBS_API.Repositories.IRepository;

namespace PosReversalNIBBS_API.Repositories.Repository
{
	public class ExcelResponseRepository : IExcelResponseRepository
	{
		private readonly PosNibbsDbContext context;

		public ExcelResponseRepository(PosNibbsDbContext context)
		{
			this.context = context;
		}
		public async Task<ExcelResponse> AddAsync(ExcelResponse excelResponse)
		{
			excelResponse.Id = Guid.NewGuid();
			await context.ExcelResponses.AddAsync(excelResponse);
			await context.SaveChangesAsync();
			return excelResponse;
		}

		public async Task<ExcelResponse> DeleteAsync(Guid id)
		{
			var excelResponse = await GetAsync(id);

			if (excelResponse != null) 
			{
				context.ExcelResponses.Remove(excelResponse);
				await context.SaveChangesAsync();
				return excelResponse;
			}

			return null;
		}

		public async Task<IEnumerable<ExcelResponse>> GetAllAsync()
		{
			return await context.ExcelResponses.ToListAsync();
		}

		public async Task<ExcelResponse> GetAsync(Guid id)
		{
			return await context.ExcelResponses.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<ExcelResponse> UpdateAsync(Guid id, ExcelResponse excelResponse)
		{
			var existingExcelResponse = await GetAsync(id);
			if (existingExcelResponse != null)
			{
				// I do not want to update the id, but want to update other properties
				existingExcelResponse.IssuingBankName = excelResponse.IssuingBankName;
				existingExcelResponse.Merchant_Id = excelResponse.Merchant_Id;
				existingExcelResponse.Original_Data_Element = excelResponse.Original_Data_Element;	
				existingExcelResponse.Retrieval_Ref_Number = existingExcelResponse.Retrieval_Ref_Number;
				existingExcelResponse.System_Trace_Number = existingExcelResponse.System_Trace_Number;
				existingExcelResponse.Processor_Name = excelResponse.Processor_Name;
				existingExcelResponse.Amount = excelResponse.Amount;
				existingExcelResponse.Terminal_Id = excelResponse.Terminal_Id;
				existingExcelResponse.Bin = excelResponse.Bin;
				existingExcelResponse.Pan = excelResponse.Pan;
				existingExcelResponse.Processing_Code = excelResponse.Processing_Code;
				existingExcelResponse.Response_code = excelResponse.Response_code;
				existingExcelResponse.Transaction_Date = excelResponse.Transaction_Date;
				existingExcelResponse.Account_Id = excelResponse.Account_Id;

				await context.SaveChangesAsync();
				return existingExcelResponse;

			}

			return null;
		}
	}
}
