using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using GDPRiS.Api.Helpers;
using System.Web.Http;
using GDPRiS.Data.Model;
using System.Net.Mail;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http.Results;
using AutoMapper;
using GDPRiS.Api.Models.User;
using GDPRiS.Common.Enums;
using GDPRiS.Common.Exceptions;
using System.ComponentModel.DataAnnotations;
using GDPRiS.Api.Models.Company;

namespace GDPRiS.Api.Controllers
{
    public class CompanyController : BaseController
    {

        #region Company Register

        
        [ValidateModel]
        [HttpPost]
        public object Register(RegisterCompanyModel model)
        {
            Companies company = new Data.Model.Companies {nameOfCompany = model.nameOfCompany };
            Companies registeredCompany = CompanyManager.Register(company);
            RegisterCompanyModel modelNew = Mapper.Map<RegisterCompanyModel>(company);

            return new {modelNew};
        }

        #endregion

        #region Company Delete

        [AllowAnonymous]
        [HttpGet]
        public bool DeleteCompany(int companyId)
        {
            CompanyManager.DeleteCompanyById(companyId);
            return true;
        }

        #endregion

        #region

        [AllowAnonymous]
        [HttpPost]
        public CompanyEmployeeModel AddEmployees(CompanyEmployeeModel model)
        {
            Companies companyDb = CompanyManager.CompanyAddPEmployee(model.idCompany, model.NameEmployee);
            CompanyEmployeeModel viewModel = Mapper.Map<CompanyEmployeeModel>(companyDb);
            viewModel.NameEmployee = companyDb.Employees.Select(u => u.NameOfEmployee).ToList();

            return viewModel;
        }

        #endregion

    }
}