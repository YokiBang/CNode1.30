using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.CNode.Model
{
    public class Readable
    {
        public int id { get; set; } //评论ID
        public string content { get; set; } //评论内容
        public DateTime reply_at { get; set; } //评论时间
        public int whether { get; set; } //是否读过
        public string loginname { get; set; } //用户名
        public string avatar_url { get; set; } //用户头像
        public string title { get; set; } //动态标题
    }
}