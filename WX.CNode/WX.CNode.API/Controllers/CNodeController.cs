using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WX.CNode.IRepository;
using WX.CNode.Cache;
using ServiceStack.Redis;
using WX.CNode.Model;

namespace WX.CNode.API.Controllers
{
    public class CNodeController : ApiController
    {
        public IActiveRepository ActiveService { get; set; }
        public IAuthorRepository AuthorService { get; set; }
        public ICollectRepository CollectService { get; set; }
        /// <summary>
        /// 加载动态的集合
        /// </summary>
        /// <param name="tab">标签</param>
        /// <returns>动态的集合</returns>
        [HttpGet]
        public List<Active> topics(string tab, int id)
        {
            List<Active> activelist = ActiveService.GetActiveList(tab, id);

            IRedisClient redisClient = RedisManager.GetClient();
            if (activelist != redisClient.Get<List<Active>>("active"))
            {
                redisClient.Set<List<Active>>("active", activelist);
                redisClient.Save();
                redisClient.Dispose();
            }

            return activelist;
        }

        /// <summary>
        /// 获取动态的详情
        /// </summary>
        /// <param name="id">动态的主键id</param>
        /// <returns>返回单条动态信息详情</returns>
        [HttpGet]
        public Active topic(int id)
        {
            IRedisClient redisClient = RedisManager.GetClient();
            List<Active> activelist = redisClient.Get<List<Active>>("active");
            Active active = activelist.Find(m => m.id == id);
            ActiveService.UpdateVisit_count(id);
            return active;
        }

        /// <summary>
        /// 收藏
        /// </summary>
        /// <param name="author_id"></param>
        /// <param name="active_id"></param>
        /// <returns></returns>
        [HttpGet]
        public bool topic_collect(int author_id,int active_id)
        {
           bool result =  ActiveService.Collect(author_id, active_id);
            return result;
        }

        

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="accesstoken">验证凭证</param>
        /// <returns>登录账户信息</returns>
        [HttpPost]
        public Author accesstoken(string accesstoken)
        {
            Author author = AuthorService.GetAuthor(accesstoken);
            return author;
        }

        public ICommentRepository CommentServices { get; set; }

        [HttpGet]
        public bool zan(int authorid,int commentid)
        {
            bool result = CommentServices.zan(authorid,commentid);
            return result;
		}
        /// <summary>
        /// 根据用户id查询用户收藏信息
        /// </summary>
        /// <param name="Authorid">用户id</param>
        /// <returns></returns>
        [HttpGet]
        public List<Active> GetCollectAuthorid(int Authorid)
        {
            List<Active> actives = CollectService.GetCollectAuthorid(Authorid);
            return actives;
        }

        /// <summary>
        /// 历史
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Active> GetHistoryList(int roleid)
        {
            List<Active> activelist = CollectService.GetHistoryList(roleid);
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
        [HttpPost]
        public bool PostActive(string title, string content, int type, int PublisheriD)
        {
            bool result = ActiveService.PostActive(title, content, type, PublisheriD);
            return result;
        }
    }
}