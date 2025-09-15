using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDomicilioRepository : IRepository<Domicilio>
    {
        Task<IEnumerable<Domicilio>> BuscarPorCiudad(string ciudad, CancellationToken ct = default);
        Task<Domicilio?> GetConUsuario(int id, CancellationToken ct = default);
    }
}
