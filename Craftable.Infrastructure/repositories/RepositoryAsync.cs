using Craftable.Infrastructure.data;
using Craftable.SharedKernel;
using Craftable.SharedKernel.interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Infrastructure.repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T>
        where T : BaseEntity
    {
        protected readonly CraftableContext _context;

        public RepositoryAsync(CraftableContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T obj, CancellationToken cancellationToken)
        {
            if (obj is null)
            {
                throw new ArgumentNullException();
            }

            await _context.AddAsync(obj, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            var query = _context.Set<T>().AsQueryable().AsNoTrackingWithIdentityResolution();
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = _context.Set<T>().AsQueryable().AsNoTrackingWithIdentityResolution();
            return await query.SingleAsync(x => x.Id == id);
        }
    }
}