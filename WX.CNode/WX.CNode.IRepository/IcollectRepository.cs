using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.CNode.IRepository
{
    public interface ICollectRepository
    {
        List<Action> CollectAuthorid(int Authorid);
    }
}
