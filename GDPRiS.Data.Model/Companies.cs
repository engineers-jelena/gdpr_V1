using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDPRiS.Data.Model
{
    public class Companies : BaseModel
    {

        public string nameOfCompany { get; set; }

        public virtual List<Employees> Employees { get; set; }

    }
}
