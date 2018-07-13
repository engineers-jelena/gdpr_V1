using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDPRiS.Common.Enums;

namespace GDPRiS.Data.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50, ErrorMessage = "Username too long (max 50 characters)!")]
        public string Username { get; set; }

        [Required, PasswordPropertyText, StringLength(300)]
        public string Password { get; set; }

        [Required, StringLength(50, ErrorMessage = "Email too long (max 50 characters)!")]
        public string Email { get; set; }

        [Required, StringLength(50, ErrorMessage = "First name too long (max 50 characters)!")]
        public string FirstName { get; set; }

        [Required, StringLength(50, ErrorMessage = "Last name too long (max 50 characters)!")]
        public string LastName { get; set; }

        [Required]
        public Role Role { get; set; }

        public bool IsActive { get; set; }
        public virtual List<Phone> Phones { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
