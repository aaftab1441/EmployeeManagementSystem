using AutoMapper;
using DAL.Models;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department.Id));
            CreateMap<EmployeeViewModel, Employee>();
        }
    }
}
