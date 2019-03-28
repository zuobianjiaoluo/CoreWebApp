using System;
using IWshRuntimeLibrary;
using System.IO;


namespace SISM_
{
    /// <summary>
    /// 创建快捷方式
    /// </summary>
    public class Lnk
    {
        /// <summary>
        /// 创建快捷方式的方法
        /// </summary>
        /// <param name="FolderPath">快捷方式存放的位置</param>
        /// <param name="PathLink">指向连接的文件</param>
        /// <param name="LnkName">快捷方式的文件</param>
        /// <param name="LnkNote">快捷方式的备注</param>
        /// <param name="IconLocationPath">指定快捷方式的图标</param>
        public void CreateShortcutLnk(string FolderPath, string PathLink, string LnkName, string LnkNote, string IconLocationPath)
        {
            try
            {
                WshShell shell = new WshShell();
                IWshShortcut Shortcut = (IWshShortcut)shell.CreateShortcut(FolderPath + "\\" + LnkName + ".lnk");
                Shortcut.TargetPath = PathLink;
                Shortcut.WindowStyle = 1;
                Shortcut.Description = LnkNote;
                Shortcut.IconLocation = IconLocationPath;
                Shortcut.Save();
            }
            catch(Exception ex)
            {
                throw new Exception("快捷方式未能创建:"+ex.Message);
            }
        }
    }
}
