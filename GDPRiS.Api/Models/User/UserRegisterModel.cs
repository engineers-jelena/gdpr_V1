using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDPRiS.Api.Models.User
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Email is required!")]
        [StringLength(50, ErrorMessage = "Email too long (max 50 characters)!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(50, ErrorMessage = "Password too long (max 50 characters)!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required!")]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [StringLength(50, ErrorMessage = "First name too long (max 50 characters)!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        [StringLength(50, ErrorMessage = "Username too long (max 50 characters)!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(50, ErrorMessage = "Last name too long (max 50 characters)!")]
        public string LastName { get; set; }

        public int? GroupId { get; set; }

        public int? VenueId { get; set; }
    }
}