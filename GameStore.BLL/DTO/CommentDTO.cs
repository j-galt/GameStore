using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.DTO
{
    public class CommentDto
    {
        public string CommentId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string ParentCommentId { get; set; }
    }
}
