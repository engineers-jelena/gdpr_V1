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
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, UserRegisterModel>().ReverseMap();
            CreateMap<User, UserJwtModel>().ReverseMap();
            CreateMap<UserSearchModel, User>().ReverseMap();
            CreateMap<Company, RegisterCompanyModel>().ReverseMap();
            CreateMap<Company, CompanyEmployeeModel>().ReverseMap();
            

        }
    }
}