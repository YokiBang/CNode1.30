using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.CNode.Model
{
    public class Comment
    {
        /// <summary>
        /// 评论id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 点赞量
        /// </summary>
        public int zanNum { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int AuthorID { get; set; }
        /// <summary>
        /// 动态id
        /// </summary>
        public int ActiveID { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public string reply_at { get; set; }
        /// <summary>
        /// 相关动态的用户
        /// </summary>
        public Author author { get; set; }
        /// <summary>
        /// 用户头像路径
        /// </summary>
        public string avatar_url { get; set; }
        /// <summary>
        /// 用户登录名称
        /// </summary>
        public string loginname { get; set; }
        /// <summary>
        /// 是否点赞
        /// </summary>
        public bool is_zan { get; set; }
    }
}
