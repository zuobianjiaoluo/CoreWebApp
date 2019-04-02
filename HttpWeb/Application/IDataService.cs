using BeetleX.FastHttpApi;
using BeetleX.FastHttpApi.Clients;
using HttpWeb.ViewModels;
using System;
using System.Threading.Tasks;

namespace HttpWeb.Application
{
    [JsonFormater]
    [Controller(BaseUrl = "Home")]
    public interface IDataService
    {
        [Get]
        DateTime GetTime();

        [Get]
        string Hello(string name);

        
        [Post]
        Task<bool> Login(User user);

        
    }

}
