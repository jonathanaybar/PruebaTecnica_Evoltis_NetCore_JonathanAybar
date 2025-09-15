using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class UsuarioController : BaseController<Usuario, IUsuarioRepository>
    {
        public UsuarioController(IUsuarioRepository repository) : base(repository)
        { 
        }
    }
}
