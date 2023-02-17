using AutoMapper;
using DAL.Models;
using EmployeeManagementSystem.Models;

namespace Tests
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department.Id));
            CreateMap<EmployeeViewModel, Employee>();
        }
    }
}
