using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(object id);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
