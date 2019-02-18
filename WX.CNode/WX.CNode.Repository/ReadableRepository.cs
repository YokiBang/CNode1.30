using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.CNode.Repository
{
    using WX.CNode.Model;
    using WX.CNode.IRepository;
    public class ReadableRepository : IReadableRepository
    {
        /// <summary>
        /// 获取单条评论信息
        /// </summary>
        /// <param name="CommonId"></param>
        /// <returns></returns>
        public Readable GetReadable(int CommonId)
        {
            string sql = "select `comment`.id,`comment`.content,`comment`.reply_at,readable.whether,author.loginname,dataresource.avatar_url,active.title from `comment` join readable on `comment`.id = readable.commentid join active ON `comment`.ActiveID = active.id join author ON author.id = `comment`.AuthorID join dataresource on dataresource.id = author.DataID where `comment`.id = " + CommonId + " ORDER BY readable.whether,`comment`.reply_at ASC";
            Readable readable = MySqlDapper.Query<Readable>(sql).FirstOrDefault();
            return readable;
        }
        /// <summary>
        /// 获取多条评论信息
        /// </summary>
        /// <param name="AuthorId"></param>
        /// <returns></returns>
        public List<Readable> GetReadableList(int AuthorId)
        {
            string sql = "select `comment`.id,`comment`.content,`comment`.reply_at,readable.whether,author.loginname,dataresource.avatar_url,active.title from `comment` join readable on `comment`.id = readable.commentid join active ON `comment`.ActiveID = active.id join author ON author.id = `comment`.AuthorID join dataresource on dataresource.id = author.DataID where active.PublisherID = " + AuthorId + " ORDER BY readable.whether,`comment`.reply_at ASC";
            List<Readable> readables = MySqlDapper.Query<Readable>(sql);
            return readables;
        }
    }
}
