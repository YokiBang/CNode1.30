using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WX.CNode.IRepository;
using WX.CNode.Model;

namespace WX.CNode.Repository
{
   public class JobRepository : IJobRepository
    {
        public Job GetJobList(int id)
        {
            string sql = "SELECT Job.*,author.loginname,DataResource.avatar_url from Job INNER JOIN author on Job.Authorid = author.id join DataResource on DataResource.id = author.DataID where Job.id ="+id;
            return MySqlDapper.Query<Job>(sql).FirstOrDefault();
        }
    }
}
