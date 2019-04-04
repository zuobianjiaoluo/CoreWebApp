using JwtWebApp.Lib;
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

        /// <summary>
        /// 全部读取txt内容循环输出
        /// </summary>
        /// <param name="webRootUrl"></param>
        /// <returns></returns>
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
            sr.Close();

            return json;
        }

        /// <summary>
        /// 单行读取循环输出
        /// </summary>
        /// <param name="webRootUrl"></param>
        /// <returns></returns>
        public string GetOneJson(string webRootUrl)
        {
            string json = "";
            string url = "";// webRootUrl + "\\beikejson" + @"\json.txt";


            //FtpHelper m_ftp = new FtpHelper();
            //string[] list = FtpHelper.GetFileList("/app/bsp");

            //foreach (var item in list)
            //{
            //    if (item.Contains(".txt"))
            //    {
            //       m_ftp.FtpDownload(item, webRootUrl);
            //        url = webRootUrl +"\\"+ item + @"\json.txt";
            //    }
            //}

            SFTPHelper sFTPHelper = new SFTPHelper("10.0.0.109", "22", "root", "c21sistest");
            string serverPath = "/app/bsp";
            
            var lists=sFTPHelper.GetFileList(serverPath, "txt");
            string webRootPath = webRootUrl + "\\beike\\";
            foreach (var item in lists)
            {
                sFTPHelper.Get(serverPath +"/"+item, webRootPath+item);
            }
            url = webRootPath;
            StreamReader sr = new StreamReader(url, false);

            String line;
            while ((line = sr.ReadLine()) != null)
            {
                json+=line.ToString();
            }

            sr.Close();

            return json;
        }
    }
}
