using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
namespace WebApp.Controllers
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using WebApp.lib;

    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {

            var ip = IPClass.GetGatewayIp();
            var mac = IPClass.GetMacByArp(ip);


            var startDate = DateTime.Now;
            var client=_httpClientFactory.CreateClient();
            var str = "{ \"areaCode\":\"B025\",\"realtyid\":1772109}";
            var url = new Uri("http://172.16.1.121:63310/api/RealtyFiles/GetRealtyFiles");
            var response=PostAsync(url,str).Result;

            var url2 = new Uri("http://api.github.com");
            var response2=Get(url2).Result;

            //var client2 = _httpClientFactory.CreateClient();
            //var BaseAddress = new Uri("http://api.github.com");
            //string result = client2.GetStringAsync(BaseAddress).Result;

            var ms = (DateTime.Now - startDate).TotalMilliseconds;
            ViewBag.a="耗时:" + ms + "ms";
            return View();
        }



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

        /// <summary>
        /// 发起POST异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="contentType">application/xml、application/json、application/text、application/x-www-form-urlencoded</param>
        /// <param name="headers">填充消息头</param>        
        /// <returns></returns>
        [NonAction]
        public async Task<string> HttpPostAsync(string url, string postData = null, string contentType = null, int timeOut = 30, Dictionary<string, string> headers = null)
            {
                postData = postData ?? "";
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, timeOut);
                    if (headers != null)
                    {
                        foreach (var header in headers)
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8))
                    {
                        if (contentType != null)
                            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

                        HttpResponseMessage response = await client.PostAsync(url, httpContent);
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
        

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
