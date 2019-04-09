using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebRedis.Logic;

namespace WebRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            RedisHelper redisHelper = new RedisHelper("127.0.0.1:6379");
            string value = "abcdefg";
            bool r1 = redisHelper.SetValue("mykey", value);
            string saveValue = redisHelper.GetValue("mykey");
            bool r2 = redisHelper.SetValue("mykey", "NewValue");
            saveValue = redisHelper.GetValue("mykey");
            //bool r3 = redisHelper.DeleteKey("mykey");  删除
            string uncacheValue = redisHelper.GetValue("mykey");



            return new string[] { uncacheValue, "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
