using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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

        // GET api/values
        [HttpGet]
        public IActionResult GetJson()
        {
            
            Txt txt = new Txt();
            var json =txt.GetJson(_hostingEnv.WebRootPath);
            return Ok(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public string GetUser(string access_token)
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + access_token);
            var url2 = new Uri("http://api.github.com");
            var response2 = Get(url2).Result;
            return "";
        }

        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<string> PostAsync(Uri uri, string content)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var clientAddress = uri.GetLeftPart(UriPartial.Authority);
                client.BaseAddress = new Uri(clientAddress);
                var conten = new StringContent(content, Encoding.UTF8, "application/json");
                var uriAbsolutePath = uri.AbsolutePath;
                var response = await client.PostAsync(uriAbsolutePath, conten);
                var responseJson = response.Content.ReadAsStringAsync().Result;
                return responseJson;
            }
        }

        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<string> Get(Uri uri)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                try
                {
                    var clientAddress = uri.GetLeftPart(UriPartial.Authority);
                    var BaseAddress = new Uri(clientAddress);
                    client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Test");
                    // var conten = new StringContent(content, Encoding.UTF8, "application/json");
                    var result = await client.GetAsync(clientAddress);
                    var responseJson = result.Content.ReadAsStringAsync().Result;
                    return responseJson;
                }
                catch (Exception ex)
                {

                    return ex.Message;
                }

            }
        }

    }
}
