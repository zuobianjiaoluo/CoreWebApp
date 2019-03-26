using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimeJobDemo.Models;

namespace TimeJobDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "valueddd1", "valuddddddddde2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }


        /// <summary>
        /// 机器人回调接口extra节点处理
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        // POST api/values/PostA
        [HttpPost]
        public ActionResult<string> PostA([FromBody]string json)
        {

            var list = getints();
            //json = "{\"cells\":[[{\"value\":\"是\",\"checked\":false},{\"value\":\">=2\",\"checked\":false},{\"value\":\">=5\",\"checked\":false},{\"value\":\"\",\"checked\":false},{\"value\":\"\",\"checked\":false},{\"value\":\"\",\"checked\":false},{\"value\":\"\",\"checked\":false}],[{\"value\":\"\",\"checked\":false},{\"value\":\">=1\",\"checked\":false},{\"value\":\">=3\",\"checked\":false},{\"value\":\"<=1\",\"checked\":false},{\"value\":\"\",\"checked\":false},{\"value\":\"\",\"checked\":false},{\"value\":\"通话时长>=50\",\"checked\":false}],[{\"value\":\"\",\"checked\":false},{\"value\":\"<=0\",\"checked\":true},{\"value\":\"<=0\",\"checked\":true},{\"value\":\">=2\",\"checked\":false},{\"value\":\"是\",\"checked\":false},{\"value\":\"<=2\",\"checked\":true},{\"value\":\"\",\"checked\":false}],{\"value\":\"\",\"checked\":false},{\"value\":\"拒接、无人接听、客户忙、主叫号码欠费、关机、占线、线路故障\",\"checked\":false},{\"value\":\"停机、空号\",\"checked\":false}],\"resultCell\":[{\"value\":\"否\",\"checked\":false},{\"value\":\"0\",\"checked\":false},{\"value\":\"0\",\"checked\":false},{\"value\":\"0\",\"checked\":false},{\"value\":\"否\",\"checked\":false},{\"value\":\"0\",\"checked\":false},{\"value\":\"已接听\",\"checked\":false}],\"userLevel\":\"LITTLE\"}";
            string str = "";
            var entity = JsonConvert.DeserializeObject<CallInstance>(json);
            if (entity != null)
            {
               
            }
            str = str;
            return JsonConvert.SerializeObject(entity).ToString();
        }

        /// <summary>
        /// 逐条返回集合数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> getints()
        {
           // var list = new List<int>();
            for (int i = 0;i < 10;i++)
            {

                System.Threading.Thread.Sleep(2000);

                yield return i;
               
            }

          //  return list;
        }



        /// <summary>
        /// 机器人回调接口extra节点处理
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        // POST api/values/PostA
        [HttpPost]
        public ActionResult<string> PostB([FromBody]string json)
        {
            json = "{\"cells\":[[{\"value\":\"是\",\"checked\":false},{\"value\":\">=2\",\"checked\":false},{\"value\":\">=5\",\"checked\":false},{\"value\":\"\",\"checked\":false},{\"value\":\"\",\"checked\":false},{\"value\":\"\",\"checked\":false},{\"value\":\"\",\"checked\":false}],[{\"value\":\"\",\"checked\":false},{\"value\":\">=1\",\"checked\":false},{\"value\":\">=3\",\"checked\":false},{\"value\":\"<=1\",\"checked\":false},{\"value\":\"\",\"checked\":false},{\"value\":\"\",\"checked\":false},{\"value\":\"通话时长>=50\",\"checked\":false}],[{\"value\":\"\",\"checked\":false},{\"value\":\"<=0\",\"checked\":true},{\"value\":\"<=0\",\"checked\":true},{\"value\":\">=2\",\"checked\":false},{\"value\":\"是\",\"checked\":false},{\"value\":\"<=2\",\"checked\":true},{\"value\":\"\",\"checked\":false}],{\"value\":\"\",\"checked\":false},{\"value\":\"拒接、无人接听、客户忙、主叫号码欠费、关机、占线、线路故障\",\"checked\":false},{\"value\":\"停机、空号\",\"checked\":false}],\"resultCell\":[{\"value\":\"否\",\"checked\":false},{\"value\":\"0\",\"checked\":false},{\"value\":\"0\",\"checked\":false},{\"value\":\"0\",\"checked\":false},{\"value\":\"否\",\"checked\":false},{\"value\":\"0\",\"checked\":false},{\"value\":\"已接听\",\"checked\":false}],\"userLevel\":\"LITTLE\"}";
            string str = "";
            //var entity = JsonConvert.DeserializeObject<TaskresultExtra>(json);
            //if (entity!=null)
            //{
            //    var a = entity.cells;
            //    foreach (var item in a)
            //    {
            //        if (!string.IsNullOrEmpty(item.ToString()))
            //        {
            //            str += item + ",";
            //            //Resultcell [] aa = JsonConvert.DeserializeObject<Resultcell[]>(item.ToString());
            //            //foreach (var item2 in aa)
            //            //{
            //            //    var va = item2.value;
            //            //    var ch = item2._checked;


            //            //    str += va + ",";
            //            //    str += ch + ",";
            //            //}

            //            //Resultcell aaa = JsonConvert.DeserializeObject<Resultcell>(item.ToString());
            //            //if (aaa!=null)
            //            //{
            //            //    str += aaa.value + ",";
            //            //    str += aaa._checked+",";
            //            //}
            //        }

            //    }
            //    var b = entity.resultCell;
            //    foreach (var bb in b)
            //    {
            //        str += bb + ",";
            //    }
            //    var c = entity.userLevel;
            //}
            //str = str;
            return "";// JsonConvert.SerializeObject(entity).ToString();
        }
    }
}
