using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.CNode.Repository 
{
    using WX.CNode.IRepository;
    using WX.CNode.Model;
    public class CollectRepository : ICollectRepository
    {
        /// <summary>
        /// 根据用户id查询用户收藏信息
        /// </summary>
        /// <param name="Authorid">用户id</param>
        /// <returns></returns>
        public List<Active> GetCollectAuthorid(int Authorid)
        {
            string sql = "select active.*,author.loginname,dataresource.avatar_url,type.tab,(select count(*) from `comment` where `comment`.ActiveID = active.id) as reply_count,(select reply_at from `comment` where `comment`.ActiveID = active.id ORDER BY(reply_at) desc limit 0, 1) as last_reply_at,(SELECT is_collect from collect where Authorid = "+ Authorid + " and Activeid = active.id) as is_collect from active join author on author.id = PublisherID join dataresource on dataresource.id = author.DataID join type on type.id = active.TypeID join collect on active.id = collect.Activeid where collect.Authorid = " + Authorid;
            List<Active> actives = MySqlDapper.Query<Active>(sql).ToList();
            if (actives.Count > 0)
            {
                foreach (var active in actives)
                {
                    active.author = new Author();
                    active.author.avatar_url = active.avatar_url;
                    active.author.loginname = active.loginname;
                    string sql2 = "select `comment`.*,author.loginname,dataresource.avatar_url,(select is_zan from clickgood where Authorid = " + Authorid + " and Commentid = comment.id ) as is_zan,(select count(*) from clickgood where Commentid = comment.id) as zanNum from `comment` join author on author.id=`comment`.AuthorID join dataresource on dataresource.id=author.DataID where ActiveID=" + active.id;
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
            return actives;
        }

        public List<Active> GetHistoryList(int roleid)
        {
            string sql = string.Format("select active.*,author.loginname,dataresource.avatar_url,type.tab,(select count(*) from `comment` where `comment`.ActiveID=active.id)as reply_count,(select reply_at from `comment` where `comment`.ActiveID=active.id ORDER BY(reply_at) desc limit 0,1) as last_reply_at,(SELECT is_collect from collect where Authorid = " + roleid + " and Activeid = active.id) as is_collect from active join author on author.id=PublisherID join dataresource on dataresource.id=author.DataID join type on type.id=active.TypeID where active.PublisherID = {0}", roleid);

                List<Active> activelist = new List<Active>();
                try
                {
                    activelist = MySqlDapper.Query<Active>(sql);
                }
                catch (Exception)
                {
                    return activelist;
                }


                if (activelist.Count > 0)
                {
                    foreach (var active in activelist)
                    {
                        active.author = new Author();
                        active.author.avatar_url = active.avatar_url;
                        active.author.loginname = active.loginname;

                        string sql2 = "select `comment`.*,author.loginname,dataresource.avatar_url,(select is_zan from clickgood where Authorid = " + roleid + " and Commentid = comment.id ) as is_zan,(select count(*) from clickgood where Commentid = comment.id) as zanNum from `comment` join author on author.id=`comment`.AuthorID join dataresource on dataresource.id=author.DataID where ActiveID=" + active.id;
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
