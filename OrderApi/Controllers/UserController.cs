using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace OrderApi.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> GetSex(string name)
        {
            if (name == "Jonathan")
                return "Man";
            return null;
        }

        [HttpGet]
        public ActionResult<int> GetAge(string name)
        {
            if (name == "Jonathan")
                return 24;
            return null;
        }
    }
}
