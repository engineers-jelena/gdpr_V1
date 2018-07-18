using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDPRiS.Api.Models.Company
{
    public class EmployeeCarModel
    {
        public List<string> CarTypes { get; set; }

        public int idEmployee { get; set; }
    }
}