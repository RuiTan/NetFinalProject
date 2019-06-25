using NetFinalProject.Models;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NetFinalProject.Service
{
    public class CommentService
    {

        public static string tocken()
        {
            string AK = "LIjKmW98gcj0ahR_Fogx3AV8V1JsRtOQo6qT43Cu";
            string SK = "W_lPo12yjHkaVyfibquVSJH8ImLWXw-tIXeggNSo";
            Mac mac = new Mac(AK, SK);
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = "guitoubing";
            putPolicy.SetExpires(3600);
            string tocken = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            return tocken;
        }

        public static Dictionary<string, object> upload(int movieId, string title, string intro, string cover, string content)
        {
            int userId = SessionService.getSession();
            MovieEntities movieEntities = new MovieEntities();
            Comment comment = new Comment();
            comment.movie_id = movieId;
            comment.user_id = userId;
            comment.title = title;
            comment.intro = intro;
            comment.cover = cover;
            comment.content = content;
            comment.likes = 0;
            comment.comments = 0;
            comment.time = DateTime.Now;
            movieEntities.Comment.Add(comment);
            movieEntities.SaveChanges();
            return ClassToDictionary.classToDictionary(comment);
        }

        public static LinkedList<Dictionary<string, object>> list(int movieId)
        {
            MovieEntities movieEntities = new MovieEntities();
            LinkedList<Dictionary<string, object>> result = new LinkedList<Dictionary<string, object>>();
            var comments = movieEntities.Comment.Where(c => c.movie_id == movieId);
            var movieName = new MovieEntities().Movie.Where(m => m.id == movieId).FirstOrDefault().name;
            foreach (var c in comments)
            {
                User user = new MovieEntities().User.Where(u => u.id == c.user_id).FirstOrDefault();
                Dictionary<string, object> comment = ClassToDictionary.classToDictionary(c);
                comment.Add("username", user.name);
                comment.Add("email", user.email);
                comment.Add("movieName", movieName);
                result.AddLast(comment);
            }
            return result;
        }

        public static LinkedList<Dictionary<string, object>> listByPage(int page)
        {
            MovieEntities movieEntities = new MovieEntities();
            LinkedList<Dictionary<string, object>> result = new LinkedList<Dictionary<string, object>>();
            Array comments = movieEntities.Comment.ToArray();
            int size = comments.Length;
            //foreach (var comment in comments)
            //    size++;
            int start = page * 6;
            int end = (page + 1) * 6;
            if (size < start)
            {
                return null;
            }
            else if(size >= start && size < end)
            {
                for(int i = start; i < size; i++)
                {
                    Comment comment = (Comment)comments.GetValue(index: i);
                    User user = new MovieEntities().User.Where(u => u.id == comment.user_id).FirstOrDefault();
                    Movie movie = new MovieEntities().Movie.Where(m => m.id == comment.movie_id).FirstOrDefault();
                    var temp = ClassToDictionary.classToDictionary(comment);
                    temp.Add("username", user.name);
                    temp.Add("email", user.email);
                    temp.Add("movieName", movie.name);
                    result.AddLast(temp);
                }
                return result;
            }
            else
            {
                for (int i = start; i < end; i++)
                {
                    result.AddLast(ClassToDictionary.classToDictionary(comments.GetValue(i)));
                }
                return result;
            }
        }

        public static LinkedList<Dictionary<string, object>> listByUser(int userId)
        {
            MovieEntities movieEntities = new MovieEntities();
            LinkedList<Dictionary<string, object>> result = new LinkedList<Dictionary<string, object>>();
            var comments = movieEntities.Comment.Where(c => c.user_id == userId);
            User user = movieEntities.User.Where(u => u.id == userId).FirstOrDefault();
            foreach(var c in comments)
            {
                Dictionary<string, object> comment = ClassToDictionary.classToDictionary(c);
                Movie movie = new MovieEntities().Movie.Where(m => m.id == c.movie_id).FirstOrDefault();
                comment.Add("username", user.name);
                comment.Add("email", user.email);
                comment.Add("movieName", movie.name);
                result.AddLast(comment);
            }
            return result;
        }


    }
}