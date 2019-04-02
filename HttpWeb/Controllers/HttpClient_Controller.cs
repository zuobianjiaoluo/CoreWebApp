using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HttpWeb.Application;
using HttpWeb.Lib;
using HttpWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            string access_token = "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee";

            string Host26 = "https://localhost:5050/Home/Login";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Authorization", "Bearer " + access_token);

            User user = new User();
            user.name = "admin";
            user.pwd = "123456";
            var result = await _client.PostAsync(Host26, user, access_token);
            var str = "";
            if (result.IsSuccessStatusCode)
            {
                str = await result.Content.ReadAsStringAsync();
            }
            return Json(str);
        }
    }
}