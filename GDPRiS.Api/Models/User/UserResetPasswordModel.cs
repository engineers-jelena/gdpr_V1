using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDPRiS.Api.Models.User
{
    public class UserResetPasswordModel
    {
        public string ConfirmHash { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(50, ErrorMessage = "Password too long (max 50 characters)!")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required!")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
    }
}