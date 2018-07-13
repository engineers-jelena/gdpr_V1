using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GDPRiS.Common.Enums;

namespace GDPRiS.Api.Models.User
{
    public class UserJwtModel
    {
        public int Id { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Role Role { get; set; }
    }
}