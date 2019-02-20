using ServiceStack.Redis;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WX.CNode.Cache
{
    /// <summary>
    /// Redis缓存操作类
    /// </summary>
    public class RedisHelper
    {
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        public static bool Exist(string key)
        {
            var result = new RedisOperatorBase().Redis.ContainsKey(key);
            return result;
        }

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        public static bool Exist<T>(string hashId, string key)
        {
            var result = new RedisOperatorBase().Redis.HashContainsEntry(hashId, key);
            return result;
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        public static bool Set(string key, string value)
        {
            var result = new RedisOperatorBase().Set(key, value);
            return result;
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        public static bool Set(string key, string value, DateTime tmpExpire)
        {
            var result = new RedisOperatorBase().Set(key, value, tmpExpire);
            return result;
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        public static bool Set<T>(string key, T t)
        {
            var result = new RedisOperatorBase().Set<T>(key, t);
            return result;
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        public static bool Set<T>(string key, T t, DateTime tmpExpire)
        {
            var result = new RedisOperatorBase().Set<T>(key, t, tmpExpire);
            return result;
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        public static bool Set<T>(string hashId, string key, T t, DateTime tmpExpire)
        {
            var result = new RedisOperatorBase().Set(key, t);
            return result;
        }

        /// <summary>
        /// 移除key的value
        /// </summary>
        public static bool Remove(string key)
        {
            var result = new RedisOperatorBase().Remove(key);
            return result;
        }

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        public static bool Remove(string hashId, string key)
        {
            var result = new RedisOperatorBase().Remove(hashId, key);
            return result;
        }

        /// <summary>
        /// 根据key获取数据
        /// </summary>
        public static string Get(string key)
        {
            var result = new RedisOperatorBase().Get(key);
            return result;
        }

        /// <summary>
        /// 存取任意类型的值(hashId与key相同)
        /// </summary>
        public static T Get<T>(string key)
        {
            var result = new RedisOperatorBase().Get<T>(key);
            return result;
        }

        /// <summary>
        /// Redis缓存操作
        /// </summary>
        private class RedisOperatorBase
        {
            /// <summary>
            /// redis配置文件信息
            /// </summary>
            private readonly string _redisServiceUrl = ConfigHelper.RedisServiceUrl;
            private readonly int _redisServicePortNum = ConfigHelper.RedisServicePortNum;

            //redis缓存对象
            private RedisClient _redis = null;

            /// <summary>
            /// 定义缓存对象
            /// </summary>
            public RedisClient Redis
            {
                get
                {
                    if (_redis == null)
                    {
                        _redis = new RedisClient(_redisServiceUrl, _redisServicePortNum);
                    }
                    return _redis;
                }
            }

            private void Dispose()
            {
                _redis.Dispose();
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// 保存数据DB文件到硬盘
            /// </summary>
            private void Save()
            {
                Redis.Save();
                Dispose();
            }

            /// <summary>
            /// 异步保存数据DB文件到硬盘
            /// </summary>
            private void SaveAsync()
            {
                Redis.SaveAsync();
                Dispose();
            }

            #region /// redis操作方法

            /// <summary>
            /// 判断某个数据是否已经被缓存
            /// </summary>
            public bool Exist(string key)
            {
                try
                {
                    var result = Redis.ContainsKey(key);
                    Dispose();
                    return result;
                }
                catch
                {
                    return false;
                }
            }

            /// <summary>
            /// 判断某个数据是否已经被缓存
            /// </summary>
            public bool Exist<T>(string hashId, string key)
            {
                try
                {
                    var result = Redis.HashContainsEntry(hashId, key);
                    Dispose();
                    return result;
                }
                catch
                {
                    return false;
                }
            }

            /// <summary>
            /// 存储数据到hash表
            /// </summary>
            public bool Set(string key, string value)
            {
                try
                {
                    var tmpVal = JsonSerializer.SerializeToString<string>(value);
                    Redis.Set<string>(key, tmpVal, DateTime.Now.AddHours(12));
                    Save();
                }
                catch
                {
                    return false;
                }
                return true;
            }

            /// <summary>
            /// 存储数据到hash表
            /// </summary>
            public bool Set(string key, string value, DateTime tmpExpire)
            {
                try
                {
                    var tmpVal = JsonSerializer.SerializeToString<string>(value);
                    Redis.Set(key, tmpVal, tmpExpire);
                    Save();
                }
                catch
                {
                    return false;
                }
                return true;
            }

            /// <summary>
            /// 存储数据到hash表
            /// </summary>
            public bool Set<T>(string key, T t)
            {
                try
                {
                    Redis.Set<T>(key, t, DateTime.Now.AddHours(12));
                    Save();
                }
                catch
                {
                    return false;
                }
                return true;
            }

            /// <summary>
            /// 存储数据到hash表
            /// </summary>
            public bool Set<T>(string key, T t, DateTime tmpExpire)
            {
                try
                {
                    Redis.Set<T>(key, t, tmpExpire);
                    Save();
                }
                catch
                {
                    return false;
                }
                return true;
            }

            /// <summary>
            /// 移除key的value
            /// </summary>
            public bool Remove(string key)
            {
                try
                {
                    var result = Redis.Remove(key);
                    Dispose();
                    return result;
                }
                catch
                {
                    return false;
                }
            }

            /// <summary>
            /// 移除hash中的某值
            /// </summary>
            public bool Remove(string hashId, string key)
            {
                try
                {
                    var result = Redis.RemoveEntryFromHash(hashId, key);
                    Dispose();
                    return result;
                }
                catch
                {
                    return false;
                }
            }

            /// <summary>
            /// 根据key获取数据
            /// </summary>
            public string Get(string key)
            {
                try
                {
                    var value = Redis.Get<string>(key);
                    var result = JsonSerializer.DeserializeFromString<string>(value);
                    Dispose();
                    return result;
                }
                catch
                {
                    return null;
                }
            }

            /// <summary>
            /// 存取任意类型的值
            /// </summary>
            public T Get<T>(string key)
            {
                try
                {
                    var result = Redis.Get<T>(key);
                    Dispose();
                    return result;
                }
                catch
                {
                    return default(T);
                }
            }

            #endregion

        }
    }

}