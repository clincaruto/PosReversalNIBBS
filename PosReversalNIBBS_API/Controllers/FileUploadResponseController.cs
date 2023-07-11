using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HPSF;
using PosReversalNIBBS_API.Models.Domain;
using PosReversalNIBBS_API.Models.DTO;
using PosReversalNIBBS_API.Repositories.IRepository;
using PosReversalNIBBS_API.Utilities;

namespace PosReversalNIBBS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadResponseController : ControllerBase
    {
        private readonly IUploadedExcelDetailsRepository uploadedExcelDetailsRepository;
        private readonly IMapper mapper;

        public FileUploadResponseController(IUploadedExcelDetailsRepository uploadedExcelDetailsRepository, IMapper mapper)
        {
            this.uploadedExcelDetailsRepository = uploadedExcelDetailsRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUpload()
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                string token = authorizationHeader.Replace("Bearer ", "");
                bool checker = JWTDecryption.JWTChecker(token);
                // Get data from database - domain model
                var uploadDomain = await uploadedExcelDetailsRepository.GetAllUploadAsync();

                ////grpCCMS.REMARKS_BY_THE_BANK = util.GetRemarkByBank(dr["CLOSURE_COMMENT"].ToString());
                //string closureComment = util.GetRemarkByBank(dr["CLOSURE_COMMENT"].ToString());
                //if (closureComment != null)
                //{ grpCCMS.REMARKS_BY_THE_BANK = util.GetRemarkByBank(dr["CLOSURE_COMMENT"].ToString()); }
                //else { grpCCMS.REMARKS_BY_THE_BANK = "NA"; }

                //UploadedExcelDetail stat = new UploadedExcelDetail();
                //string statuschecker = 
                // Map domain model to Dtos
                var uploadDTO = mapper.Map<List<FileUploadDto>>(uploadDomain);

                return Ok(uploadDTO);
            }
            else
            {
                // Authorization header is not present
                return BadRequest("Authorization header is missing.");
            }
           
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByIdUpload(Guid id)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                string token = authorizationHeader.Replace("Bearer ", "");
                bool checker = JWTDecryption.JWTChecker(token);

                var upload = await uploadedExcelDetailsRepository.GetByIdAsync(id);

                if (upload == null)
                {
                    return NotFound();
                }

                // Map Domain model to DTO
                var uploadDTO = mapper.Map<FileUploadDto>(upload);

                return Ok(uploadDTO);
            }
            else
            {

                // Authorization header is not present
                return BadRequest("Authorization header is missing.");
            }
            
        }

    }
}
