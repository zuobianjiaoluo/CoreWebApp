using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using AliyunDemo.Services;
namespace AliyunDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var url = AliyunHelp.GetObjectBySignedUrlWithClient("B151/20190108102458209662447277.mp3");
            var i = 1;
            CreateWebHostBuilder(args).Build().Run();
           
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
