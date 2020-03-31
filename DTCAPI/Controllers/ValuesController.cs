using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DTCAPI.Controllers
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

        // POST api/values
        public bool Post([FromBody] DtcValues dtcValues)
        {
            DTC.Dtc dtc = new DTC.Dtc();
           bool result = dtc.SyncDTCTransaction(dtcValues.Token, dtcValues.Sql);
            return result;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] DtcValues dtcValues)
        {
            
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
