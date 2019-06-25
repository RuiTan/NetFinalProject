using NetFinalProject.Models;
using NetFinalProject.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace NetFinalProject.Controllers
{
    public class MovieController : ApiController
    {


        [HttpGet]
        [Route("api/movie/detail")]
        public Dictionary<string, object> getMovies(int id)
        {
            return MovieService.getMovies(id);
        }

        [HttpGet]
        [Route("api/movie/getRate")]
        public int getRate(int movieId)
        {
            return MovieService.getRate(movieId);
        }

        [HttpGet]
        [Route("api/movie/rate")]
        public Dictionary<string, object> rate(int movieId, int score)
        {
            return MovieService.rate(movieId, score);
        }
    }
}
