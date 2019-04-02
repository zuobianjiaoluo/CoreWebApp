using BeetleX.FastHttpApi;
using HttpWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HttpWeb.Controllers
{
    public class HomeController : Controller
    {
        public DateTime GetTime()
        {
            return DateTime.Now;
        }
        public IActionResult Hello(string name)
        {
            return new Microsoft.AspNetCore.Mvc.JsonResult($"hello {name}");
        }

        
        [HttpPost]
        public bool Login([CHeader(name: "Authorization", value: "Bearer SDfsdfsdfsdfsdfsdfsdf")][FromBody]User user)
        {
            if (user.name == "admin" && user.pwd == "123456")
                return true;
            return false;
        }
    }
}
