using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Estacionamiento.Domain.Ports
{
    public interface IGenericRepository<E> : IDisposable
        where E : Estacionamiento.Domain.Entities.DomainEntity
    {
        Task<IEnumerable<E>> GetAsync(Expression<Func<E, bool>>? filter = null,
            Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null,
             bool isTracking = false, uint page = 0, uint size = ushort.MaxValue, params Expression<Func<E, object>>[] includeObjectProperties);

        Task<int> CountAsync(Expression<Func<E, bool>> filter);
        Task<int> CountAsync();

        Task<E> GetByIdAsync(object id);
        Task<E> AddAsync(E entity);
        Task UpdateAsync(E entity);
        Task DeleteAsync(E entity);
    }
}
