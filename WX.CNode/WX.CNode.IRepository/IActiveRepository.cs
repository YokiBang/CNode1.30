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
        List<Active> GetActiveList(string tab);
    }
}
