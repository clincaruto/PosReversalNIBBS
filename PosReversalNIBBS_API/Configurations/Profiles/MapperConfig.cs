using AutoMapper;
using PosReversalNIBBS_API.Models.Domain;
using PosReversalNIBBS_API.Models.DTO;

namespace PosReversalNIBBS_API.Configurations.Profiles
{
	public class MapperConfig : Profile
	{
		public MapperConfig()
		{
			CreateMap<ExcelResponse, ExcelResponseVM>().ReverseMap();
			CreateMap<UpdateUploadedExcelDetailVM, UploadedExcelDetail>().ReverseMap();

			CreateMap<UploadedExcelDetail, FileUploadDto>().ForMember(x=>x.Status, y=>y.MapFrom(z=>z.Status.ToString())).ReverseMap();
			CreateMap<ExcelResponse, FileExcelResponseDto>()
				.ForMember(x => x.LOG_DRP, y =>y.MapFrom(z=>z.LOG_DRP.ToString()))
				.ReverseMap();
		}
	}
}
