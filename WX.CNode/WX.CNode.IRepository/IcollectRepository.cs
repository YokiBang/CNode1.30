using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.CNode.IRepository
{
    using WX.CNode.Model;
    public interface ICollectRepository
    {
        /// <summary>
        /// 根据用户id查询用户收藏信息
        /// </summary>
        /// <param name="Authorid">用户id</param>
        /// <returns></returns>
        List<Active> CollectAuthorid(int Authorid);
    }
}
