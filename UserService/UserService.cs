using NetFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NetFinalProject.Service
{
    public class UserService
    {
        public static Dictionary<string, object> Login(string email, string pwd)
        {
            MovieEntities movieEntities = new MovieEntities();
            Dictionary<string, object> result = new Dictionary<string, object>();
            User user = movieEntities.User.Where(u => u.email.Equals(email)).FirstOrDefault();
            if (user == null)
            {
                result.Add("type", ConstantValue.UserNotExist);
                return result;
            }
            else
            {
                if (user.password.Equals(pwd))
                {
                    result.Add("type", ConstantValue.LoginSucceed);
                    result.Add("id", user.id);
                    HttpContext.Current.Session["user"] = user.id;
                    return result;
                }
                else
                {
                    result.Add("type", ConstantValue.WrongPwd);
                    return result;
                }
            }

        }

        public static Dictionary<string, object> Register(string email, string pwd, string name)
        {
            MovieEntities movieEntities = new MovieEntities();
            Dictionary<string, object> result = new Dictionary<string, object>();
            User user = movieEntities.User.Where(u => u.email.Equals(email)).FirstOrDefault();
            if (user == null)
            {
                user = new User
                {
                    email = email,
                    password = pwd,
                    name = name
                };
                movieEntities.User.Add(user);
                movieEntities.SaveChanges();
                HttpContext.Current.Session["user"] = user.id;
                result.Add("type", ConstantValue.RegisterSucceed);
                result.Add("id", movieEntities.User.Where(u => u.email.Equals(email)).FirstOrDefault().id);
                return result;
            }
            else
            {
                result.Add("type", ConstantValue.UserExist);
                return result;
            }
        }

        public static Dictionary<string, object> getUserInfo(int id)
        {
            MovieEntities movieEntities = new MovieEntities();
            User user = movieEntities.User.Where(u => u.id == id).FirstOrDefault();
            return ClassToDictionary.classToDictionary(user);
        }

        public static int getSession()
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
