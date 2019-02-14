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
        public List<Active> CollectAuthorid(int Authorid)
        {
            string sql = "select active.* from active join collect on active.id = collect.Activeid where collect.Authorid =" + Authorid;
            List<Active> actives = MySqlDapper.Query<Active>(sql).ToList();
            return actives;
        }
    }
}
