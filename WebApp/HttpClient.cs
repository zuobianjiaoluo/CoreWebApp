using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApp
{
    public class HttpClient
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public HttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        #region http请求代码


        public string PostAudioFileHandle()
        {
            var str = @"{ 'moduleName':'B024/test','file':'C:\Users\Administrator\Desktop\12.png','fileName':'20181129.jpg','clientInfo':'{'method':'add','containerName':'c21 - test'}'}";
            var url = new Uri("http://172.16.1.121:63310/api/RealtyFiles/GetRealtyFiles");
            var response = PostAsync(url, str).Result;
            return response;
        }

        /// <summary>
        /// 测试用
        /// </summary>
        /// <returns></returns>
        public string AudioFileHandle()
        {


            var str = "{ \"areaCode\":\"B025\",\"realtyid\":1772109}";
            var url = new Uri("http://10.0.0.203:8080/aliyun/oss/putObject");
            var response = PostAsync(url, str).Result;
            return response;
        }

        /// <summary>
        /// 测试用
        /// </summary>
        /// <returns></returns>
        public string GetAudioFileHandle()
        {
            var url = new Uri("http://api.github.com");
            var response = Get(url).Result;
            return response;
        }


        /// <summary>
        /// POST
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<string> PostAsync(Uri uri, string content)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                try
                {
                    var clientAddress = uri.GetLeftPart(UriPartial.Authority);
                    client.BaseAddress = new Uri(clientAddress);
                    //var conten = new StringContent(content, Encoding.UTF8, "application/json");
                    var conten = new StringContent(content, Encoding.UTF8, "multipart/form-data");

                    var uriAbsolutePath = uri.AbsolutePath;
                    var response = await client.PostAsync(uriAbsolutePath, conten);
                    var responseJson = response.Content.ReadAsStringAsync().Result;
                    return responseJson;
                }
                catch (Exception ex)
                {

                    return ex.Message;
                }

            }
        }

        /// <summary>
        /// GET
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
                    //将指定的标头及其值添加到System.Net.Http.Headers.HttpHeaders
                    client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Test");
                    client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                    client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
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

        #endregion
    }
}
