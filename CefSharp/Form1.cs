using CefSharp.WinForms;
using System.Windows.Forms;

namespace CefSharp
{
    public partial class Form1 : Form
    {
        public ChromiumWebBrowser chromeBrowser;

        public Form1()
        {
            InitializeComponent();

            //this.WindowState = FormWindowState.Maximized;
            //CefSharp.CefSettings settings = new CefSharp.CefSettings();

            //CefSharp.Cef.Initialize(settings);
            //CefSharp.WinForms.ChromiumWebBrowser webView = new CefSharp.WinForms.ChromiumWebBrowser("https://www.baidu.com");
            //webView.Dock = DockStyle.Fill;
            //webView.LifeSpanHandler = new OpenPageSelf();
            //this.Controls.Add(webView);

        }

       
    }
}