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
using System.Text.Json;

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
						try
						{
                            JsonSerializerOptions options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };
                            await formFile.CopyToAsync(fileStream);
                            string excelO = ReadExcel(fileStream);
                            var objt = JsonConvert.DeserializeObject<AddExcelResponseVM>(excelO);
                            return Ok();

                        }
						catch (Exception ex)
						{
                            

                        }
                   
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

					if ( rowList.Count > 0)
						//Console.WriteLine("" + dtTable.Rows[0].Table.Columns.Count);
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
				TERMINAL_ID = addExcelResponseVM.TERMINAL_ID,
				MERCHANT_ID = addExcelResponseVM.MERCHANT_ID,
                AMOUNT = addExcelResponseVM.AMOUNT,
				STAN = addExcelResponseVM.STAN,
				RRN = addExcelResponseVM.RRN,
                PAN = addExcelResponseVM.PAN,
                TRANSACTION_DATE = addExcelResponseVM.TRANSACTION_DATE,
				PROCESSOR = addExcelResponseVM.PROCESSOR,
				BANK = addExcelResponseVM.BANK
				
               
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
				TERMINAL_ID = excelRes.TERMINAL_ID,
				MERCHANT_ID = excelRes.MERCHANT_ID,
				AMOUNT = excelRes.AMOUNT,
				STAN = excelRes.STAN,
				RRN = excelRes.RRN,
				PAN = excelRes.PAN,
				TRANSACTION_DATE= excelRes.TRANSACTION_DATE,
				PROCESSOR = excelRes.PROCESSOR,
				BANK = excelRes.BANK,
				ACCOUNT_ID = excelRes.ACCOUNT_ID

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
