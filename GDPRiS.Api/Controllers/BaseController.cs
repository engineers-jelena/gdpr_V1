using GDPRiS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using GDPRiS.Api.Models.User;

namespace GDPRiS.Api.Controllers
{
    public class BaseController : ApiController
    {
        internal UserJwtModel CurrentUser { get; set; }

        private UserManager userManager;
        protected UserManager UserManager => userManager ?? (userManager = new UserManager());

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
        }
    }
}