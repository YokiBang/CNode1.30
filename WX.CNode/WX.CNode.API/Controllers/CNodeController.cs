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
        [HttpGet]
        public List<Active> topics(string tab)
        {
            List<Active> activelist = ActiveService.GetActiveList(tab);
            IRedisClient redisClient = RedisManager.GetClient();
            redisClient.Set<List<Active>>("active", activelist);
            redisClient.Save();
            redisClient.Dispose();
            return activelist;
        }

        [HttpGet]
        public Active topic(int id)
        {
            IRedisClient redisClient = RedisManager.GetClient();
            List<Active> activelist = redisClient.Get<List<Active>>("active");
            Active active = activelist.Find(m=>m.id==id);
            return active;
        }

    }
}
