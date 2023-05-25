using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PosReversalNIBBS_API.Models.Domain;
using PosReversalNIBBS_API.Models.DTO;
using PosReversalNIBBS_API.Repositories.IRepository;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace PosReversalNIBBS_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ExcelResponseController : Controller
	{
		private readonly IExcelResponseRepository excelResponseRepository;
		private readonly IMapper mapper;


		public ExcelResponseController(IExcelResponseRepository excelResponseRepository, IMapper mapper)
		{
			this.excelResponseRepository = excelResponseRepository;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task <IActionResult> GetAllExcelAsync()
		{
			var excelRes = await excelResponseRepository.GetAllAsync();
			var excelResDTO = mapper.Map<List<ExcelResponseVM>>(excelRes);
			return Ok(excelResDTO);
		}

		[HttpGet]
		[Route("{id:guid}")]
		[ActionName("GetExcelAsyncById")]
		public async Task<IActionResult> GetExcelAsyncById(Guid id) 
		{
			var excelRes = await excelResponseRepository.GetAsync(id);

			if (excelRes == null) 
			{
				return NotFound();
				//return BadRequest("Data not found");
			}

			var excelResDTO = mapper.Map<ExcelResponseVM>(excelRes);
			return Ok(excelResDTO);
		}

        [HttpPost]
        [Route("file-upload")]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
          
          
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)

                {
					var filePath  = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    if (!Directory.Exists(filePath))
					{
                        Directory.CreateDirectory(filePath);
                    }
                    using (var fileStream = new FileStream(Path.Combine(filePath, formFile.FileName), FileMode.Create))
                    {
                        await formFile.CopyToAsync(fileStream);
                        return Ok(ReadExcel(fileStream));
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }


        string ReadExcel(Stream stream)
        {
            DataTable dtTable = new DataTable();
            List<string> rowList = new List<string>();
            ISheet sheet;
			using (stream)

			{
				stream.Position = 0;
				XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
				sheet = xssWorkbook.GetSheetAt(0);
				IRow headerRow = sheet.GetRow(0);
				int cellCount = headerRow.LastCellNum;
				for (int j = 0; j < cellCount; j++)
				{
					ICell cell = headerRow.GetCell(j);
					if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
					{
						dtTable.Columns.Add(cell.ToString());
					}
				}
				for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
				{
					IRow row = sheet.GetRow(i);
					if (row == null) continue;
					if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
					for (int j = row.FirstCellNum; j < cellCount; j++)
					{
						if (row.GetCell(j) != null)
						{
							if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) && !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
							{
								rowList.Add(row.GetCell(j).ToString());
							}
						}
					}
					if (rowList.Count > 0)
						dtTable.Rows.Add(rowList.ToArray());
					rowList.Clear();
				}
			}
            return JsonConvert.SerializeObject(dtTable);
        }



        [HttpPost]
		public async Task<IActionResult> AddExcelAsync(AddExcelResponseVM addExcelResponseVM)
		{
			// conver excelDto to domain model
			var excelRes = new ExcelResponse()
			{
				Terminal_Id = addExcelResponseVM.Terminal_Id,
				Merchant_Id = addExcelResponseVM.Merchant_Id,
				Processing_Code = addExcelResponseVM.Processing_Code,
				Bin = addExcelResponseVM.Bin,
				Pan = addExcelResponseVM.Pan,
				Response_code= addExcelResponseVM.Response_code,
				Amount= addExcelResponseVM.Amount,
				System_Trace_Number = addExcelResponseVM.System_Trace_Number,
				Retrieval_Ref_Number = addExcelResponseVM.Retrieval_Ref_Number,
				IssuingBankName = addExcelResponseVM.IssuingBankName,
				Transaction_Date = addExcelResponseVM.Transaction_Date,
				Original_Data_Element = addExcelResponseVM.Original_Data_Element,
				Processor_Name = addExcelResponseVM.Processor_Name
			};

			// pass domain object to Repository
			excelRes = await excelResponseRepository.AddAsync(excelRes);

			// Convert the domain back to DTO
			var excelResDTO = mapper.Map<ExcelResponseVM>(excelRes);
			return CreatedAtAction(nameof(GetExcelAsyncById), new { id = excelResDTO.Id}, excelResDTO);

		}

		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> DeleteExcelAsync(Guid id)
		{
			// call repository to delete excel
			var excelRes = await excelResponseRepository.DeleteAsync(id);

			if (excelRes == null)
			{
				return NotFound();
			}

			// Convert response back to DTO
			var excelResDTO = new ExcelResponseVM
			{
				Id = excelRes.Id,
				Terminal_Id = excelRes.Terminal_Id,
				Merchant_Id = excelRes.Merchant_Id,
				Processing_Code = excelRes.Processing_Code,
				Bin = excelRes.Bin,
				Pan = excelRes.Pan,
				Response_code = excelRes.Response_code,
				Amount = excelRes.Amount,
				System_Trace_Number = excelRes.System_Trace_Number,
				Retrieval_Ref_Number = excelRes.Retrieval_Ref_Number,
				IssuingBankName = excelRes.IssuingBankName,
				Transaction_Date = excelRes.Transaction_Date,
				Original_Data_Element = excelRes.Original_Data_Element,
				Processor_Name = excelRes.Processor_Name,
				Account_Id = excelRes.Account_Id

			};

			return Ok(excelResDTO);
		}



        #region Private Method

		private async Task<bool> ValidateAddExcelAsync(AddExcelResponseVM addExcelResponseVM)
		{
			if (addExcelResponseVM == null)
			{
				ModelState.AddModelError(nameof(addExcelResponseVM),
					$"{nameof(addExcelResponseVM)} can not be empty");
				return false;
			}

			if(ModelState.ErrorCount > 0)
			{
				return false;

			}

			return true;
		}

        #endregion




    }
}
