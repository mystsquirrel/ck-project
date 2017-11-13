using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Helpers
{
    public static class LoginHelper
    {
        public static bool IsAdmin(this ViewUserControl pg)
        {
            return pg.Page.User.IsInRole("Administrator");
        }
    }
}