﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WX.CNode.Model;
using WX.CNode.IRepository;

namespace WX.CNode.Repository
{
    public class ActiveRepository : IActiveRepository
    {
        /// <summary>
        /// 获取动态和相关评论的集合
        /// </summary>
        /// <returns>返回一个集合</returns>
        public List<Active> GetActiveList(string tab, int id)
        {
            string sql = "select active.*,author.loginname,dataresource.avatar_url,type.tab,(select count(*) from `comment` where `comment`.ActiveID=active.id)as reply_count,(select reply_at from `comment` where `comment`.ActiveID=active.id ORDER BY(reply_at) desc limit 0,1) as last_reply_at,(SELECT is_collect from collect where Authorid = " + id + " and Activeid = active.id) as is_collect from active join author on author.id=PublisherID join dataresource on dataresource.id=author.DataID join type on type.id=active.TypeID";
            if (tab != "all")
            {
                sql += " where tab='" + tab + "'";
            }

            List<Active> activelist = new List<Active>();
            try
            {
                activelist = MySqlDapper.Query<Active>(sql);
            }
            catch (Exception)
            {
                return activelist;
            }


            if (activelist.Count > 0)
            {
                foreach (var active in activelist)
                {
                    active.author = new Author();
                    active.author.avatar_url = active.avatar_url;
                    active.author.loginname = active.loginname;

                    string sql2 = "select `comment`.*,author.loginname,dataresource.avatar_url,(select is_zan from clickgood where Authorid = "+id+" and Commentid = comment.id ) as is_zan,(select count(*) from clickgood where Commentid = comment.id) as zanNum from `comment` join author on author.id=`comment`.AuthorID join dataresource on dataresource.id=author.DataID where ActiveID=" + active.id;
                    List<Comment> commentlist = MySqlDapper.Query<Comment>(sql2);

                    foreach (var comment in commentlist)
                    {
                        comment.author = new Author();
                        comment.author.loginname = comment.loginname;
                        comment.author.avatar_url = comment.avatar_url;
                    }
                    active.replies = commentlist;
                }
            }

            return activelist;
        }

        /// <summary>
        /// 收藏
        /// </summary>
        /// <param name="author_id"></param>
        /// <param name="active_id"></param>
        /// <returns></returns>
        public bool Collect(int author_id, int active_id)
        {
            string sql = "SELECT count(*) from collect where Authorid = " + author_id + " and Activeid = " + active_id;
            //List<int> i = MySqlDapper.Query<int>(sql);
            //int count = i.FirstOrDefault();
            int count = Convert.ToInt32(MySqlDapper.Scalar(sql));
            if (count > 0)
            {
                string delsql = "delete from collect where  Authorid = " + author_id + " and Activeid = " + active_id;
                MySqlDapper.Execute(delsql);
                return false;
            }
            else
            {
                string addsql = string.Format("insert into collect(Authorid,Activeid,is_collect) values({0},{1},1)", author_id, active_id);
                MySqlDapper.Execute(addsql);
                return true;
            }
        }

        /// <summary>
        /// 修改访问量
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public int UpdateVisit_count(int id)
        {
            string sql = string.Format("update Active set visit_count=visit_count+1 where id='{0}'", id);
            return MySqlDapper.Execute(sql);
        }

        /// <summary>
        /// 历史
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public List<Active> GetHistoryList(int roleid)
        {
            string sql = string.Format("SELECT a.*,b.loginname from active a INNER JOIN author b ON a.PublisherID = '{0}'", roleid);
            List<Active> activelist = MySqlDapper.Query<Active>(sql);
            return activelist;
        }
        /// <summary>
        /// 发布添加
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="type">类型</param>
        /// <param name="PublisheriD">用户id</param>
        /// <returns></returns>
        public bool PostActive(string title, string content, int type, int PublisheriD)
        {
            string sql = "insert into active values(id,'" + title + "','" + content + "'," + type + ",0,0,NOW()," + PublisheriD + ",0,0)";
            int count = MySqlDapper.Execute(sql);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
