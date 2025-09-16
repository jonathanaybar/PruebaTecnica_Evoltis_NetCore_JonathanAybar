using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public interface IHasId<out TKey>
    {
        TKey Id { get; }
    }
}
