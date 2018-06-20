using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.BLL.Entities
{
    public class PlatformType
    {
        public string Type { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
