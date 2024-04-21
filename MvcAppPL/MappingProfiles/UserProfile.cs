using AutoMapper;
using MvcAppDAL.Models;
using MvcAppPL.ViewModels;

namespace MvcAppPL.MappingProfiles
{
    public class UserProfile:Profile
    {

        public UserProfile()
        {
            CreateMap<AppUser, UserViewModel>().ReverseMap();
        }
    }
}
