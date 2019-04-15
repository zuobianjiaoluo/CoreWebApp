using CSRedis;
using System;

namespace WebRedis.CSRedis
{
    public class RedisBase
    {
        protected static string _connect_str_write= "127.0.0.1:6379";
        protected static string _connect_str_read="127.0.0.1:6379";


        protected static CSRedisClient rds_write = null;
        protected static CSRedisClient rds_read = null;

        private static object _lockObj_write = new object();
        private static object _lockObj_read = new object();

        public static CSRedisClient GetWriteClient()
        {
            if (string.IsNullOrEmpty(_connect_str_write))
            {
                Console.WriteLine("未设置写入连接字符串");
            }
            if (rds_write == null)
            {
                lock (_lockObj_write)
                {
                    if (rds_write == null)
                    {
                        rds_write = new CSRedisClient($"{_connect_str_write},poolsize=50,ssl=false,writeBuffer=10240");
                        RedisHelper.Initialization(rds_write);
                    }
                }
            }
            return rds_write;
        }
        public static CSRedisClient GetReadClient()
        {
            if (string.IsNullOrEmpty(_connect_str_read))
            {
                Console.WriteLine("未设置读取连接字符串");
            }
            if (rds_read == null)
            {
                lock (_lockObj_read)
                {
                    if (rds_read == null)
                    {
                        rds_read = new CSRedisClient($"{_connect_str_read},poolsize=50,ssl=false,writeBuffer=10240");
                    }
                }
            }
            return rds_read;
        }
    }
}
