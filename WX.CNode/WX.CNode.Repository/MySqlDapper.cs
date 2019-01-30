using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace WX.CNode.Repository
{
    internal static class MySqlDapper
    {
        //连接字符串
        //设置了配置文件使用这一部分连接字符串
        //public static string connectionStrings = ConfigurationManager.ConnectionStrings["laowutong"].ConnectionString;
        //未设置配置文件使用下面的连接字符串
        //public static string connectionStrings = "server=169.254.205.75;user id=FiveGroup;database=laowutongdb;password=123456";
        public static string connectionStrings = ConfigurationManager.ConnectionStrings["cnode"].ConnectionString;
        /// <summary>
        /// 得到受影响行数
        /// </summary>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="param">参数</param>
        /// <returns>返回受影响行数</returns>
        public static int Execute(string commandText,CommandType commandType=CommandType.Text, DynamicParameters param = null)
        {
            using (IDbConnection conn=new MySqlConnection(connectionStrings))
            {
                return conn.Execute(commandText,param,null,null,commandType);
            }
        }
        /// <summary>
        /// 得到一个结果集合
        /// </summary>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="param">参数</param>
        /// <returns>返回一个结果集合</returns>
        public static List<T> Query<T>(string commandText, CommandType commandType = CommandType.Text, DynamicParameters param = null) where T: class,new()
        {
            using (IDbConnection conn = new MySqlConnection(connectionStrings))
            {
                return conn.Query<T>(commandText,param,null,true,null,commandType).ToList();
            }
        }
    }
}
