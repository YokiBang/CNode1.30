using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WX.CNode.IRepository;
using WX.CNode.Repository;
using WX.CNode.Model;

namespace WX.CNode.API.Controllers
{
    public class CNodeController : ApiController
    {
        public IActiveRepository ActiveService { get; set; }
        [HttpGet]
        public List<Active> topics()
        {
            List<Active> activelist = ActiveService.GetActiveList();
            return activelist;
        }

        [HttpGet]
        public Active topic(int id)
        {
            Active active = ActiveService.GetActiveList().Find(m=>m.id==id);
            return active;
        }

    }
}
