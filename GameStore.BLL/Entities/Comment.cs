using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.BLL.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int GameId { get; set; }
        public int? ParentCommentId { get; set; }

        public Comment ParentComment { get; set; }
        public Game Game { get; set; }
    }
}
