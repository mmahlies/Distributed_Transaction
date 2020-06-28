using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace BackOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private DbContext _BackOfficeDBContext;
        private IConfiguration _iConfig;

        public ValuesController(DbContext BackOfficeDBContext, IConfiguration iConfig)
        {
            _BackOfficeDBContext = BackOfficeDBContext;
            _iConfig = iConfig;


        }
        // GET api/values
     
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            var xx = ConfigurationManager.AppSettings.AllKeys;
          var x=   ConfigurationManager.GetSection("Modules:Logging:logDb"); ;
         
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }


        // POST api/values
        [ServiceFilter(typeof(TransactionFilter))]
        [HttpPost]
        public void Post([FromBody]  string token)
        {

      
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
