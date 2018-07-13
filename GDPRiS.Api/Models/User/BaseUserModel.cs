using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDPRiS.Api.Models.User
{
    public abstract class BaseUserModel
    {
        [Required(ErrorMessage = "Email is required!")]
        [StringLength(50, ErrorMessage = "Email too long (max 50 characters)!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [StringLength(50, ErrorMessage = "First name too long (max 50 characters)!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(50, ErrorMessage = "Last name too long (max 50 characters)!")]
        public string LastName { get; set; }
    }
}