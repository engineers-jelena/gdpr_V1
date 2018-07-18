using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDPRiS.Api.Models.Company
{
    public class CompanyEmployeeModel
    {

        public List<string> NameEmployee { get; set; }

        public int idCompany { get; set; }
    }
}