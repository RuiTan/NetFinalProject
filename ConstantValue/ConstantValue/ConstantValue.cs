using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetFinalProject.Service
{
    public class ConstantValue
    {
        public static int SessionExisted = -1;
        public static int SessionNotExisted = -1;
        public static int UserNotExist = -2;
        public static int WrongPwd = -3;
        public static int UserExist = -4;
        public static int LoginSucceed = 1;
        public static int RegisterSucceed = 2;

        public static int MovieNotFound = -1;
        public static string Genres = "genres";
        public static string Pictures = "pictures";
        public static string Regions = "regions";
        public static string Stars = "stars";
        public static string Directors = "directors";
        public static string Poster = "poster";
        public static string Rate = "rate";
        public static string Likes = "likes";

        public static int NoScore = 0;

    }
}