using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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
            string sql = "SELECT Job.*,author.loginname,DataResource.avatar_url from Job INNER JOIN author on Job.Authorid = author.id join DataResource on DataResource.id = author.DataID where Activeid ="+id;
            return MySqlDapper.Query<Job>(sql).FirstOrDefault();
        }

        public int Addjob(string Jobtitle,string Jobname,string Jobaddress,string JobMes,string Jobask,string Jobemail,int Authorid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("jobtitle", Jobtitle,DbType.String);
            param.Add("jobname", Jobname, DbType.String);
            param.Add("jobaddress", Jobaddress, DbType.String);
            param.Add("jobMes", JobMes, DbType.String);
            param.Add("jobask", Jobask, DbType.String);
            param.Add("jobemail", Jobemail, DbType.String);
            param.Add("authorid", Authorid, DbType.Int32);
            param.Add("result", 0, DbType.Int32,ParameterDirection.Output);
            MySqlDapper.Execute("p_Addjob",CommandType.StoredProcedure,param);
            int res = param.Get<int>("result");
            return res; 
        }
    }
}
