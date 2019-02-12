using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WX.CNode.Model;
using WX.CNode.IRepository;

namespace WX.CNode.Repository
{
    public class AuthorRepository:IAuthorRepository
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Author accesstoken(string token)
        {
            string sql = "select *from author JOIN dataresource ON author.DataID = dataresource.id where loginname = '" + token + "'";
            Author author = MySqlDapper.Query<Author>(sql).FirstOrDefault();
            return author;
        }
    }
}