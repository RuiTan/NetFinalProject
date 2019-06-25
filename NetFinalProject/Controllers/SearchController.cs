using NetFinalProject.Models;
using NetFinalProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NetFinalProject.Controllers
{
    public class SearchController : ApiController
    {
        [HttpGet]
        [Route("api/search/exactSearch")]
        public LinkedList<Dictionary<string, object>> exactSearch(string name, string genre, string region, string director, string star)
        {
            return SearchService.exactSearch(name, genre, region, director, star);
        }

        [HttpGet]
        [Route("api/search/fuzzySearch")]
        public LinkedList<Dictionary<string, object>> fuzzySearch(string param)
        {
            return SearchService.fuzzySearch(param);
        }
    }
}
