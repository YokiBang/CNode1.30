using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WX.CNode.Model;
using WX.CNode.IRepository;

namespace WX.CNode.Repository
{
   public class CommentRepository : ICommentRepository
    {
        public bool zan(int authorid, int commentid)
        {
            string sql = "select count(*) from clickgood where Authorid = " + authorid + " and Commentid = " + commentid;
            List<int> i = MySqlDapper.Query<int>(sql);
            int count = i.FirstOrDefault();
            if (count > 0)
            {
                string delsql = "delete from clickgood where Authorid = " + authorid + " and Commentid = " + commentid;
                MySqlDapper.Execute(delsql);
                return false;
            }
            else
            {
                string addsql = string.Format("insert into clickgood(Authorid,Commentid,is_zan) values({0},{1},1)",authorid,commentid);
                MySqlDapper.Execute(addsql);
                return true;
            }
        }
    }
}
