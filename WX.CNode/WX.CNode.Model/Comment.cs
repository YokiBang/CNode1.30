using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.CNode.Model
{
    public class Comment
    {
        public int id { get; set; }

        public string content { get; set; }

        public int zanNum { get; set; }

        public int AuthorID { get; set; }

        public int ActiveID { get; set; }

        public string reply_at { get; set; }

        public Author author { get; set; }

        public string avatar_url { get; set; }

        public string loginname { get; set; }
    }
}
