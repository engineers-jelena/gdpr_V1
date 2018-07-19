using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using System.Web.ClientServices.Providers;
using System.Web.UI;
using AutoMapper;
using GDPRiS.Api.Models.User;
using GDPRiS.Api.Models.Company;
using GDPRiS.Data.Model;

namespace GDPRiS.Api.Helpers
{
    public class AutoMapperConfigurationProfile : Profile
    {
        public AutoMapperConfigurationProfile()
        {
            CreateMap<Users, UserModel>().ReverseMap();
            CreateMap<Users, UserRegisterModel>().ReverseMap();
            CreateMap<Users, UserJwtModel>().ReverseMap();
            CreateMap<UserSearchModel, Users>().ReverseMap();
            CreateMap<Companies, RegisterCompanyModel>().ReverseMap();
            CreateMap<Companies, CompanyEmployeeModel>().ReverseMap();
            

        }
    }
}