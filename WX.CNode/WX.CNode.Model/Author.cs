using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.CNode.Model
{
    public class Author
    {
        public string session_key { get; set; } //用户令牌
        public string OpenId { get; set; }  //OpendId
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string loginname { get; set; }
        /// <summary>
        /// 资料库ID
        /// </summary>
        public int DataID { get; set; }

        public string avatar_url { get; set; }

        public bool success { get; set; }
    }
}
