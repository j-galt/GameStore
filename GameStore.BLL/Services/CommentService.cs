using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IRepository<Comment> commentRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateComment(Comment comment)
        {
            _commentRepository.Add(comment);
            _unitOfWork.Complete();
        }

        public IEnumerable<Comment> GetCommentsByGameId(int id)
        {
            return _commentRepository.Find(c => c.Game.GameId == id);
        }
    }
}
