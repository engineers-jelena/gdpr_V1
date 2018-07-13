using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDPRiS.Api.Models.User
{
    public class UsersPhoneModel
    {
        public List<string> PhoneNumbers { get; set; }

        public int UserId { get; set; }

    }
}