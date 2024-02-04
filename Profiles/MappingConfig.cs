using AutoMapper;
using EMS.DTO.DepartmentDto;
using EMS.Model;

namespace EMS.Profiles
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Department, AddDepartmentDto>().ReverseMap();
            CreateMap<Department, DepartmentDtos>().ReverseMap();
            CreateMap<Department, UpdateDepartmentDto>().ReverseMap();


        }
    }
}
