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
        Task<List<Usuario>> Search(string? nombre, string? provincia, string? ciudad, CancellationToken ct = default);

    }
}
