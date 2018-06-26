using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Services
{
    public class Service<T> : IService<T> where T : class
    {
        protected IRepository<T> _repository;
        protected readonly IUnitOfWork _unitOfWork;

        public Service(IRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public virtual T Edit(int id, T updatedEntity)
        {
            throw new NotImplementedException();
        }

        public virtual void Create(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _repository.Add(entity);
            _unitOfWork.Complete();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _repository.Remove(entity);
            _unitOfWork.Complete();
        }        

        public virtual T Get(Expression<Func<T, bool>> predicate, 
            params Expression<Func<T, object>>[] includes)
        {
            var entity = _repository.GetWithIncludes(predicate, includes);
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return entity.FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            var entities = _repository.GetAll();
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            return entities;
        }
    }
}
