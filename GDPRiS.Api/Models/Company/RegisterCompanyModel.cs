using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GDPRiS.Api.Models.Company
{
    public class RegisterCompanyModel
    {
       
        public int idCompany { get; set; }

        [Required(ErrorMessage = "Name of company is required!")]
        public string nameOfCompany { get; set; }

    }
}