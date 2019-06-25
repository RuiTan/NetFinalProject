using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.InteropServices;

namespace NetFinalProject.Controllers
{
    public class ValuesController : ApiController
    {
        [DllImport("TIMEUTIL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?FormatTime@@YAPADH@Z")]
        extern static string FormatTime(int id);

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public int Get(int id)
        {
            //int a = new ClassLibrary1.Class1().Add(1, 2);
            //return new Test().Add(id);
            return 1;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
