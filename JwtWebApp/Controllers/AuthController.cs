using JWT;
using JWT.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JwtWebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        public bool CanValidateToken => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityToken"></param>
        /// <returns></returns>
        public bool CanReadToken(string securityToken)
        {
            return true;
        }


        private JwtSettings _jwtSettings;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public AuthController(IConfiguration configuration, IOptions<JwtSettings> _jwtSettingsAccesser)
        {
            _configuration = configuration;
            _jwtSettings = _jwtSettingsAccesser.Value;
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="Username">用户名</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequestToken([FromBody]UserInfo userInfo)
        {
            if (userInfo.Username == "AngelaDaddy" && userInfo.Password == "123456")
            {
                //将用户的名称放入声明中，以便稍后识别该用户。
                var claims = new[]
                {
                   new Claim(ClaimTypes.Name, userInfo.Username)
               };
                //使用密钥对令牌进行签名。此机密将在您的API和任何需要检查令牌是否合法的内容之间共享。
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //.NET Core’s JwtSecurityToken 类承担了沉重的负担，并实际创建了令牌。
                /**
                 * Claims (Payload)
                    Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:

                    iss: The issuer of the token，token 发行人
                    audience:听众
                    sub: The subject of the token，token 主题
                    exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                    iat: Issued At。 token 创建时间， Unix 时间戳格式
                    jti: JWT ID。针对当前 token 的唯一标识
                    除了规定的字段外，可以包含其他任何 JSON 兼容的字段。



                 * */
                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return BadRequest("Could not verify username and password");
        }

        /// <summary>
        /// 解析JWT
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DecryptToken(string token)
        {
            //两种转换方式

            //第一种
            var token1 = new JwtSecurityToken(token);
            //获取到Token的一切信息
            var payload = token1.Payload;
            var role = (from t in payload where t.Key == ClaimTypes.Role select t.Value).FirstOrDefault();
            var name = (from t in payload where t.Key == ClaimTypes.Name select t.Value).FirstOrDefault();
            var issuer = token1.Issuer;
            var key = token1.SecurityKey;
            var audience = token1.Audiences;


            //第二种，获取不到登录用户名
            var json = "";
            var secret = _jwtSettings.SecretKey;
            try
            {
                json = new JwtBuilder().WithSecret(secret).MustVerifySignature().Decode(token);

                var jsons = JsonConvert.DeserializeObject<JwtResponse>(json);
                return Ok(jsons.ToString());
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
            }

            return Ok(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityToken"></param>
        /// <param name="validatedToken"></param>
        /// <returns></returns>
        [HttpGet]
        public ClaimsPrincipal ValidateToken(string securityToken, out SecurityToken validatedToken)
        {
            ClaimsPrincipal principal;
            try
            {
                validatedToken = null;
                //这里需要验证生成的Token
                /*
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoid2FuZ3NoaWJhbmciLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiwgTWFuYWdlIiwibmJmIjoxNTIyOTI0MDgxLCJleHAiOjE1MjI5MjU4ODEsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.fa0jDYt_MqHFcwQfsMS30eCsjEwQt_uiv96bGtMQJBE
                */
                var token = new JwtSecurityToken(securityToken);
                //获取到Token的一切信息
                var payload = token.Payload;
                var role = (from t in payload where t.Key == ClaimTypes.Role select t.Value).FirstOrDefault();
                var name = (from t in payload where t.Key == ClaimTypes.Name select t.Value).FirstOrDefault();
                var issuer = token.Issuer;
                var key = token.SecurityKey;
                var audience = token.Audiences;
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, name.ToString()));
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"));
                principal = new ClaimsPrincipal(identity);
            }
            catch
            {
                validatedToken = null;
                principal = null;
            }
            return principal;
        }
    }

}
