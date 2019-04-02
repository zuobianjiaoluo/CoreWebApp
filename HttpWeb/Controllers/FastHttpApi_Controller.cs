using BeetleX.FastHttpApi.Clients;
using HttpWeb.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HttpWeb.Controllers
{
    public class FastHttpApi_Controller : Controller
    {
        public IActionResult Index()
        {
            string url = "https://localhost:5050/home/";
            HttpApiClient client = new HttpApiClient(url);
            IDataService service = client.Create<IDataService>();

           

            //DateTime dt = service.GetTime();
            //Console.WriteLine($"get time:{dt}");

            //string hello = service.Hello("henry");
            //Console.WriteLine($"hello :{hello}");


            var login = service.Login(new ViewModels.User() {name= "admin", pwd= "123456" });
            //Console.WriteLine($"login status:{login}");

           
            return Ok(login);
        }


        private static HttpClusterApi hca = new HttpClusterApi();
        /// <summary>
        /// 集群化访问定义
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ClusterIndex()
        {
            string Host26 = "https://localhost:5050/";
            string Host27 = "https://localhost:5051/";
            string Host28 = "https://localhost:5052/";

            hca.AddHost("home.*", Host26);
            IDataService serviceHca = hca.Create<IDataService>();
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + obj.access_token);

            var login = await serviceHca.Login(new ViewModels.User() { name = "admin", pwd = "123456" });





            string[] hosts = new string[] { Host26};
            HTTPRemoteSourceHandler remoteSourceHandler = new HTTPRemoteSourceHandler("default", hosts);
            hca.NodeSourceHandler = remoteSourceHandler;
            IDataService serviceHcas = hca.Create<IDataService>();

            //var result = await HttpClusterApi.LoadNodeSource();




            var result = await serviceHcas.Login(new ViewModels.User() { name = "admin", pwd = "123456" });
            //Console.WriteLine($"login status:{login}");


            return Ok(login);
        }
    }
}