using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.CNode.Model
{
   public class Job
    {
        /// <summary>
        /// 招聘id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string fabutime { get; set; }
        /// <summary>
        /// 招聘标题
        /// </summary>
        public string Jobtitle { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Jobname { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string Jobaddress { get; set; }
        /// <summary>
        /// 公司消息
        /// </summary>
        public string JobMes { get; set; }
        /// <summary>
        /// 招聘要求
        /// </summary>
        public string Jobask { get; set; }
        /// <summary>
        /// 公司邮件地址
        /// </summary>
        public string Jobemail { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Authorid { get; set; }
        /// <summary>
        /// 动态id
        /// </summary>
        public int Activeid { get; set; }

        public Author author { get; set; }
        public string loginname { get; set; }
         public string avatar_url { get; set; }

    }
}
