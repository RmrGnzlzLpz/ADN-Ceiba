using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Estacionamiento.Domain.Ports;
using Estacionamiento.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Estacionamiento.Infrastructure.Adapters
{
    public class GenericRepository<E> : IGenericRepository<E> where E : Domain.Entities.DomainEntity
    {
        readonly PersistenceContext _context;
        public GenericRepository(PersistenceContext context)
        {
            _context = context;
        }

        public async Task<E> AddAsync(E entity)
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity), "Entity can not be null");
            _context.Set<E>().Add(entity);
            await this.CommitAsync();
            return entity;
        }

        public async Task DeleteAsync(E entity)
        {
            if (entity != null)
            {
                _context.Set<E>().Remove(entity);
                await this.CommitAsync().ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<E>> GetAsync(
            Expression<Func<E, bool>>? filter = null,
            Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null,
            bool isTracking = false,
            uint page = 0,
            uint size = ushort.MaxValue,
            params Expression<Func<E, object>>[] includeObjectProperties
        )
        {
            IQueryable<E> query = _context.Set<E>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeObjectProperties != null)
            {
                foreach (Expression<Func<E, object>> include in includeObjectProperties)
                {
                    query = query.Include(include);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return (!isTracking) ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
        }

        public async Task<E> GetByIdAsync(object id)
        {
            return await _context.Set<E>().FindAsync(id);
        }

        public async Task UpdateAsync(E entity)
        {
            if (entity != null)
            {
                _context.Set<E>().Update(entity);
                await this.CommitAsync();
            }
        }

        public async Task CommitAsync()
        {
            _context.ChangeTracker.DetectChanges();

            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property("CreatedOn").CurrentValue = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Property("UpdatedOn").CurrentValue = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }

            await _context.CommitAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this._context.Dispose();
        }

        public async Task<int> CountAsync(Expression<Func<E, bool>>? filter)
        {
            var query = _context.Set<E>();
            if (filter != null)
            {
                return await query.CountAsync(filter);
            }
            return await query.CountAsync();
        }

        public async Task<int> CountAsync()
        {
            return await CountAsync(null);
        }
    }
}
