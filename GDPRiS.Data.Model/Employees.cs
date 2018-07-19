using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDPRiS.Data.Model
{
    public class Employees
    {
        [Key]
        public int EmployeeId { get; set; }
        public string NameOfEmployee { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateDeleted { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public virtual Companies Company { get; set; }

        //public virtual List<Car> Cars { get; set; }

    }
}
