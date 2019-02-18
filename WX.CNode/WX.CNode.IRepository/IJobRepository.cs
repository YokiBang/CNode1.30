using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WX.CNode.Model;

namespace WX.CNode.IRepository
{
    public interface IJobRepository
    {
        Job GetJobList(int id);
    }
}
