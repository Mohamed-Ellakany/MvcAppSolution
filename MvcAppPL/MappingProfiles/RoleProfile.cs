using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MvcAppPL.ViewModels;

namespace MvcAppPL.MappingProfiles
{
    public class RoleProfile:Profile
    {

        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().ForMember(R=>R.RoleName , O=>O.MapFrom(s=>s.Name)  ).ReverseMap();
        }
    }
}
