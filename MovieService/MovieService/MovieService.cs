using NetFinalProject.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetFinalProject.Service
{
    public class MovieService
    {
        public static void convertObjectToDictionary(Dictionary<string, object> result, Movie movie)
        {
            foreach (var m in JObject.FromObject(movie).ToObject<Dictionary<string, object>>())
            {
                if (m.Key.Equals(ConstantValue.Regions) || m.Key.Equals(ConstantValue.Genres) || m.Key.Equals(ConstantValue.Directors) || m.Key.Equals(ConstantValue.Stars))
                    splitBySlash(result, m);
                else if (m.Key.Equals(ConstantValue.Pictures))
                    splitBySemicolon(result, m);
                else
                    result.Add(m.Key, m.Value);
            }
        }

        private static void splitBySlash(Dictionary<string, object> result, KeyValuePair<string, object> o)
        {
            result.Add(o.Key, o.Value.ToString().Split('/'));
        }

        private static void splitBySemicolon(Dictionary<string, object> result, KeyValuePair<string, object> o)
        {
            string[] pictures = o.Value.ToString().Split(';');
            if (pictures.Length == 0)
            {
                result.Add(ConstantValue.Poster, null);
                result.Add(o.Key, null);
            }
            else if(pictures.Length == 1)
            {
                result.Add(ConstantValue.Poster, pictures[0]);
                result.Add(o.Key, null);
            }
            else
            {
                result.Add(ConstantValue.Poster, pictures[0]);
                string[] ps = new string[pictures.Length - 1];
                Array.Copy(pictures, 1, ps, 0, pictures.Length - 1);
                result.Add(o.Key, ps);
            }
        }

        public static void updateMovieRate(Movie movie, int score)
        {
            Nullable<float> totalRate = movie.rate * movie.likes;
            movie.rate = (totalRate + score) / (movie.likes + 1);
        }
        public static void updateMovieRate(Movie movie, Rate rate, int score)
        {
            Nullable<float> totalRate = movie.rate * movie.likes;
            movie.rate = (totalRate - rate.score + score) / (movie.likes);
        }

        public static Dictionary<string, object> getMovies(int id)
        {
            MovieEntities movieEntities = new MovieEntities();
            Dictionary<string, object> result = new Dictionary<string, object>();
            Movie movie = movieEntities.Movie.Where(m => m.id == id).FirstOrDefault();
            if (movie == null)
            {
                result.Add("error", ConstantValue.MovieNotFound);
                return result;
            }
            else
            {
                MovieService.convertObjectToDictionary(result, movie);
                return result;
            }
        }
        public static int getRate(int movieId)
        {
            MovieEntities movieEntities = new MovieEntities();
            int userId = SessionService.getSession();
            Movie movie = movieEntities.Movie.Where(m => m.id == movieId).FirstOrDefault();
            Rate rate = movieEntities.Rate.Where(r => r.movie_id == movieId && r.user_id == userId).FirstOrDefault();
            if (rate == null)
            {
                return ConstantValue.NoScore;
            }
            else
            {
                return rate.score;
            }
        }
        public static Dictionary<string, object> rate(int movieId, int score)
        {
            MovieEntities movieEntities = new MovieEntities();
            int userId = SessionService.getSession();
            Movie movie = movieEntities.Movie.Where(m => m.id == movieId).FirstOrDefault();
            Rate rate = movieEntities.Rate.Where(r => r.movie_id == movieId && r.user_id == userId).FirstOrDefault();
            if (rate == null)
            {
                MovieService.updateMovieRate(movie, score);
                movie.likes += 1;
                Rate newRate = new Rate
                {
                    movie_id = movieId,
                    user_id = userId,
                    score = score
                };
                movieEntities.Rate.Add(newRate);
            }
            else
            {
                MovieService.updateMovieRate(movie, rate, score);
                if (score != rate.score)
                    rate.score = score;
            }
            movieEntities.SaveChanges();
            Dictionary<string, object> results = new Dictionary<string, object>
            {
                { ConstantValue.Rate, movie.rate },
                { ConstantValue.Likes, movie.likes }
            };
            return results;
        }
    }
}