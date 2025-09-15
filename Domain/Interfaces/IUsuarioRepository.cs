using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<IEnumerable<Usuario>> BuscarPorNombre(string nombre, CancellationToken ct = default);
        Task<Usuario?> GetConDomicilios(int id, CancellationToken ct = default);
    }
}
