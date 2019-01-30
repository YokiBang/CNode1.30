using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.CNode.Model
{
    public class Active
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 类别id
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int visit_count { get; set; }

        /// <summary>
        /// 获赞量
        /// </summary>
        public int zanNum { get; set; }

        /// <summary>
        /// 上一次回复时间
        /// </summary>
        public string last_reply_at { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_at { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool top { get; set; }

        /// <summary>
        /// 是否点赞
        /// </summary>
        public bool good { get; set; }

        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool is_collect { get; set; }


        public Author author { get; set; }

        public string loginname { get; set; }

        public int reply_count { get; set; }

        public string avatar_url { get; set; }

        public string tab { get; set; }

        public List<Comment> replies { get; set; }
    }
}
