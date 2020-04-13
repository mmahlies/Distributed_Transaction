using MedicalAPI.Filter;
using MedicalEF6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MedicalAPI.Controllers
{
    public class ValuesController : ApiController
    {
      
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }


       [TransactionFilter]

        // POST api/values
        public void Post([FromBody]string token)
        {
            MedicalLogic.Logic();
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
