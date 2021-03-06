﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WX.CNode.IRepository;
using WX.CNode.Cache;
using ServiceStack.Redis;
using WX.CNode.Model;
using WX.CNode.API.App_Start;

namespace WX.CNode.API.Controllers
{
    public class CNodeController : ApiController
    {
        public IActiveRepository ActiveService { get; set; }
        public IAuthorRepository AuthorService { get; set; }
        public ICollectRepository CollectService { get; set; }
        public IJobRepository JobService { get; set; }
        public IReadableRepository ReadableService { get; set; }
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
            redisClient.Set<List<Active>>("active", activelist);
            redisClient.Save();
            redisClient.Dispose();

            return activelist;
        }

        /// <summary>
        /// 获取动态的详情
        /// </summary>
        /// <param name="activeid">动态的主键id</param>
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
        /// 获取招聘详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Job job(int id)
        {
            Job job = JobService.GetJobList(id);
            job.author = new Author();
            job.author.loginname = job.loginname;
            job.author.avatar_url = job.avatar_url;
            return job;
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
        [HttpGet]
        public Author accesstoken(string accesstoken)
        {
            Author author = AuthorService.GetAuthor(accesstoken);
            return author;
        }
        [HttpGet]
        public Author Logins(string code,string loginname)
        {
            Author author = AuthorService.Logins(code,loginname);
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
        public List<Active> GetCollectList(int Authorid)
        {
            List<Active> activelist = CollectService.GetCollectAuthorid(Authorid);
            IRedisClient redisClient = RedisManager.GetClient();
            redisClient.Set<List<Active>>("active", activelist);
            redisClient.Save();
            redisClient.Dispose();
            return activelist;
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
            IRedisClient redisClient = RedisManager.GetClient();
            redisClient.Set<List<Active>>("active", activelist);
            redisClient.Save();
            redisClient.Dispose();
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
        /// <summary>
        /// 获取已读未读信息列表
        /// </summary>
        /// <param name="AuthorId"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Readable> GetReadableList(int AuthorId)
        {
            List<Readable> readables = ReadableService.GetReadableList(AuthorId);
            return readables;
        }
        /// <summary>
        /// 根据评论id获取信息
        /// </summary>
        /// <param name="CommonId"></param>
        /// <returns></returns>
        [HttpGet]
        public Readable GetReadable(int CommonId)
        {
            Readable readable = ReadableService.GetReadable(CommonId);
            return readable;
        }
        /// <summary>
        /// 获取多条评论信息(数量)
        /// </summary>
        /// <param name="AuthorId"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetReadableCount(int AuthorId)
        {
            int counts = ReadableService.GetReadableCount(AuthorId);
            return counts;
        }
        /// <summary>
        /// 发布招聘信息
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [HttpGet]
        public int Addjob(string Jobtitle, string Jobname, string Jobaddress, string JobMes, string Jobask, string Jobemail, int Authorid)
        {
            int a = JobService.Addjob(Jobtitle,Jobname,Jobaddress,JobMes,Jobask,Jobemail,Authorid);
            return a;
        }

        /// <summary>
        /// 查看点过赞的评论
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public List<Comment> GetPraiseList(int roleid)
        {
            List<Comment> commentlist = CollectService.GetPraiseList(roleid);
            return commentlist;
        }
    }
}