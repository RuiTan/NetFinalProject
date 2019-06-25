using NetFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFinalProject.Service
{
    public class SearchService
    {

        public static LinkedList<Dictionary<string, object>> exactSearch(string name, string genre, string region, string director, string star)
        {
            LinkedList<Dictionary<string, object>> result = new LinkedList<Dictionary<string, object>>();
            var movies = new MovieEntities().Movie.Where(m => m.name.Contains(name) && m.genres.Contains(genre) && m.regions.Contains(region) && m.stars.Contains(director) && m.directors.Contains(star));
            foreach (var movie in movies)
            {
                result.AddLast(ClassToDictionary.movieToDictionary(movie));
            }
            return result;
        }

        public static LinkedList<Dictionary<string, object>> fuzzySearch(string param)
        {
            LinkedList<Dictionary<string, object>> result = new LinkedList<Dictionary<string, object>>();
            var movies = new MovieEntities().Movie.Where(m => m.name.Contains(param) || m.genres.Contains(param) || m.regions.Contains(param) || m.directors.Contains(param) || m.stars.Contains(param));
            foreach (var movie in movies)
            {
                result.AddLast(ClassToDictionary.movieToDictionary(movie));
            }
            return result;
        }

    }
}
