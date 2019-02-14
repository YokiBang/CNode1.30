using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.CNode.Repository
{
    using WX.CNode.Model;
    using WX.CNode.IRepository;
    public class AuthorRepository:IAuthorRepository
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Author GetAuthor(string accesstoken)
        {
            string sql = "select author.*,dataresource.avatar_url from author join dataresource on author.DataID = dataresource.id where loginname = '" + accesstoken + "'";
            Author author = MySqlDapper.Query<Author>(sql).FirstOrDefault();
            if (author != null)
            {
                author.success = true;
            }
            return author;
        }
    }
}