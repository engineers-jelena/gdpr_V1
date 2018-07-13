using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDPRiS.Api.Models.User
{
    public class UserModel: BaseUserModel
    {
        public int Id { get; set; }

        public DateTime RegistrationDate { get; set; }

        public bool IsAdmin { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
    }
}