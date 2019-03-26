using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.lib
{
    public class IPClass
    {
        /// <summary>
        /// LanIPv4       
        /// </summary>
        /// <returns></returns>
        public static string GetGatewayIp()
        {
            try
            {
                var list = new List<string[]>();
                var rows = GetTracertResult().Split(new char[] { '\n', '\r' }).Where(q => q.Length > 0).ToArray();
                var row = rows[rows.Count() - 2];
                if (row != null)
                {
                    var ipinfo = System.Text.RegularExpressions.Regex.Split(row.Trim(), " +");
                    if (ipinfo.Length == 8)
                    {
                        return ipinfo[7];
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }



        /// <summary>
        /// LanMac
        /// </summary>
        /// <param name="ip">LanIPv4</param>
        /// <returns></returns>
        public static string GetMacByArp(string ip)
        {
            try
            {
                var list = new List<string[]>();
                var arprows = GetArpResult().Split(new char[] { '\n', '\r' });
                var row = arprows.Where(q => q.Contains(ip + " ")).FirstOrDefault();
                if (row != null)
                {
                    var ipinfo = System.Text.RegularExpressions.Regex.Split(row.Trim(), " +");//.Split(new char[] { ' ', '\t' });
                    if (ipinfo.Length == 3)
                    {
                        return ipinfo[1];
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }


        private static string GetTracertResult()
        {
            Process p = null;
            string output = string.Empty;
            try
            {
                p = Process.Start(new ProcessStartInfo("tracert", "-h 1 -d www.baidu.com")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                });
                output = p.StandardOutput.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception("IPInfo: Error Retrieving 'arp -a' Results", ex);
            }
            finally
            {
                if (p != null)
                {
                    p.Close();
                }
            }
            return output;
        }

        /// <summary>
        /// 获取ARP查询字符串
        /// </summary>
        /// <returns></returns>
        private static string GetArpResult()
        {
            Process p = null;
            string output = string.Empty;
            try
            {
                p = Process.Start(new ProcessStartInfo("arp", "-a")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                });
                output = p.StandardOutput.ReadToEnd();
            }
            catch (Exception ex)
            {
                // throw new Exception("IPInfo: Error Retrieving 'arp -a' Results", ex);
            }
            finally
            {
                if (p != null)
                {
                    p.Close();
                }
            }
            return output;
        }

    }
}
