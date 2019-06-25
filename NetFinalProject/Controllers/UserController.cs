using NetFinalProject.Models;
using NetFinalProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace NetFinalProject.Controllers
{
    public class UserController : ApiController
    {
        // Get: api/user/login
        [HttpGet]
        [Route("api/user/login")]
        public Dictionary<string, object> Login(string email, string pwd)
        {
            return UserService.Login(email, pwd);
        }

        // Get: api/user/register
        [HttpGet]
        [Route("api/user/register")]
        public Dictionary<string, object> Register(string email, string pwd, string name)
        {
            return UserService.Register(email, pwd, name);
        }

        [HttpGet]
        [Route("api/user/userInfo")]
        public Dictionary<string, object> getUserInfo(int id)
        {
            return UserService.getUserInfo(id);
        }

        [HttpGet]
        [Route("api/user/session")]
        public int getSession()
        {
            var session = HttpContext.Current.Session["user"];
            
            if (session == null)
            {
                return ConstantValue.SessionNotExisted;
            }
            else
            {
                return (int)session;
            }
        }
    }
}
