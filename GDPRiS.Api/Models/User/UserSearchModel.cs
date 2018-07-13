using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDPRiS.Api.Models.User
{
    public class UserSearchModel : BaseUserModel
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public List<string> Phones { get; set; }
    }
}