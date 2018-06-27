using GameStore.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IGenreService : IService<Genre>
    {
        Genre Get(string name);
        Genre Edit(string id, Genre updatedEntity);
    }
}
