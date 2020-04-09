using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private DbContext _BackOfficeDBContext;
        private Itest _test;
        public ValuesController(DbContext BackOfficeDBContext, Itest test)
        {
            _BackOfficeDBContext = BackOfficeDBContext;
          //  test.Value = 1;
            this._test = test;
            var x = "";
        }
        // GET api/values
     
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

           
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }


        [ServiceFilter(typeof(TransactionFilter))]

        // POST api/values
        [HttpPost]
        public void Post([FromBody]  string token)
        {

            var x = this._test;
            BackOffice.BackOffice backOffice = new BackOffice.BackOffice();
            backOffice.Logic(_BackOfficeDBContext);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
