using GDPRiS.Common.Exceptions;
using GDPRiS.Common.Helpers;
using GDPRiS.Data;
using GDPRiS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDPRiS.Common.Enums;
using System.Net.Http;
using System.Net.Http.Headers;

//using System.Net.Http.;

namespace GDPRiS.Core
{
    public class CompanyManager
    {

        #region Register Company

        public Company Register(Company company)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                company.DateModified = DateTime.UtcNow;
                company.DateCreated = DateTime.UtcNow;
                uow.CompanyRepository.Insert(company);
                uow.Save();
                 uow.CompanyRepository.GetById(company.id);
                return company;
            }
        }

        #endregion

        #region Delete Company


        public void DeleteCompanyById(int companyId)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Company company = uow.CompanyRepository.GetById(companyId);
                ValidationHelper.ValidateNotNull(company);

                company.DateDeleted = DateTime.UtcNow;
                uow.CompanyRepository.Update(company);

                uow.Save();
            }
        }

        #endregion

        #region add Employee


        public Company CompanyAddPEmployee(int companyId, List<string> employees)
        {

            using (UnitOfWork uow = new UnitOfWork())
            {
                Company companyDb = uow.CompanyRepository.Find(u => u.id == companyId).FirstOrDefault();
                ValidationHelper.ValidateNotNull(companyDb);

                DateTime now = DateTime.UtcNow;

                foreach (string employee in employees)
                {
                    Employee newEmployee = new Employee { DateCreated = now, DateModified = now, NameOfEmployee = employee, CompanyId = companyId };
                    uow.EmployeeRepository.Insert(newEmployee);
                }

                uow.Save();

                return companyDb;
            }
        }


        #endregion


    }
}
