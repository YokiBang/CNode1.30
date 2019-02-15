using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WX.CNode.Model;

namespace WX.CNode.IRepository
{
    /// <summary>
    /// 获取动态集合
    /// </summary>
    /// <returns>返回一个集合</returns>
    public interface IActiveRepository
    {
        List<Active> GetActiveList(string tab,int id);

        bool Collect(int author_id,int active_id);

        int UpdateVisit_count(int id);

        /// <summary>
        /// 历史
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        List<Active> GetHistoryList(int roleid);

        bool PostActive(string title, string content);

    }
}
