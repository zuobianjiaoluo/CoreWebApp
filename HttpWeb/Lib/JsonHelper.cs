using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpWeb.Lib
{
    public class JsonHelper
    {
        public static HttpResponseMessage toJson(Object obj)
        {
            String str;
            if (obj is String || obj is Char)//如果是字符串或字符直接返回
            {
                str = obj.ToString();
            }
            else//否则序列为json字串
            {
                str = JsonConvert.SerializeObject(obj);
                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //str = serializer.Serialize(obj);
            }
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }
    }
}
