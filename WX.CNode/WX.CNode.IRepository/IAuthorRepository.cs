using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WX.CNode.Model;

namespace WX.CNode.IRepository
{
    public interface IAuthorRepository
    {
        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <param name="code">accesstoken</param>
        /// <returns></returns>
        Author Logins(string code);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Author GetAuthor(string accesstoken);
    }
}
