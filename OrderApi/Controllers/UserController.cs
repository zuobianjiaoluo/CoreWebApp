using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace OrderApi.Controllers
{
    /// <summary>
    /// 用户API
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 根据名称获取性别
        /// </summary>
        /// <param name="name">用户姓名</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> GetSex(string name)
        {
            if (name == "Jonathan")
                return "Man";
            return null;
        }

        /// <summary>
        /// 根据名称获取年龄
        /// </summary>
        /// <param name="name">用户姓名</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<int> GetAge(string name)
        {
            if (name == "Jonathan")
                return 24;
            return null;
        }

        /// <summary>
        /// 根据名称获取用户信息
        /// </summary>
        /// <param name="name">用户姓名</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<int> PostInfo([FromBody]string name)
        {
             return Ok(name);
        }

        
    }
}
