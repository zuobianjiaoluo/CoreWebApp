using HttpWeb.Application;
using HttpWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpWeb.Controllers
{
    public class HttpClient_Controller : Controller
    {
        private IHttpClientService _client;

        public HttpClient_Controller(IHttpClientService client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            string access_token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQW5nZWxhRGFkZHkiLCJleHAiOjE1NTQyMDAxNTYsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTUwMCxodHRwOi8vbG9jYWxob3N0OjU2MDAifQ.xYpkRah-3jDg9OmTvy_b-LvcZgt8gpvjuTonKJZd_YU";
            string Host23 = "http://localhost:5500/api/values/PostInfo";//Jwt项目-带有token参数的获取用户信息接口

            var results = await _client.PostAsync(Host23, "eeeeeee", access_token);
            var str = await SerializeResponseAsync(results);
            if (results.IsSuccessStatusCode)
            {
                str = await results.Content.ReadAsStringAsync();//获取响应体内容
            }
            else
            {
                var sstr = await results.Content.ReadAsStringAsync();
                str = results.StatusCode.ToString();
            }



            string strHtml = "";
            string Host26 = "https://localhost:5050/Home/Login";// 

            User user = new User();
            user.name = "admin";
            user.pwd = "123456";
            var result = await _client.PostAsync(Host26, user, access_token);
            strHtml = await SerializeResponseAsync(result);



            //if (result.IsSuccessStatusCode)
            //{
            //    str = await result.Content.ReadAsStringAsync();
            //}
            return Json(str + "-------------" + strHtml);
        }


        public static async Task<string> SerializeResponseAsync(HttpResponseMessage response)
        {
            var builder = new StringBuilder();
            var content = await ReadContentAsStringAsync(response.Content);

            builder.AppendFormat("HTTP/{0} {1:d} {1}", response.Version.ToString(2), response.StatusCode);
            builder.AppendLine();
            builder.Append(response.Headers);

            if (!string.IsNullOrWhiteSpace(content))
            {
                builder.Append(response.Content.Headers);

                builder.AppendLine();
                builder.AppendLine(content);
            }

            return builder.ToString();
        }

        private static Task<string> ReadContentAsStringAsync(HttpContent content)
        {
            return content == null ? Task.FromResult<string>(null) : content.ReadAsStringAsync();
        }




        public static T PutAsync<T>(HttpResponseMessage responseMessage)
        {

            responseMessage.EnsureSuccessStatusCode();
            var responseData = responseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(responseData);

        }
    }


    public class Rootobject
    {
        public Version version { get; set; }
        public Content content { get; set; }
        public int statusCode { get; set; }
        public string reasonPhrase { get; set; }
        public object[] headers { get; set; }
        public Requestmessage requestMessage { get; set; }
        public bool isSuccessStatusCode { get; set; }
    }

    public class Version
    {
    }

    public class Content
    {
    }

    public class Requestmessage
    {
        public Version1 version { get; set; }
        public object content { get; set; }
        public Method method { get; set; }
        public string requestUri { get; set; }
        public object[] headers { get; set; }
        public Properties properties { get; set; }
    }

    public class Version1
    {
    }

    public class Method
    {
        public string method { get; set; }
    }

    public class Properties
    {
    }

}