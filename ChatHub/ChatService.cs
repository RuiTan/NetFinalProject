using NetFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetFinalProject.Service
{
    public class ChatService
    {

        public static Record save(int userId, int movieId, string content)
        {
            Record record = new Record
            {
                user_id = userId,
                movie_id = movieId,
                time = DateTime.Now,
                content = content
            };
            MovieEntities movieEntities = new MovieEntities();
            movieEntities.Record.Add(record);
            movieEntities.SaveChanges();
            return record;
        }

        public static LinkedList<Dictionary<string, object>> list(int movieId)
        {
            MovieEntities movieEntities = new MovieEntities();
            LinkedList<Dictionary<string, object>> result = new LinkedList<Dictionary<string, object>>();
            var records = movieEntities.Record.Where(r => r.movie_id == movieId);
            foreach (var r in records)
            {
                Dictionary<string, object> temp = ClassToDictionary.classToDictionary(r);
                temp.Add("username", new MovieEntities().User.Where(u=>u.id == r.user_id).FirstOrDefault().name);
                result.AddLast(temp);
            }
            return result;
        }
    }
}
