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
				existingExcelResponse.TERMINAL_ID = excelResponse.TERMINAL_ID;
				existingExcelResponse.MERCHANT_ID = excelResponse.MERCHANT_ID;
				existingExcelResponse.AMOUNT = excelResponse.AMOUNT;
				existingExcelResponse.STAN = excelResponse.STAN;
				existingExcelResponse.RRN = excelResponse.RRN;
				existingExcelResponse.PAN = excelResponse.PAN;
				existingExcelResponse.TRANSACTION_DATE = excelResponse.TRANSACTION_DATE;
				existingExcelResponse.PROCESSOR = excelResponse.PROCESSOR;
				existingExcelResponse.ACCOUNT_ID = excelResponse.ACCOUNT_ID;

				await context.SaveChangesAsync();
				return existingExcelResponse;

			}

			return null;
		}
	}
}
