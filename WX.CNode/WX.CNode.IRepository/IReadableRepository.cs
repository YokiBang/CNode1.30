using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.CNode.IRepository
{
    using WX.CNode.Model;
    public interface IReadableRepository
    {
        /// <summary>
        /// 根据用户id查询收到的评论信息
        /// </summary>
        /// <param name="AuthorId"></param>
        /// <returns></returns>
        List<Readable> GetReadableList(int AuthorId);
        /// <summary>
        /// 根据评论id获取信息
        /// </summary>
        /// <param name="CommonId"></param>
        /// <returns></returns>
        Readable GetReadable(int CommonId);
    }
}
