using JwtWebApp.Lib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JwtWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHostingEnvironment _hostingEnv;

        public ValuesController(IHostingEnvironment hostingEnv,IHttpClientFactory httpClientFactory)
        {
            _hostingEnv = hostingEnv;
            _httpClientFactory = httpClientFactory;
        }
        // GET api/values
        [HttpGet]
        [Authorize]//添加Authorize标签，可以加在方法上，也可以加在类上
        public ActionResult<IEnumerable<string>> GetInfo()
        {
            return new string[] { "valfffffffffffffffffffffffffue1", "vasdddddddddddddddlue2" };
        }

        [HttpPost]
        [Authorize]//添加Authorize标签，可以加在方法上，也可以加在类上
        public ActionResult<IEnumerable<string>> PostInfo([FromBody]string name)
        {
            return new string[] { name, "PostInfoneirong" };
        }

        // GET api/values
        [HttpGet]
        public IActionResult GetJson()
        {
            
            Txt txt = new Txt();
            var json =txt.GetOneJson(_hostingEnv.WebRootPath);
            return Ok(json);
        }


        [HttpGet]
        public IActionResult GetMongoDB()
        {
            Bar json =new Bar();
            MongoDBHelper mdbh = new MongoDBHelper();
            mdbh.AddUser();

            var lists = mdbh.GetUser();
            foreach (var item in lists)
            {
                json = item;
            }
            return Ok(json);
        }
        

    }
}
