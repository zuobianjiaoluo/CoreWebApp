using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;

namespace JwtWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ValuesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // GET api/values
        [HttpGet]
        [Authorize]//添加Authorize标签，可以加在方法上，也可以加在类上
        public ActionResult<IEnumerable<string>> GetInfo()
        {
            return new string[] { "valfffffffffffffffffffffffffue1", "vasdddddddddddddddlue2" };
        }

        public string GetUser(string access_token)
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + access_token);

            return "";
        }
    }
}
