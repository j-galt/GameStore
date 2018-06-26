using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Services
{
    public class CommentService : Service<Comment>, ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;

        public CommentService(IRepository<Comment> commentRepository, IUnitOfWork unitOfWork)
            : base(commentRepository, unitOfWork)
        {
            _commentRepository = commentRepository;
        }

        public override void Create(Comment comment)
        {
            if (comment == null) throw new ArgumentNullException(nameof(comment));

            if (comment.GameId == 0)
            {
                var parentComment = _repository.Get((int)comment.ParentCommentId);
                comment.GameId = parentComment.GameId;
            }

            base.Create(comment);
        }

        public override Comment Edit(int id, Comment updatedEntity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetCommentsByGameId(int id)
        {
            return _commentRepository.Find(c => c.Game.GameId == id);
        }
    }
}
