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

        public Companies Register(Companies company)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                company.DateModified = DateTime.UtcNow;
                company.DateCreated = DateTime.UtcNow;
                uow.CompanyRepository.Insert(company);
                uow.Save();
                 uow.CompanyRepository.GetById(company.Id);
                return company;
            }
        }

        #endregion

        #region Delete Company


        public void DeleteCompanyById(int companyId)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Companies company = uow.CompanyRepository.GetById(companyId);
                ValidationHelper.ValidateNotNull(company);

                company.DateDeleted = DateTime.UtcNow;
                uow.CompanyRepository.Update(company);

                uow.Save();
            }
        }

        #endregion

        #region add Employee


        public Companies CompanyAddPEmployee(int companyId, List<string> employees)
        {

            using (UnitOfWork uow = new UnitOfWork())
            {
                Companies companyDb = uow.CompanyRepository.Find(u => u.Id == companyId).FirstOrDefault();
                ValidationHelper.ValidateNotNull(companyDb);

                DateTime now = DateTime.UtcNow;

                foreach (string employee in employees)
                {
                    Employees newEmployee = new Employees { DateCreated = now, DateModified = now, NameOfEmployee = employee, CompanyId = companyId };
                    uow.EmployeeRepository.Insert(newEmployee);
                }

                uow.Save();

                return companyDb;
            }
        }


        #endregion


    }
}
