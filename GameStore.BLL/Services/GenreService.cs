using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Services
{
    public class GenreService : Service<Genre>, IGenreService
    {
        public GenreService(IRepository<Genre> genreRepository, IUnitOfWork unitOfWork) 
            :base(genreRepository, unitOfWork)
        {
        }

        public Genre Edit(string id, Genre updatedEntity)
        {
            var genre = _repository
                .GetWithIncludes(g => g.GenreName == id)
                .FirstOrDefault();

            if (genre == null) throw new ArgumentNullException();

            genre.GenreName = updatedEntity.GenreName;
            genre.ParentGenreName = updatedEntity.ParentGenreName;

            _unitOfWork.Complete();

            return genre;
        }

        public Genre Get(string name)
        {
            return base.Get(g => g.GenreName == name, g => g.Games);
        }
    }
}
