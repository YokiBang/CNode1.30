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
            return actives;
        }
    }
}
