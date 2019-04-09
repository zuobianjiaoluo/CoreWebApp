.net core使用redis
本地启动redis控制台 && 安装redis服务（用于调试）
1.下载最新版redis，选择.zip则是免安装的版本
下载地址：https://github.com/MicrosoftArchive/redis/releases

 


2.解压到指定目录，地址栏输入cmd回车，并运行cmd命令
3.在该文件夹下运行命令：redis-server.exe redis.windows.conf
4.看到如下显示，则表示启动成功


E:\软件\Redis\Redis-x64-3.2.100>redis-server.exe --service-install redis.windows.conf
[27212] 09 Apr 15:48:43.246 # Granting read/write access to 'NT AUTHORITY\NetworkService' on: "E:\软件\Redis\Redis-x64-3.2.100" "E:\软件\Redis\Redis-x64-3.2.100\"
[27212] 09 Apr 15:48:43.246 # Redis successfully installed as a service.

E:\软件\Redis\Redis-x64-3.2.100>redis-cli -h 127.0.0.1 -p 6379
127.0.0.1:6379> get name
(nil)
127.0.0.1:6379> set name qixiao
 


5.将redis安装成服务
在该文件夹下运行命令：redis-server.exe --service-install redis.windows.conf

 


去服务列表查询服务，可以看到redis服务默认没有开启，开启redis服务（可以设置为开机自动启动）

 


之后会发现远程访问失败，需要进行配置，在此不赘述，参考以下的第三篇文章
参考：
http://blog.csdn.net/e62ces0iem/article/details/73477182
http://www.cnblogs.com/weiqinl/p/6490372.html
http://blog.csdn.net/love__coder/article/details/8272180

 

安装管理工具 Redis Desktop Manager(可选项)
主要用于查询db
1.连接redis

 


2.查看/操作数据

 

 

代码
1.引用StackExchange.Redis
2.简单封装的库

public class RedisHelper
{
    private ConnectionMultiplexer redis { get; set; }
    private IDatabase db { get; set; }
    public RedisHelper(string connection)
    {
        redis = ConnectionMultiplexer.Connect(connection);
        db = redis.GetDatabase();
    }

    /// <summary>
    /// 增加/修改
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool SetValue(string key,string value)
    {
        return db.StringSet(key, value);
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetValue(string key)
    {
        return db.StringGet(key);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool DeleteKey(string key)
    {
        return db.KeyDelete(key);
    }
}
3.调用、测试

RedisHelper redisHelper = new RedisHelper("127.0.0.1:6379");
string value = "abcdefg";
bool r1 = redisHelper.SetValue("mykey", value);
string saveValue = redisHelper.GetValue("mykey");
bool r2 = redisHelper.SetValue("mykey", "NewValue");
saveValue = redisHelper.GetValue("mykey");
bool r3 = redisHelper.DeleteKey("mykey");
string uncacheValue = redisHelper.GetValue("mykey");
服务器启动redis成功后，需要将程序的连接字符串由本机测试地址改为服务器的redis地址

示例代码
https://github.com/zLulus/NotePractice/tree/dev3/Console/DotNetCoreConsole