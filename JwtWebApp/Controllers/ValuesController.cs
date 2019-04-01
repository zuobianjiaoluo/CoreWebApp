using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;

namespace JwtWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHostingEnvironment _hostingEnv;

        public ValuesController(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
            //_httpClientFactory = httpClientFactory;
        }
        // GET api/values
        [HttpGet]
        [Authorize]//添加Authorize标签，可以加在方法上，也可以加在类上
        public ActionResult<IEnumerable<string>> GetInfo()
        {
            return new string[] { "valfffffffffffffffffffffffffue1", "vasdddddddddddddddlue2" };
        }

        // GET api/values
        [HttpGet]
        public IActionResult GetJson()
        {
            
               Txt txt = new Txt();
            var json =txt.GetJson(_hostingEnv.WebRootPath);


            return Ok(json);
        }
        


        //public string GetUser(string access_token)
        //{
        //    var client = _httpClientFactory.CreateClient();

        //    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + access_token);

        //    return "";
        //}
    }
}
