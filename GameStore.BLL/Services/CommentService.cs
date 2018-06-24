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

        public void CreateComment(Comment comment)
        {
            _commentRepository.Add(comment);
            _unitOfWork.Complete();
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
