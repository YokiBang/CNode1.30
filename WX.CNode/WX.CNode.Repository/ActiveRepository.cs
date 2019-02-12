using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WX.CNode.Model;
using WX.CNode.IRepository;

namespace WX.CNode.Repository
{
    public class ActiveRepository : IActiveRepository
    {
        /// <summary>
        /// 获取动态和相关评论的集合
        /// </summary>
        /// <returns>返回一个集合</returns>
        public List<Active> GetActiveList(string tab)
        {
            string sql = "select active.*,author.loginname,dataresource.avatar_url,type.tab,(select count(*) from `comment` where `comment`.ActiveID=active.id)as reply_count,(select reply_at from `comment` where `comment`.ActiveID=active.id ORDER BY(reply_at) desc limit 0,1) as last_reply_at from active join author on author.id=PublisherID join dataresource on dataresource.id=author.DataID join type on type.id=active.TypeID";
            if (tab!="all")
            {
                sql += " where tab=" + tab;
            }

            List<Active> activelist = new List<Active>() ;
            try
            {
                activelist = MySqlDapper.Query<Active>(sql);
            }
            catch (Exception)
            {
                return activelist;
            }
            

            if (activelist.Count>0)
            {
                foreach (var active in activelist)
                {
                    active.author = new Author();
                    active.author.avatar_url = active.avatar_url;
                    active.author.loginname = active.loginname;

                    string sql2 = "select `comment`.*,author.loginname,dataresource.avatar_url from `comment` join author on author.id=`comment`.AuthorID join dataresource on dataresource.id=author.DataID where ActiveID=" + active.id;
                    List<Comment> commentlist = MySqlDapper.Query<Comment>(sql2);

                    foreach (var comment in commentlist)
                    {
                        comment.author = new Author();
                        comment.author.loginname = comment.loginname;
                        comment.author.avatar_url = comment.avatar_url;
                    }
                    active.replies = commentlist;
                }
            }

            return activelist;
        }
    }
}
