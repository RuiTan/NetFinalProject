using NetFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetFinalProject.Service
{
    public class SessionService
    {
        public static bool checkUser(object session)
        {
            if (session == null)
                return true;
            return false;
        }

        public static int getSession()
        {
            var session = HttpContext.Current.Session["user"];
            if (session == null) session = 1;
            return (int)session;
        }
    }
}