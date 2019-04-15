using CSRedis;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WebRedis.Logic;

namespace WebRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IRedisProvider _redisProvider;

        public ValuesController(IRedisProvider redisProvider)
        {
            _redisProvider = redisProvider;
       }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var lists = new List<string>();
            lists.Add("aaaa");
            lists.Add("bbbb");
            ////redis1
            //var list = _redisProvider.Set<List<string>>("eee", lists);
            //var getLists = _redisProvider.Get("eee");

            ////redis2
            //RedisHelper2 redisHelper = new RedisHelper2("127.0.0.1:6379");
            //string value = "abcdefg";
            //bool r1 = redisHelper.SetValue("mykey", value);
            //string saveValue = redisHelper.GetValue("mykey");
            //bool r2 = redisHelper.SetValue("mykey", "NewValue");
            //saveValue = redisHelper.GetValue("mykey");
            ////bool r3 = redisHelper.DeleteKey("mykey");  删除
            //string uncacheValue = redisHelper.GetValue("mykey");

            string ssd = "";

            //redis3  CSRedisCore直接使用  推荐
            using (var redis = new RedisClient("127.0.0.1", 6379))
            {
                string ping = redis.Ping();
                var s = redis.Info();
                string echo = redis.Echo("hello world");
                System.DateTime time = redis.Time();


                string ss=redis.Set("key44",11111111111122222222);
                ssd = redis.Get("key44");
            }

            var csredis = new CSRedisClient("mymaster,password=123,prefix=key前辍", new[] { "192.169.1.10:26379", "192.169.1.11:26379", "192.169.1.12:26379" }); //集群


            using (var redis = new RedisClient("127.0.0.1", 6379))
            {
                //不加缓存的时候，要从数据库查询
                var t1 = lists;
                var se=RedisHelper.Set("test1",t1);

                //一般的缓存代码，如不封装还挺繁琐的
                var cacheValue = RedisHelper.Get("test1");
                if (!string.IsNullOrEmpty(cacheValue))
                {
                    try
                    {
                        var sj = JsonConvert.DeserializeObject(cacheValue);
                    }
                    catch
                    {
                        //出错时删除key
                        var bools= RedisHelper.Del("test1");
                    }
                }


                var t11 = lists;
                RedisHelper.Set("test1", JsonConvert.SerializeObject(t11), 10); //缓存10秒

                //使用缓存壳效果同上，以下示例使用 string 和 hash 缓存数据
                var ta = RedisHelper.CacheShell("test1", 10, () => lists);
                var tb = RedisHelper.CacheShell("test", "1", 10, () => lists);
                var tc = RedisHelper.CacheShell("test", new[] { "1", "2" }, 10, notCacheFields => new[] {("1", lists),("2", lists)
});
            }


            return new string[] { ssd, "value2" };
        }



        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
