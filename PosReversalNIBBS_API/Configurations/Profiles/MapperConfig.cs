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
		}
	}
}
