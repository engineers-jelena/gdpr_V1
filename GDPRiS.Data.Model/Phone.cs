using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GDPRiS.Data.Model
{
    public class Phone
    {
        
        [Key]
        public int Id { get; set; }

        public string PhoneNumber { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }

    }
}
