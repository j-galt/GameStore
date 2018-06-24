using GameStore.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IPlatformTypeRepository : IRepository<PlatformType>
    {
        IEnumerable<PlatformType> GetPlatformTypesByNames(IEnumerable<string> names);
    }
}
