using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentProject.Controllers.Resources;
using StudentProject.Models;

namespace StudentProject.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Models to Resources
            CreateMap<LoginInfo, LoginInfoResource>();
            CreateMap<LoginInfo, LoginIdTypeResource>()
                .ForMember(logi => logi.Id, opt => opt.MapFrom(log => log.Id))
                .ForMember(logi => logi.UserType, opt => opt.MapFrom(log => log.UserType))
                .ForMember(logi => logi.Loginid, opt => opt.MapFrom(log => log.Loginid));

            //Resource to Model
        }
    }
}
