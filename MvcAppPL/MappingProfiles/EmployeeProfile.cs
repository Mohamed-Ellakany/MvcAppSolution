using AutoMapper;
using MvcAppDAL.Models;
using MvcAppPL.ViewModels;

namespace MvcAppPL.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
