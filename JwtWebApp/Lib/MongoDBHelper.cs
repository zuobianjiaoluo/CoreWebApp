using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace JwtWebApp.Lib
{
    /// <summary>
    /// MongoDB帮助类
    /// </summary>
    public class MongoDBHelper
    {
        private static MongoClient client = new MongoClient("mongodb://127.0.0.1:27017"); //服务器地址
        private static IMongoDatabase database = client.GetDatabase("BeiKe");  //库名
        private static IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("bar"); //表名

        /// <summary>
        /// 添加
        /// </summary>
        public void AddUser()
        {
            var document = new BsonDocument
            {
                { "name", "测试数据1" },
                { "type", "大类" },
                { "number", 5 },
                { "info", new BsonDocument
                          {
                              { "x", 111 },
                              { "y", 222 }
                          }}
            };
            collection.InsertOne(document);
        }

        /// <summary>
        /// 查询
        /// </summary>
        public List<Bar> GetUser()
        {
            var result=collection.Find(Builders<BsonDocument>.Filter.Eq("name", "测试数据1") & Builders<BsonDocument>.Filter.Lt("number", 6)).As<Bar>().ToList();

            //Filter.Lt创建小于筛选器,Filter.Gt大于，Filter.Eq等于，SortBy 按升序字段对结果排序
            //var result = collection
            //.Find((Builders<BsonDocument>.Filter.Lt("number", 999) & Builders<BsonDocument>.Filter.Gt("number", 110)) & Builders<BsonDocument>.Filter.Eq("name", "测试数据1")).SortBy(x => x["number"])//
            //.Skip(10)//跳过
            //.Limit(10)//限制
            //.As<Bar>()//m=>o
            //.ToList();//像极了Linq吧?

            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public void UpdateUser()
        {
            collection.UpdateMany(Builders<BsonDocument>.Filter.Eq("name", "测试数据1"), Builders<BsonDocument>.Update.Set("number2", 666));

        }

        /// <summary>
        /// 删除
        /// </summary>
        public void DelUser()
        {
            collection.DeleteMany(Builders<BsonDocument>.Filter.Eq("name", "测试数据1"));
        }
    }

    public class Bar
    {
        public ObjectId _id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int number { get; set; }
        public int number2 { get; set; }
        public BarInfo info { get; set; }

        public class BarInfo
        {
            public int x { get; set; }
            public int y { get; set; }
        }
    }
}
