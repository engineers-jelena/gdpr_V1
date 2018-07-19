using GDPRiS.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDPRiS.Data.Model
{
    public class Cars : BaseModel
    {
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public virtual Employees Employee { get; set; }

        public MarkCar MarkOfCar { get; set; }

    }
}
