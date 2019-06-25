using NetFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetFinalProject.Service
{
    public class FeedbackService
    {
        public static List<Dictionary<string, object>> list(int commentId)
        {
            MovieEntities movieEntities = new MovieEntities();
            LinkedList<Dictionary<string, object>> result = new LinkedList<Dictionary<string, object>>();
            var feedbacks = movieEntities.Feedback.Where(f => f.comment_id == commentId).Where(f => f.reply_id == 0);
            foreach (var fb in feedbacks)
            {
                User user = new MovieEntities().User.Where(u => u.id == fb.user_id).FirstOrDefault();
                User at = fb.at_id == 0 ? null : new MovieEntities().User.Where(u => u.id == fb.at_id).FirstOrDefault();
                var f = ClassToDictionary.classToDictionary(fb);
                f.Add("username", user.name);
                f.Add("at_username", at == null ? null : at.name);
                LinkedList<Dictionary<string, object>> replies = new LinkedList<Dictionary<string, object>>();
                foreach (var subfb in new MovieEntities().Feedback.Where(sf => sf.reply_id == fb.id))
                {
                    var subf = ClassToDictionary.classToDictionary(subfb);
                    subf.Add("username", new MovieEntities().User.Where(u => u.id == subfb.user_id).FirstOrDefault().name);
                    subf.Add("at_username", new MovieEntities().User.Where(u => u.id == subfb.at_id).FirstOrDefault().name);
                    replies.AddLast(subf);
                }
                f.Add("replies", replies);
                result.AddLast(f);
            }
            return result.Reverse().ToList();
        }

        public static Dictionary<string, object> reply(int commentId, int atId, string content, int replyId)
        {
            MovieEntities movieEntities = new MovieEntities();
            int userId = SessionService.getSession();
            Feedback feedback = new Feedback();
            feedback.comment_id = commentId;
            feedback.user_id = userId;
            feedback.at_id = atId;
            feedback.reply_id = replyId;
            feedback.content = content;
            feedback.time = DateTime.Now;
            movieEntities.Feedback.Add(feedback);
            movieEntities.SaveChanges();
            return ClassToDictionary.classToDictionary(feedback);
        }

        public static Dictionary<string, object> like(int commentId)
        {
            MovieEntities movieEntities = new MovieEntities();
            int userId = SessionService.getSession();
            Like like = movieEntities.Like.Where(m => m.user_id == userId && m.comment_id == commentId).FirstOrDefault();
            Comment comment = movieEntities.Comment.Where(c => c.id == commentId).FirstOrDefault();
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (like == null)
            {
                Like newLike = new Like();
                newLike.user_id = userId;
                newLike.comment_id = commentId;
                movieEntities.Like.Add(newLike);
                comment.likes += 1;
                movieEntities.SaveChanges();
                result.Add("like", true);
                result.Add("likes", comment.likes);
            }
            else
            {
                movieEntities.Like.Remove(like);
                comment.likes -= 1;
                movieEntities.SaveChanges();
                result.Add("like", false);
                result.Add("likes", comment.likes);
            }
            return result;
        }

        public static Dictionary<string, object> getLike(int commentId)
        {
            MovieEntities movieEntities = new MovieEntities();
            int userId = SessionService.getSession();
            Like like = movieEntities.Like.Where(m => m.user_id == userId && m.comment_id == commentId).FirstOrDefault();
            Comment comment = movieEntities.Comment.Where(c => c.id == commentId).FirstOrDefault();
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("like", like != null);
            result.Add("likes", comment.likes);
            return result;
        }
    }
}