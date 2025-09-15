using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T?> Get(int id, CancellationToken ct = default);
        Task<List<T>> GetAll(CancellationToken ct = default);
        Task<T> Add(T entity, CancellationToken ct = default);
        Task<T?> Delete(int id, CancellationToken ct = default);
        Task<T> Update(T entity, CancellationToken ct = default);
    }

}
