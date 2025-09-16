using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IServiceBase<TCreate, TUpdate, TRead, TKey>
    {
        Task<TRead> CreateAsync(TCreate dto, CancellationToken ct = default);
        Task<TRead?> GetByIdAsync(TKey id, CancellationToken ct = default);
        Task<List<TRead>> GetAllAsync(CancellationToken ct = default);
        Task<TRead?> UpdateAsync(TKey id, TUpdate dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(TKey id, CancellationToken ct = default);
    }

}
