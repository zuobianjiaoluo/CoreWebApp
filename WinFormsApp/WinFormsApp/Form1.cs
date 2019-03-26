using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            bool bl = GetProcessState("QQ");
        }

        /// <summary>
        /// 判断进程是否开启 有返回true
        /// </summary>
        /// <param name="processName">进程名称</param>
        /// <returns></returns>
        public static bool GetProcessState(string processName)
        {
            bool isKQ = false;
            string mtid = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();

            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process process in processList)
            {
                if (process.ProcessName.ToUpper() == processName)
                {
                    string jc = "";
                    if (processName == "客户端主程序")
                    {
                        jc = System.Environment.CurrentDirectory + "\\Bin\\" + processName + ".exe";
                    }
                    else
                    {
                        jc = System.Environment.CurrentDirectory + "\\MasterUpgrade\\" + processName + ".exe";
                    }

                    if (process.MainModule.FileName == jc)
                    {
                        isKQ = true;
                    }
                }
            }
            return isKQ;
        }
    }
}
