using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;

namespace WX.CNode.Cache
{
    /// <summary>
    /// Description: 获取配置文件信息
    /// Version: 1.0
    /// Created: 2016-07-29
    /// Author:  zlf
    /// Company: 北京xxx信息科技有限责任公司
    /// 
    /// ModifyEditTime: 修改时间
    /// ModifyContent:  修改内容
    /// ModifyPerson :  修改人
    /// </summary>
    public static class ConfigHelper
    {
        #region 获取自定义返回值信息

        /// <summary>
        /// 自定义获取配置文件中的值
        /// </summary>
        /// <param name="strConfigKey">节点名称</param>
        /// <returns>节点值</returns>
        private static string GetConfigValue(string RedisPath)
        {
            return ConfigurationManager.AppSettings[RedisPath];
        }

        #endregion

        #region  /// Redis服务器地址及端口号

        private static string _redisServiceUrl = string.Empty;

        /// <summary>
        /// Redis服务器地址 
        /// </summary>
        public static string RedisServiceUrl
        {
            get
            {
                _redisServiceUrl = GetConfigValue("RedisServiceUrl");
                //_redisServiceUrl = ConfigurationManager.AppSettings["RedisServiceUrl"];
                return _redisServiceUrl;
            }
        }

        private static int _redisServicePortNum = 0;

        /// <summary>
        /// Redis服务器端口号
        /// </summary>
        public static int RedisServicePortNum
        {
            get
            {
                _redisServicePortNum = int.Parse(GetConfigValue("RedisServicePortNum"));
                return _redisServicePortNum;
            }
        }

        #endregion
    }
}
