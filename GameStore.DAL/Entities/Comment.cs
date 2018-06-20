using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.DAL.Entities
{
    public class Comment
    {
        public string CommentId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string ParentCommentId { get; set; }

        public Comment ParentComment { get; set; }
        public Publisher Author { get; set; }
        public Game Game { get; set; }
    }
}
