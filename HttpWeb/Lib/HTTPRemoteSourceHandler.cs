using BeetleX.FastHttpApi.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpWeb.Lib
{
    public class HTTPRemoteSourceHandler : INodeSourceHandler
    {
        private HttpClusterApi mHttpClusterApi = new HttpClusterApi();

        private IHttpSourceApi mRemoteSourceApi;

        public HTTPRemoteSourceHandler(string name, params string[] hosts)
        {
            mHttpClusterApi.AddHost("*", hosts);
            mRemoteSourceApi = mHttpClusterApi.Create<IHttpSourceApi>();
            Name = name;
        }

        public string Name { get; set; }

        public int UpdateTime { get; set; } = 5;

        public Task<ApiClusterInfo> Load()
        {
            return mRemoteSourceApi._GetCluster(Name);
        }
    }
}
