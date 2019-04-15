using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace WebRedis.Logic
{
    /// <summary>
    /// Redis帮助类
    /// </summary>
    public class RedisHelper
        {
            private static readonly string Connstr;
            private static readonly int Database = 0;
            private static ConnectionMultiplexer _redis;
            private static readonly object Locker = new object();

            private static ConnectionMultiplexer Conn;
            private static IDatabase Db;

            #region 初始化
            static RedisHelper()
            {
                try
                {
                    Connstr = "127.0.0.1:6379";////TODO 账号,密码
                                          //Conn = ConnectionMultiplexer.Connect(Connstr);
                                          //Db = Conn == null ? null : Conn.GetDatabase(2);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            public static ConnectionMultiplexer Manager
            {
                get
                {
                    if (_redis == null)
                    {
                        lock (Locker)
                        {
                            if (_redis != null) return _redis;

                            _redis = GetManager();
                            return _redis;
                        }
                    }

                    return _redis;
                }
            }

            private static ConnectionMultiplexer GetManager(string connectionString = null)
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = Connstr;
                }

                var options = ConfigurationOptions.Parse(connectionString);
                options.ConnectTimeout = 2000;//连接操作超时（ms）
                options.KeepAlive = 180;//发送消息以帮助保持套接字活动的时间（秒）（默认为60秒）
                options.SyncTimeout = 2000;//时间（ms）允许进行同步操作
                options.ConnectRetry = 3;//重试连接的次数

                return ConnectionMultiplexer.Connect(options);
            }

            #region String 可以设置过期时间

            /// <summary>
            /// 保存单个key value
            /// </summary>
            /// <param name="key">Redis Key</param>
            /// <param name="value">保存的值</param>
            /// <param name="expiry">过期时间</param>
            /// <returns></returns>
            public static bool SetStringKey(string key, string value, TimeSpan? expiry = default(TimeSpan?))
            {
                var db = Manager.GetDatabase(Database);
                return db.StringSet(key, value, expiry);
            }

            /// <summary>
            /// 保存多个key value
            /// </summary>
            /// <param name="arr">key</param>
            /// <returns></returns>
            public static bool SetStringKey(KeyValuePair<RedisKey, RedisValue>[] arr)
            {
                var db = Manager.GetDatabase(Database);
                return db.StringSet(arr);
            }

            /// <summary>
            /// 保存一个对象
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="key"></param>
            /// <param name="obj"></param>
            /// <param name="expiry"></param>
            /// <returns></returns>
            public static bool SetStringKey<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
            {
                string json = JsonConvert.SerializeObject(obj);
                var db = Manager.GetDatabase(Database);
                return db.StringSet(key, json, expiry);
            }

            /// <summary>
            /// 获取单个key的值
            /// </summary>
            /// <param name="key">Redis Key</param>
            /// <returns></returns>

            public static RedisValue GetStringKey(string key)
            {
                var db = Manager.GetDatabase(Database);
                return db.StringGet(key);
            }

            /// <summary>
            /// 获取多个Key
            /// </summary>
            /// <param name="listKey">Redis Key集合</param>
            /// <returns></returns>
            public static RedisValue[] GetStringKey(List<RedisKey> listKey)
            {
                var db = Manager.GetDatabase(Database);
                return db.StringGet(listKey.ToArray());
            }

            /// <summary>
            /// 获取一个key的对象
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            public static string GetObjectKey(string key)
            {
                var db = Manager.GetDatabase(Database);
                return db.StringGet(key).ToString();
            }

            /// <summary>
            /// 获取一个key的对象
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            public static string GetObjectKey<T>(string key)
            {
                var db = Manager.GetDatabase(Database);

                if ((db.StringGet(key) == RedisValue.Null))
                {
                    return "";
                }
                else
                {
                    var redisKey = db.StringGet(key);
                    return JsonConvert.DeserializeObject<T>(redisKey.ToString()).ToString();
                }
            }

            #endregion

            #region Hash

            /// <summary>
            /// 保存一个集合
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="key">Redis Key</param>
            /// <param name="list">数据集合</param>
            /// <param name="getModelId"></param>
            public static void HashSet<T>(string key, List<T> list, Func<T, string> getModelId)
            {
                List<HashEntry> listHashEntry = new List<HashEntry>();
                foreach (var item in list)
                {
                    string json = JsonConvert.SerializeObject(item);
                    listHashEntry.Add(new HashEntry(getModelId(item), json));
                }

                var db = Manager.GetDatabase(Database);
                db.HashSet(key, listHashEntry.ToArray());
            }

            /// <summary>
            /// 获取Hash中的单个key的值
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="key">Redis Key</param>
            /// <param name="hasFildValue">RedisValue</param>
            /// <returns></returns>
            public static T GetHashKey<T>(string key, string hasFildValue)
            {
                if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(hasFildValue))
                {
                    var db = Manager.GetDatabase(Database);
                    RedisValue value = db.HashGet(key, hasFildValue);
                    if (!value.IsNullOrEmpty)
                    {
                        return JsonConvert.DeserializeObject<T>(value);
                    }
                }
                return default(T);
            }

            /// <summary>
            /// 获取hash中的多个key的值
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="key">Redis Key</param>
            /// <param name="listhashFields">RedisValue value</param>
            /// <returns></returns>
            public static List<T> GetHashKey<T>(string key, List<RedisValue> listhashFields)
            {
                List<T> result = new List<T>();
                if (!string.IsNullOrWhiteSpace(key) && listhashFields.Count > 0)
                {
                    var db = Manager.GetDatabase(Database);

                    RedisValue[] value = db.HashGet(key, listhashFields.ToArray());
                    foreach (var item in value)
                    {
                        if (!item.IsNullOrEmpty)
                        {
                            result.Add(JsonConvert.DeserializeObject<T>(item));
                        }
                    }
                }
                return result;
            }

            /// <summary>
            /// 获取hashkey所有Redis key
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="key"></param>
            /// <returns></returns>
            public static List<T> GetHashAll<T>(string key)
            {
                var db = Manager.GetDatabase(Database);

                List<T> result = new List<T>();
                RedisValue[] arr = db.HashKeys(key);
                foreach (var item in arr)
                {
                    if (!item.IsNullOrEmpty)
                    {
                        result.Add(JsonConvert.DeserializeObject<T>(item));
                    }
                }
                return result;
            }

            /// <summary>
            /// 获取hashkey所有的值
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="key"></param>
            /// <returns></returns>
            public static List<T> HashGetAll<T>(string key)
            {
                var db = Manager.GetDatabase(Database);

                List<T> result = new List<T>();
                HashEntry[] arr = db.HashGetAll(key);
                foreach (var item in arr)
                {
                    if (!item.Value.IsNullOrEmpty)
                    {
                        result.Add(JsonConvert.DeserializeObject<T>(item.Value));
                    }
                }
                return result;
            }

            /// <summary>
            /// 删除hasekey
            /// </summary>
            /// <param name="key"></param>
            /// <param name="hashField"></param>
            /// <returns></returns>
            public static bool DeleteHase(RedisKey key, RedisValue hashField)
            {
                var db = Manager.GetDatabase(Database);
                return db.HashDelete(key, hashField);
            }

            #endregion

            #region key

            /// <summary>
            /// 删除单个key
            /// </summary>
            /// <param name="key">redis key</param>
            /// <returns>是否删除成功</returns>
            public static bool KeyDelete(string key)
            {
                var db = Manager.GetDatabase(Database);
                return db.KeyDelete(key);
            }

            /// <summary>
            /// 删除多个key
            /// </summary>
            /// <param name="keys">rediskey</param>
            /// <returns>成功删除的个数</returns>
            public static long KeyDelete(RedisKey[] keys)
            {
                var db = Manager.GetDatabase(Database);
                return db.KeyDelete(keys);
            }

            /// <summary>
            /// 判断key是否存储
            /// </summary>
            /// <param name="key">redis key</param>
            /// <returns></returns>
            public static bool KeyExists(string key)
            {
                var db = Manager.GetDatabase(Database);
                return db.KeyExists(key);
            }

            /// <summary>
            /// 重新命名key
            /// </summary>
            /// <param name="key">就的redis key</param>
            /// <param name="newKey">新的redis key</param>
            /// <returns></returns>
            public static bool KeyRename(string key, string newKey)
            {
                var db = Manager.GetDatabase(Database);
                return db.KeyRename(key, newKey);
            }
            #endregion

            #region Search Keys
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
            public static List<string> SearchKeys(string key)
            {
                List<string> list = null;
                for (int i = 1; i <= 5; i++)
                {
                    bool bFlag = KeyExists(key + "_" + i);
                    string value = RedisHelper.GetObjectKey(key + "_" + i);

                    if (!string.IsNullOrEmpty(value))
                    {
                        if (list == null)
                            list = new List<string>();

                        list.Add(value);
                    }
                }
                return list;

            }
            #endregion

        }
    
}
