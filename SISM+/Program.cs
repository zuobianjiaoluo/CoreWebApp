using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SISM_
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CreateLnk();
            StrartProcess();
            //Application.Run(new Form1());

        }

        public static void CreateLnk()
        {
            Lnk CreateLnk = new Lnk();// MagicLibrary.Shortcuts.Lnk();

            CreateLnk.CreateShortcutLnk(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Application.StartupPath + @"\sis3.0.exe", "SIS M+", "", Application.ExecutablePath);  //桌面创建快捷方式

           // CreateLnk.CreateShortcutLnk(Application.StartupPath, Application.StartupPath + @"\sis3.0.exe", "SISM+.exe", "", Application.ExecutablePath);//程序文件夹内创建快捷方式
        }



        /// <summary>
        /// 启动SIS5客户端主程序
        /// </summary>
        public static string StrartProcess()
        {
            string fileName = "", error = "";
            Process p = new Process();
            try
            {
                //更新（调用更新的exe，这个是单独的一个程序，下面再说怎么写）
                //fileName = Application.StartupPath.Replace("Debug", "Test") + @"\BlogAutoUpdate.exe";
                fileName = Application.StartupPath + @"\sis3.0.exe";

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = fileName;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = "";//参数以空格分隔，如果某个参数为空，可以传入””
                p.Start();
                System.Environment.Exit(System.Environment.ExitCode);   //结束主线程
            }
            catch (Exception ex)
            {

                //Utils.WriteLog("启动主程序" + fileName, "异常信息：" + ex.Message + "\r\n");//日志
                p.Close();
                p.Dispose();
                error = ex.Message;
            }
            return error;
        }

    }
}
