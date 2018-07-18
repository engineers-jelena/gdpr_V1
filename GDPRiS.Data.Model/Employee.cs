using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDPRiS.Data.Model
{
    public class Employee : BaseModel
    {

        public string NameOfEmployee { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual List<Car> Cars { get; set; }

    }
}
