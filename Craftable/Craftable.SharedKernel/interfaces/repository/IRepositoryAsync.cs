using Craftable.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.SharedKernel.interfaces.Repository
{
    public interface IRepositoryAsync<T>
        where T : BaseEntity
    {
        Task CreateAsync(T obj, CancellationToken cancellationToken);

        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);
    }
}