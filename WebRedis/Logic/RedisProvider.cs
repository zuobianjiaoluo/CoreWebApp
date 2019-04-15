using System;
using System.Collections.Generic;

namespace WebRedis.Logic
{
    /// <summary>
    /// redis帮助类
    /// </summary>
    public class RedisProvider : IRedisProvider
    {
        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            try
            {
                return RedisHelper.GetObjectKey(key);
            }
            catch (Exception ex)
            {
                //LOG
                return null;
            }
        }
        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get<T>(string key)
        {
            try
            {
                return RedisHelper.GetObjectKey<T>(key);
            }
            catch (Exception ex)
            {
                //LOG
                return null;
            }
        }
        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool Set(string key, string value)
        {
            try
            {
               return RedisHelper.SetStringKey(key, value);
            }
            catch (Exception ex)
            {
                //LOG
                return false;
            }
        }
        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Set<T>(string key, T value)
        {
            try
            {
               return RedisHelper.SetStringKey<T>(key, value);
            }
            catch (Exception ex)
            {
                //LOG
                return false;
            }
        }
        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool Set(string key, string value, TimeSpan sp)
        {
            try
            {
                return RedisHelper.SetStringKey(key, value, sp);
            }
            catch (Exception ex)
            {
                //LOG
                return false;
            }
        }
        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, TimeSpan sp)
        {
            try
            {
                return RedisHelper.SetStringKey<T>(key, value, sp);
            }
            catch (Exception ex)
            {
                //LOG
                return false;
            }
        }
        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>是否删除成功</returns>
        public bool Remove(string key)
        {
            try
            {
                return RedisHelper.KeyDelete(key);
            }
            catch (Exception ex)
            {
                //LOG
                return false;
            }
        }

        public List<string> SearchKeys(string key)
        {
            try
            {
                return RedisHelper.SearchKeys(key);
            }
            catch (Exception ex)
            {
                //LOG
                return null;
            }
        }
        /// <summary>
        /// 判断key是否存储
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            try
            {
                return RedisHelper.KeyExists(key);
            }
            catch (Exception ex)
            {
                //LOG
                return false;
            }
        }
    }
}
