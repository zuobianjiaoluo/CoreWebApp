using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtWebApp
{
    /// <summary>
/// 
/// </summary>
    public class JwtResponse
    {
        public string httpschemasxmlsoaporgws200505identityclaimsname { get; set; }
        public int exp { get; set; }
        public string iss { get; set; }
        public string aud { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

    }
}
