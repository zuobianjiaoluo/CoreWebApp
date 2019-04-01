using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace JwtWebApp
{
    public class Txt
    {

        public string GetJson(string webRootUrl)
        {
            string json = "";

            string url = webRootUrl + "\\beikejson" + @"\json.txt";
            StreamReader sr = new StreamReader(url, false);
            string[] yy = sr.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToArray(); //StringSplitOptions.RemoveEmptyEntries 分割后内容为空则不返回。.EndsWith("strat")取以“strat”结尾的结果

            foreach (var item in yy)
            {
                json += item + "---";
            }








            //将结果写入文本
            //StreamWriter sw = File.AppendText(@"E:/timeoutE.TXT");
            //sw.Write(string.Join("\r\n", yy));
            //sw.Close();

            //string strVer = sr.ReadLine().ToString();//读取 一行
            sr.Close();

            return json;
        }
    }
}
