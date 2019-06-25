using NetFinalProject.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetFinalProject.Service
{
    public class ClassToDictionary
    {
        public static Dictionary<string, object> classToDictionary(params object[] os)
        {
            Dictionary<string, object> results = new Dictionary<string, object>();
            foreach ( var o in os)
                foreach (var m in JObject.FromObject(o).ToObject<Dictionary<string, object>>())
                    results.Add(m.Key, m.Value);
            return results;
        }

        public static Dictionary<string, object> movieToDictionary(Movie movie)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            MovieService.convertObjectToDictionary(result, movie);
            return result;
        }
    }
}