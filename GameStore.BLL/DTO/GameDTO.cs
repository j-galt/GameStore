using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.DTO
{
    public class GameDto
    {
        public string GameId { get; set; }
        public string GameName { get; set; }
        public string Description { get; set; }
        public string PublisherId { get; set; }
    }
}
